using Microsoft.AspNetCore.Mvc.Testing;
using PcShop.BL.Api.Models.Evaluation;
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
    [Collection(name: "productControllerTests")]
    public class ProductControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        private const string TooShortName = "c";
        private const string TooLongName = "This name is too long for the model. This name is too long for the model. This name is too long for the model. " +
            "This name is too long for the model. This name is too long for the model. This name is too long for the model. ";
        
        private readonly ProductDetailModel[] _productsDetailModel =
        {
            new ProductDetailModel
            {
                Id = new Guid("0d4fa150-ad80-4d46-a511-4c666166ec5a"),
                Name = "Lattitude E6440",
                Description = "...",
                Price = 600,
                Weight = 200,
                CountInStock = 10,
                Photo = "path",
                ManufacturerName = "Dell",
                CategoryName = "Graphic design",
                Evaluations = new List<EvaluationListModel>()
            },
            new ProductDetailModel
            {
                Id = new Guid("23b3902d-7d4f-4213-9cf0-112348f56233"),
                Name = "Thinkpad L580",
                Description = "...",
                Price = 1000,
                Weight = 200,
                CountInStock = 10,
                Photo = "path",
                ManufacturerName = "Lenovo",
                CategoryName = "Professional",
                Evaluations = new List<EvaluationListModel>()
            }
        };

        private readonly ProductNewModel[] _productsNewModel =
        {
            new ProductNewModel
            {
                Name = "Hp ProBook 6560b",
                Photo = "path",
                Description = "...",
                Price = 8000,
                Weight = 550,
                CountInStock = 50,
                Ram = null,
                Cpu = null,
                Gpu = null, 
                Hdd = null, 
                ManufacturerId = new Guid("0d4fa150-ad80-4d46-a511-4c666166ec5e"),
                CategoryId = new Guid("fabde0cd-eefe-443f-baf6-3d96cc2cbf2e")
            },
            new ProductNewModel
            {
                Name = "",
                Photo = "path",
                Description = "...",
                Price = 123456,
                Weight = 654321,
                CountInStock = 42,
                Ram = null,
                Cpu = null,
                Gpu = null,
                Hdd = null,
                ManufacturerId = new Guid("0d4fa150-ad80-4d46-a511-4c666166ec5e"),
                CategoryId = new Guid("fabde0cd-eefe-443f-baf6-3d96cc2cbf2e")
            }, 
            new ProductNewModel
            {
                Name = "MacKdoviCo-WrongName",
                Photo = "path",
                Description = "...",
                Price = 123456,
                Weight = 654321,
                CountInStock = 42,
                Ram = null,
                Cpu = null,
                Gpu = null,
                Hdd = null,
                ManufacturerId = new Guid("0d4fa150-ad80-4d46-a511-4c666166ec5e"),
                CategoryId = new Guid("fabde0cd-eefe-443f-baf6-3d96cc2cbf2e")
            }
        };

        private readonly ProductUpdateModel _productUpdateModel = new ProductUpdateModel
        {
            Id = Guid.Empty,
            Name = "MacBook Air",
            Photo = "path",
            Description = "...",
            Price = 123456,
            Weight = 654321,
            CountInStock = 42,
            Ram = null,
            Cpu = null,
            Gpu = null,
            Hdd = null,
            ManufacturerId = new Guid("0d4fa150-ad80-4d46-a511-4c666166ec5e"),
            CategoryId = new Guid("fabde0cd-eefe-443f-baf6-3d96cc2cbf2e"),
            Evaluations = new List<EvaluationUpdateModel>()
        };

        private readonly ProductDetailModel[] _productsDetailModelExpected =
        {
            new ProductDetailModel
            {
                Id = Guid.Empty,
                Name = "Hp ProBook 6560b",
                Photo = "path",
                Description = "...",
                Price = 8000,
                Weight = 550,
                CountInStock = 50,
                ManufacturerName = "Dell",
                CategoryName = "Professional",
                Ram = null,
                Cpu = null,
                Gpu = null,
                Hdd = null, 
                Evaluations = new List<EvaluationListModel>()
            },
            new ProductDetailModel
            {
                Id = Guid.Empty,
                Name = "MacBook Air",
                Photo = "path",
                Description = "...",
                Price = 123456,
                Weight = 654321,
                CountInStock = 42,
                Ram = null,
                Cpu = null,
                Gpu = null,
                Hdd = null,
                ManufacturerName = "Dell",
                CategoryName = "Professional",
                Evaluations = new List<EvaluationListModel>()
            }
        };

        public ProductControllerTests(WebApplicationFactory<Startup> fixture)
        {
            _client = fixture.CreateClient();
        }

        /*===============================    GetAll Tests    ===============================*/

        [Fact]
        public async Task GetAll_should_result_OK()
        {
            var response = await _client.GetAsync("api/Product");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetAll_should_return_some_product()
        {
            var response = await _client.GetAsync("api/Product");

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var products = JsonConvert.DeserializeObject<List<ProductListModel>>(await response.Content.ReadAsStringAsync());
            products.Should().HaveCountGreaterOrEqualTo(1);
        }

        /*===============================    GetById Tests    ===============================*/

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public async Task GetById_should_return_something(int indexOfTestedProduct)
        {
            var response = await _client.GetAsync($"api/Product/{_productsDetailModel[indexOfTestedProduct].Id}");

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var product = JsonConvert.DeserializeObject<ProductDetailModel>(await response.Content.ReadAsStringAsync());
            product.Should().NotBeNull();
        }

        [Fact]
        public async Task GetById_with_empty_Id_should_return_NotFound()
        {
            // Act
            var response = await _client.GetAsync($"api/Product/{Guid.Empty}");

            // Assert
            response.Should().NotBeNull();
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        /*===============================    Create Tests    ===============================*/

        [Fact]
        public async Task Create_should_return_new_ID()
        {
            // Arrange
            var newProductSerialized = JsonConvert.SerializeObject(_productsNewModel[0]);
            var stringContent = new StringContent(newProductSerialized, Encoding.UTF8, "application/json");

            // Act 
            var response = await _client.PostAsync("api/Product", stringContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var newProductGuid = JsonConvert.DeserializeObject<Guid>(await response.Content.ReadAsStringAsync());
            newProductGuid.Should().NotBeEmpty();
        }

        [Fact]
        public async Task Create_should_create_findable_Product()
        {
            // Arrange
            var newProductSerialized = JsonConvert.SerializeObject(_productsNewModel[0]);
            var stringContent = new StringContent(newProductSerialized, Encoding.UTF8, "application/json");

            // Act 
            var response = await _client.PostAsync("api/Product", stringContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var newProductGuid = JsonConvert.DeserializeObject<Guid>(await response.Content.ReadAsStringAsync());
            newProductGuid.Should().NotBeEmpty();

            var responseGetById = await _client.GetAsync($"api/Product/{newProductGuid}");
            responseGetById.StatusCode.Should().Be(HttpStatusCode.OK);
            var product = JsonConvert.DeserializeObject<ProductDetailModel>(await responseGetById.Content.ReadAsStringAsync());
            _productsDetailModelExpected[0].Id = product.Id;
            product.Should().BeEquivalentTo(_productsDetailModelExpected[0]);
        }

        [Theory]
        [InlineData(TooShortName)]
        [InlineData(TooLongName)]
        public async Task Create_with_invalid_name_should_return_BadRequest(string wrongName)
        {
            // Arrange 
            _productsNewModel[1].Name = wrongName;
            var newProductSerialized = JsonConvert.SerializeObject(_productsNewModel[1]);
            var stringContent = new StringContent(newProductSerialized, Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("api/Product", stringContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        /*===============================    Update Tests    ===============================*/

        [Fact]
        public async Task Update_should_update_existing_product()
        {
            // Create new product 
                // Arrange
            var newProductSerialized = JsonConvert.SerializeObject(_productsNewModel[2]);
            var stringContentCreate = new StringContent(newProductSerialized, Encoding.UTF8, "application/json");

                // Act 
            var responseCreate = await _client.PostAsync("api/Product", stringContentCreate);

                // Assert
            responseCreate.StatusCode.Should().Be(HttpStatusCode.OK);
            var newProductGuid = JsonConvert.DeserializeObject<Guid>(await responseCreate.Content.ReadAsStringAsync());
            newProductGuid.Should().NotBeEmpty();
            _productUpdateModel.Id = newProductGuid;


            // Arange
            var productToUpdateSerialized = JsonConvert.SerializeObject(_productUpdateModel);
            var stringContent = new StringContent(productToUpdateSerialized, Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PutAsync("api/Product?verison=3.0&culture=en", stringContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            
            // GetById
            var responseGetById = await _client.GetAsync($"api/Product/{_productUpdateModel.Id}");
            responseGetById.StatusCode.Should().Be(HttpStatusCode.OK);

            var product = JsonConvert.DeserializeObject<ProductDetailModel>(await responseGetById.Content.ReadAsStringAsync());
            _productsDetailModelExpected[1].Id = product.Id;
            product.Should().BeEquivalentTo(_productsDetailModelExpected[1]);
        }

        /*===============================    Delete Tests    ===============================*/

        [Fact]
        public async Task Delete_should_delete_product()
        {
            // Arrange - Create new product
            var newProductSerialized = JsonConvert.SerializeObject(_productsNewModel[0]);
            var stringContent = new StringContent(newProductSerialized, Encoding.UTF8, "application/json");

                // Act 
            var responseCreate = await _client.PostAsync("api/Product", stringContent);

                // Assert
            responseCreate.StatusCode.Should().Be(HttpStatusCode.OK);
            var newProductGuid = JsonConvert.DeserializeObject<Guid>(await responseCreate.Content.ReadAsStringAsync());
            newProductGuid.Should().NotBeEmpty();

            // Act
            var response = await _client.DeleteAsync($"api/Product/{newProductGuid}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            // GetById deleted Product -> assert NotFound
                // Act
            var responseGetById = await _client.GetAsync($"api/Product/{newProductGuid}");

            // Assert
            responseGetById.Should().NotBeNull();
            responseGetById.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Delete_empty_Id_should_return_BadRequest()
        {
            // Arrange 
            var newProductGuid = Guid.Empty;

            // Act
            var response = await _client.DeleteAsync($"api/Product/{newProductGuid}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
