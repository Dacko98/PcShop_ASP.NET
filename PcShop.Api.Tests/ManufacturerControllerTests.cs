using PcShop.BL.Api.Models.Manufacturer;
using Microsoft.AspNetCore.Mvc.Testing;
using PcShop.BL.Api.Models.Product;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using System.Net;
using System;
using Xunit;

namespace PcShop.Api.Tests
{
    [Collection(name: "ManufacturerControllerTests")]
    public class ManufacturerControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;
        private const string TooShortName = "c";
        private const string TooLongName = "This name is too long for the model. This name is too long for the model. This name is too long for the model. " +
            "This name is too long for the model. This name is too long for the model. This name is too long for the model. ";

        private const string Manufacturer1Id = "0d4fa150-ad80-4d46-a511-4c666166ec5e";
        private const string Manufacturer1Country = "USA";

        private const string Manufacturer2Id = "87833e66-05ba-4d6b-900b-fe5ace88dbd8";
        private const string Manufacturer2Country = "China";

        private readonly ManufacturerNewModel _newManufacturer = new ManufacturerNewModel
        {
            Name = "Hpcko",
            Description = "...",
            Logo = "path",
            CountryOfOrigin = "UK"
        };

        private readonly ManufacturerUpdateModel _updateManufacturer = new ManufacturerUpdateModel
        {
            Id = Guid.Empty,
            Name = "Hpcko",
            Description = "Good Manufacturer, you can count on it.",
            Logo = "path",
            CountryOfOrigin = "UK",
            Product = new List<ProductOnlyIdUpdateModel>()
        };

        private readonly ManufacturerDetailModel[] _expectedManufacturers =
        {
            new ManufacturerDetailModel
            {
                Id = Guid.Empty,
                Name = "Hpcko",
                Description = "...",
                Logo = "path",
                CountryOfOrigin = "UK",
                Product = new List<ProductListModel>()
            },
            new ManufacturerDetailModel
            {
                Id = Guid.Empty,
                Name = "Hpcko",
                Description = "Good Manufacturer, you can count on it.",
                Logo = "path",
                CountryOfOrigin = "UK",
                Product = new List<ProductListModel>() 
            }
        };
        
        public ManufacturerControllerTests(WebApplicationFactory<Startup> fixture)
        {
            _client = fixture.CreateClient();
        }

        /*===============================    GetAll Tests    ===============================*/

        [Fact]
        public async Task GetAll_should_result_OK()
        {
            var response = await _client.GetAsync("api/Manufacturer");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetAll_should_return_some_Manufacturers()
        {
            var response = await _client.GetAsync("api/Manufacturer");

            response.StatusCode.Should().Be(HttpStatusCode.OK);


            var manufacturers = JsonConvert.DeserializeObject<List<ManufacturerListModel>>(await response.Content.ReadAsStringAsync());
            manufacturers.Should().HaveCountGreaterOrEqualTo(1);
        }

        [Fact]
        public async Task GetAll_should_return_2_manufacturers()
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

        [Theory]
        [InlineData(Manufacturer1Id, Manufacturer1Country)]
        [InlineData(Manufacturer2Id, Manufacturer2Country)]
        public async Task GetById_should_return_something(string testedId, string country)
        {
            var response = await _client.GetAsync($"api/Manufacturer/{testedId}");

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var manufacturer = JsonConvert.DeserializeObject<ManufacturerDetailModel>(await response.Content.ReadAsStringAsync());
            manufacturer.Should().NotBeNull();
            manufacturer.CountryOfOrigin.Should().Be(country);
        }

        [Fact]
        public async Task GetById_with_empty_Id_should_return_NotFound()
        {
            // Act
            var response = await _client.GetAsync($"api/Manufacturer/{Guid.Empty}");

            // Assert
            response.Should().NotBeNull();
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        /*===============================    Create Tests    ===============================*/

        [Fact]
        public async Task Create_should_return_new_ID()
        {
            // Arrange
            var newManufacturerSerialized = JsonConvert.SerializeObject(_newManufacturer);
            var stringContent = new StringContent(newManufacturerSerialized, Encoding.UTF8, "application/json");

            // Act 
            var response = await _client.PostAsync("api/Manufacturer", stringContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var newManufacturerGuid = JsonConvert.DeserializeObject<Guid>(await response.Content.ReadAsStringAsync());
            newManufacturerGuid.Should().NotBe(Guid.Empty);
        }

        [Fact]
        public async Task Create_should_create_findable_manufacturer()
        {
            // Arrange
            var newManufacturerSerialized = JsonConvert.SerializeObject(_newManufacturer);
            var stringContent = new StringContent(newManufacturerSerialized, Encoding.UTF8, "application/json");

            // Act 
            var response = await _client.PostAsync("api/Manufacturer", stringContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var newManufacturerGuid = JsonConvert.DeserializeObject<Guid>(await response.Content.ReadAsStringAsync());
            newManufacturerGuid.Should().NotBe(Guid.Empty);

            _expectedManufacturers[0].Id = newManufacturerGuid;

            var responseGetById = await _client.GetAsync($"api/Manufacturer/{newManufacturerGuid}");
            responseGetById.StatusCode.Should().Be(HttpStatusCode.OK);

            var manufacturer = JsonConvert.DeserializeObject<ManufacturerDetailModel>(await responseGetById.Content.ReadAsStringAsync());
            manufacturer.Id.Should().NotBe(Guid.Empty);
            manufacturer.Should().BeEquivalentTo(_expectedManufacturers[0]);
        }

        [Theory]
        [InlineData(TooShortName)]
        [InlineData(TooLongName)]
        public async Task Create_with_invalid_name_should_return_BadRequest(string newName)
        {
            // Arrange 
            var newwManufacturerSerialized = JsonConvert.SerializeObject(_newManufacturer);
            var stringContent = new StringContent(newwManufacturerSerialized, Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("api/wManufacturer", stringContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        /*===============================    Update Tests    ===============================*/

        [Fact]
        public async Task Update_should_update_existing_Manufacturer()
        {
            // Arrange
            var newManufacturerSerialized = JsonConvert.SerializeObject(_newManufacturer);
            var stringContentCreate = new StringContent(newManufacturerSerialized, Encoding.UTF8, "application/json");
            var responseCreate = await _client.PostAsync("api/Manufacturer", stringContentCreate);
            var newManufacturerGuid = JsonConvert.DeserializeObject<Guid>(await responseCreate.Content.ReadAsStringAsync());

            _updateManufacturer.Id = newManufacturerGuid;
            _expectedManufacturers[1].Id = newManufacturerGuid;

            var manufacturerToUpdateSerialized = JsonConvert.SerializeObject(_updateManufacturer);
            var stringContent = new StringContent(manufacturerToUpdateSerialized, Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PutAsync("api/Manufacturer?verison=3.0&culture=en", stringContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var responseGetById = await _client.GetAsync($"api/Manufacturer/{_updateManufacturer.Id}");
            var manufacturer = JsonConvert.DeserializeObject<ManufacturerDetailModel>(await responseGetById.Content.ReadAsStringAsync());
            manufacturer.Should().BeEquivalentTo(_expectedManufacturers[1]);
        }

        /*===============================    Delete Tests    ===============================*/

        [Fact]
        public async Task Delete_should_delete_manufacturer()
        {
            // Arrange
            var newManufacturerSerialized = JsonConvert.SerializeObject(_newManufacturer);
            var stringContent = new StringContent(newManufacturerSerialized, Encoding.UTF8, "application/json");
            var responseCreate = await _client.PostAsync("api/Manufacturer", stringContent);
            var newManufacturerGuid = JsonConvert.DeserializeObject<Guid>(await responseCreate.Content.ReadAsStringAsync());

            // Act
            var response = await _client.DeleteAsync($"api/Manufacturer/{newManufacturerGuid}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var responseGetById = await _client.GetAsync($"api/Manufacturer/{_newManufacturer}");
            responseGetById.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Delete_empty_Id_should_return_BadRequest()
        {
            // Arrange 
            var newManufacturerGuid = Guid.Empty;

            // Act
            var response = await _client.DeleteAsync($"api/Manufacturer/{newManufacturerGuid}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
