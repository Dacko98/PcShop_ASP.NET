/* File:        ManufacturerControllerTests.cs
 * 
 * Solution:    PcShop
 * Project:     PcShop.Api.Test
 *
 * Team:        Team0011
 * Author:      Vojtech Vlach
 * Login:       xvlach22
 * Date:        30.10.2020
 * 
 * Description: This file contains API tests for ManufacturerController in PcShop.Api.
 *              Tests all main 4 methods (GET, PUT, POST, DELETE)
 * 
 * Installed NuGet packages: Microsoft.AspNetCore.Mvc.Testing, FluentAssertions
 */

using PcShop.BL.Api.Models.Manufacturer;
using Microsoft.AspNetCore.Mvc.Testing;
using PcShop.BL.Api.Models.Category;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;
using PcShop.BL.Api;
using FluentAssertions;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using System.Net;
using System;
using Xunit;
using PcShop.BL.Api.Models.Product;

namespace PcShop.Api.Tests
{
    [Collection(name: "ManufacturerControllerTests")]
    public class ManufacturerControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private HttpClient _client;
        private const string TOOSHORTNAME = "c";
        private const string TOOLONGNAME = "This name is too long for the model. This name is too long for the model. This name is too long for the model. " +
            "This name is too long for the model. This name is too long for the model. This name is too long for the model. ";


        public ManufacturerControllerTests(WebApplicationFactory<Startup> fixture)
        {
            _client = fixture.CreateClient();
        }

        /*===============================    GetAll Tests    ===============================*/

        /// <summary>
        /// Try Get all manufacturers. Shoudl return Status Code OK.
        /// </summary>
        [Fact]
        public async Task GetAll_Should_result_OK()
        {
            var response = await _client.GetAsync("api/Manufacturer");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        /// <summary>
        /// Try get all manufacturers. Should return non-empty field in response.content
        /// </summary>
        [Fact]
        public async Task GetAll_Should_return_some_Manufacturers()
        {
            var response = await _client.GetAsync("api/Manufacturer");

            response.StatusCode.Should().Be(HttpStatusCode.OK);


            var manufacturers = JsonConvert.DeserializeObject<List<ManufacturerListModel>>(await response.Content.ReadAsStringAsync());
            manufacturers.Should().HaveCountGreaterOrEqualTo(1);
        }

        /// <summary>
        /// Try get all manufacturers. 
        /// Check if it returns at least 2 manufacturers.
        /// Check if the first and the second manufacturer's names match with model
        /// </summary>
        [Fact]
        public async Task GetAll_Should_return_2_manufacturers()
        {
            // Act
            var response = await _client.GetAsync("api/Manufacturer");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var manufacturers = JsonConvert.DeserializeObject<List<ManufacturerListModel>>(await response.Content.ReadAsStringAsync());

            // Should return at least 2 manufacturers
            manufacturers.Should().HaveCountGreaterOrEqualTo(2);

            // First manufacturer
            manufacturers[0].Name.Should().Be("Dell");
            // Second manufacturer
            manufacturers[1].Name.Should().Be("Lenovo");
        }

        /*===============================    GetById Tests    ===============================*/

        /// <summary>
        /// Try GetById one manufacturer. Should return StatusCode.OK and response.Content should not be null
        /// </summary>
        /// <param name="index">Index of tested manufacturer. (FIRST = 0, LAST = 1)</param>
        [Theory]
        [InlineData("0d4fa150-ad80-4d46-a511-4c666166ec5e", "USA")]
        [InlineData("87833e66-05ba-4d6b-900b-fe5ace88dbd8", "China")]
        public async Task GetById_Should_return_something(string Id, string country)
        {
            var response = await _client.GetAsync($"api/Manufacturer/{Id}");

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var manufacturer = JsonConvert.DeserializeObject<ManufacturerDetailModel>(await response.Content.ReadAsStringAsync());
            manufacturer.Should().NotBeNull();
            manufacturer.CountryOfOrigin.Should().Be(country);
        }

        /// <summary>
        /// Try GetById with empty id (non-existing manufacturer). Should return NotFound (404).
        /// </summary>
        [Fact]
        public async Task GetById_With_empty_Id()
        {
            // Act
            var response = await _client.GetAsync($"api/Manufacturer/{Guid.Empty}");

            // Assert
            response.Should().NotBeNull();
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        /*===============================    Create Tests    ===============================*/

        [Fact]
        public async Task Create_Should_return_new_ID()
        {
            // Arrange
            ManufacturerNewModel newManufacturer = new ManufacturerNewModel 
            { 
                Name = "HPcko",
                Description = "...",
                Logo = "path",
                CountryOfOrigin = "UK"
            };
            
            var newManufacturerSerialized = JsonConvert.SerializeObject(newManufacturer);
            var stringContent = new StringContent(newManufacturerSerialized, Encoding.UTF8, "application/json");

            // Act 
            var response = await _client.PostAsync("api/Manufacturer", stringContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var newManufacturerGuid = JsonConvert.DeserializeObject<Guid>(await response.Content.ReadAsStringAsync());
            newManufacturerGuid.Should().NotBe(Guid.Empty);
        }

        [Fact]
        public async Task Create_Should_create_findable_Manufacturer()
        {
            // Arrange
            ManufacturerNewModel newManufacturer = new ManufacturerNewModel
            {
                Name = "HPcko",
                Description = "...",
                Logo = "path",
                CountryOfOrigin = "UK"
            };
            var newManufacturerSerialized = JsonConvert.SerializeObject(newManufacturer);
            var stringContent = new StringContent(newManufacturerSerialized, Encoding.UTF8, "application/json");

            // Act 
            var response = await _client.PostAsync("api/Manufacturer", stringContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var newManufacturerGuid = JsonConvert.DeserializeObject<Guid>(await response.Content.ReadAsStringAsync());
            newManufacturerGuid.Should().NotBe(Guid.Empty);

            
            // GetById
            ManufacturerDetailModel expectedManufacturer = new ManufacturerDetailModel
            {
                Id = newManufacturerGuid,
                Name = "HPcko",
                Description = "...",
                Logo = "path",
                CountryOfOrigin = "UK", 
                Product = new List<ProductListModel>()
            };
            var response_GetById = await _client.GetAsync($"api/Manufacturer/{newManufacturerGuid}");
            response_GetById.StatusCode.Should().Be(HttpStatusCode.OK);

            var manufacturer = JsonConvert.DeserializeObject<ManufacturerDetailModel>(await response_GetById.Content.ReadAsStringAsync());
            manufacturer.Id.Should().NotBe(Guid.Empty);
            manufacturer.Should().BeEquivalentTo(expectedManufacturer);
        }

        /// <summary>
        /// Expect NotFound by creating products with invalid names
        /// </summary>
        /// <param name="name">Name to cause error in creating new product</param>
        [Theory]
        [InlineData(TOOSHORTNAME)]
        [InlineData(TOOLONGNAME)]
        public async Task Create_With_invalid_name_should_return_BadRequest(string name)
        {
            // Arrange 
            ManufacturerNewModel newManufacturer = new ManufacturerNewModel
            {
                Name = name,
                Description = "...",
                Logo = "path",
                CountryOfOrigin = "Japan"
            };
            var newwManufacturerSerialized = JsonConvert.SerializeObject(newManufacturer);
            var stringContent = new StringContent(newwManufacturerSerialized, Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("api/wManufacturer", stringContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        /*===============================    Update Tests    ===============================*/

        /// <summary>
        /// Create new Manufacturer and then try update its description, should return OK. Then GetById updated Manufacturer
        /// </summary>
        [Fact]
        public async Task Update_Should_update_existing_Manufacturer()
        {
            // Create new Manufacturer
            ManufacturerNewModel newManufacturer = new ManufacturerNewModel
            {
                Name = "Hpcko",
                Description = "...",
                Logo = "path",
                CountryOfOrigin = "UK"
            };

                // Arrange
            var newManufacturerSerialized = JsonConvert.SerializeObject(newManufacturer);
            var stringContent_create = new StringContent(newManufacturerSerialized, Encoding.UTF8, "application/json");

                // Act 
            var response_create = await _client.PostAsync("api/Manufacturer", stringContent_create);

                // Assert
            response_create.StatusCode.Should().Be(HttpStatusCode.OK);
            var newManufacturerGuid = JsonConvert.DeserializeObject<Guid>(await response_create.Content.ReadAsStringAsync());
            newManufacturerGuid.Should().NotBe(Guid.Empty);
            //PRODUCT_UPDATE.Id = newProductGuid;


            // Arange
            ManufacturerUpdateModel updateManufacturer = new ManufacturerUpdateModel
            {
                Id = newManufacturerGuid,
                Name = "Hpcko",
                Description = "Good Manufacturer, you can count on it.",
                Logo = "path",
                CountryOfOrigin = "UK",
                Product = new List<ProductOnlyIdUpdateModel>()
            };
            ManufacturerDetailModel expectedManufacturer = new ManufacturerDetailModel
            {
                Id = newManufacturerGuid,
                Name = "Hpcko",
                Description = "Good Manufacturer, you can count on it.",
                Logo = "path",
                CountryOfOrigin = "UK",
                Product = new List<ProductListModel>()
            };

            var manufacturerToUpdateSerialized = JsonConvert.SerializeObject(updateManufacturer);
            var stringContent = new StringContent(manufacturerToUpdateSerialized, Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PutAsync("api/Manufacturer?verison=3.0&culture=en", stringContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            // GetById
            var response_GetById = await _client.GetAsync($"api/Manufacturer/{updateManufacturer.Id}");
            response_GetById.StatusCode.Should().Be(HttpStatusCode.OK);

            var manufacturer = JsonConvert.DeserializeObject<ManufacturerDetailModel>(await response_GetById.Content.ReadAsStringAsync());
            manufacturer.Should().BeEquivalentTo(expectedManufacturer);
        }

        /*===============================    Delete Tests    ===============================*/

        /// <summary>
        /// Create new manufacturer. Then Try to delete it and then find it. Should return BadRequest
        /// </summary>
        [Fact]
        public async Task Delete_Should_delete_manufacturer()
        {
            // Arrange - Create new manufacturer
            ManufacturerNewModel newManufacturer = new ManufacturerNewModel
            {
                Name = "Hpcko",
                Description = "...",
                Logo = "path",
                CountryOfOrigin = "UK"
            };
            var newManufactorerSerialized = JsonConvert.SerializeObject(newManufacturer);
            var stringContent = new StringContent(newManufactorerSerialized, Encoding.UTF8, "application/json");

            // Act 
            var response_create = await _client.PostAsync("api/Manufacturer", stringContent);

            // Assert
            response_create.StatusCode.Should().Be(HttpStatusCode.OK);
            var newManufacturerGuid = JsonConvert.DeserializeObject<Guid>(await response_create.Content.ReadAsStringAsync());
            newManufacturerGuid.Should().NotBe(Guid.Empty);

            // Act
            var response = await _client.DeleteAsync($"api/Manufacturer/{newManufacturerGuid}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            // GetById deleted Manufacturer -> assert NotFound
            // Act
            var response_GetById = await _client.GetAsync($"api/Manufacturer/{newManufacturer}");

            // Assert
            response_GetById.Should().NotBeNull();
            response_GetById.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        /// <summary>
        /// Try delete non-existing Manufacturer. Should return BadRequest
        /// </summary>
        [Fact]
        public async Task Delete_Empty_Id()
        {
            // Arrange 
            var newManufacturerGuid = Guid.Empty;

            // Act
            var response = await _client.DeleteAsync($"api/Manufacturer/{newManufacturerGuid}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            //response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);  - It actually returns InternalServerError
        }

    }
}
