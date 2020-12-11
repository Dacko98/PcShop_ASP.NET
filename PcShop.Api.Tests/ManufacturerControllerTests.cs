using PcShop.BL.Api.Models.Manufacturer;
using Microsoft.AspNetCore.Mvc.Testing;
using PcShop.BL.Api.Models.Category;
using PcShop.BL.Api.Models.Product;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;
using FluentAssertions;
using System.Net.Http;
using Newtonsoft.Json;
using PcShop.BL.Api;
using System.Text;
using System.Net;
using System;
using Xunit;

namespace PcShop.Api.Tests
{
    [Collection(name: "ManufacturerControllerTests")]
    public class ManufacturerControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private HttpClient client;
        private const string TOO_SHORT_NAME = "c";
        private const string TOO_LONG_NAME = "This name is too long for the model. This name is too long for the model. This name is too long for the model. " +
            "This name is too long for the model. This name is too long for the model. This name is too long for the model. ";

        private const string MANUFACTURER_1_ID = "0d4fa150-ad80-4d46-a511-4c666166ec5e";
        private const string MANUFACTURER_1_COUNTRY = "USA";

        private const string MANUFACTURER_2_ID = "87833e66-05ba-4d6b-900b-fe5ace88dbd8";
        private const string MANUFACTURER_2_COUNTRY = "China";

        public ManufacturerControllerTests(WebApplicationFactory<Startup> fixture)
        {
            client = fixture.CreateClient();
        }

        /*===============================    GetAll Tests    ===============================*/

        [Fact]
        public async Task GetAll_should_result_OK()
        {
            var response = await client.GetAsync("api/Manufacturer");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetAll_should_return_some_Manufacturers()
        {
            var response = await client.GetAsync("api/Manufacturer");

            response.StatusCode.Should().Be(HttpStatusCode.OK);


            var manufacturers = JsonConvert.DeserializeObject<List<ManufacturerListModel>>(await response.Content.ReadAsStringAsync());
            manufacturers.Should().HaveCountGreaterOrEqualTo(1);
        }

        [Fact]
        public async Task GetAll_should_return_2_manufacturers()
        {
            // Act
            var response = await client.GetAsync("api/Manufacturer");

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

        [Theory]
        [InlineData(MANUFACTURER_1_ID, MANUFACTURER_1_COUNTRY)]
        [InlineData(MANUFACTURER_2_ID, MANUFACTURER_2_COUNTRY)]
        public async Task GetById_should_return_something(string testedId, string country)
        {
            var response = await client.GetAsync($"api/Manufacturer/{testedId}");

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var manufacturer = JsonConvert.DeserializeObject<ManufacturerDetailModel>(await response.Content.ReadAsStringAsync());
            manufacturer.Should().NotBeNull();
            manufacturer.CountryOfOrigin.Should().Be(country);
        }

        [Fact]
        public async Task GetById_with_empty_Id_should_return_NotFound()
        {
            // Act
            var response = await client.GetAsync($"api/Manufacturer/{Guid.Empty}");

            // Assert
            response.Should().NotBeNull();
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        /*===============================    Create Tests    ===============================*/

        [Fact]
        public async Task Create_should_return_new_ID()
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
            var response = await client.PostAsync("api/Manufacturer", stringContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var newManufacturerGuid = JsonConvert.DeserializeObject<Guid>(await response.Content.ReadAsStringAsync());
            newManufacturerGuid.Should().NotBe(Guid.Empty);
        }

        [Fact]
        public async Task Create_should_create_findable_manufacturer()
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
            var response = await client.PostAsync("api/Manufacturer", stringContent);

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
            var response_GetById = await client.GetAsync($"api/Manufacturer/{newManufacturerGuid}");
            response_GetById.StatusCode.Should().Be(HttpStatusCode.OK);

            var manufacturer = JsonConvert.DeserializeObject<ManufacturerDetailModel>(await response_GetById.Content.ReadAsStringAsync());
            manufacturer.Id.Should().NotBe(Guid.Empty);
            manufacturer.Should().BeEquivalentTo(expectedManufacturer);
        }

        [Theory]
        [InlineData(TOO_SHORT_NAME)]
        [InlineData(TOO_LONG_NAME)]
        public async Task Create_with_invalid_name_should_return_BadRequest(string newName)
        {
            // Arrange 
            ManufacturerNewModel newManufacturer = new ManufacturerNewModel
            {
                Name = newName,
                Description = "...",
                Logo = "path",
                CountryOfOrigin = "Japan"
            };
            var newwManufacturerSerialized = JsonConvert.SerializeObject(newManufacturer);
            var stringContent = new StringContent(newwManufacturerSerialized, Encoding.UTF8, "application/json");

            // Act
            var response = await client.PostAsync("api/wManufacturer", stringContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        /*===============================    Update Tests    ===============================*/

        [Fact]
        public async Task Update_should_update_existing_Manufacturer()
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
            var response_create = await client.PostAsync("api/Manufacturer", stringContent_create);

                // Assert
            response_create.StatusCode.Should().Be(HttpStatusCode.OK);
            var newManufacturerGuid = JsonConvert.DeserializeObject<Guid>(await response_create.Content.ReadAsStringAsync());
            newManufacturerGuid.Should().NotBe(Guid.Empty);

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
            var response = await client.PutAsync("api/Manufacturer?verison=3.0&culture=en", stringContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            // GetById
            var response_GetById = await client.GetAsync($"api/Manufacturer/{updateManufacturer.Id}");
            response_GetById.StatusCode.Should().Be(HttpStatusCode.OK);

            var manufacturer = JsonConvert.DeserializeObject<ManufacturerDetailModel>(await response_GetById.Content.ReadAsStringAsync());
            manufacturer.Should().BeEquivalentTo(expectedManufacturer);
        }

        /*===============================    Delete Tests    ===============================*/

        [Fact]
        public async Task Delete_should_delete_manufacturer()
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
            var response_create = await client.PostAsync("api/Manufacturer", stringContent);

            // Assert
            response_create.StatusCode.Should().Be(HttpStatusCode.OK);
            var newManufacturerGuid = JsonConvert.DeserializeObject<Guid>(await response_create.Content.ReadAsStringAsync());
            newManufacturerGuid.Should().NotBe(Guid.Empty);

            // Act
            var response = await client.DeleteAsync($"api/Manufacturer/{newManufacturerGuid}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            // GetById deleted Manufacturer -> assert NotFound
            // Act
            var response_GetById = await client.GetAsync($"api/Manufacturer/{newManufacturer}");

            // Assert
            response_GetById.Should().NotBeNull();
            response_GetById.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Delete_empty_Id_should_return_BadRequest()
        {
            // Arrange 
            var newManufacturerGuid = Guid.Empty;

            // Act
            var response = await client.DeleteAsync($"api/Manufacturer/{newManufacturerGuid}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
