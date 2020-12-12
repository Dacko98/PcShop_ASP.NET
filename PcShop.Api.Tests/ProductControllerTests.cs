using Microsoft.AspNetCore.Mvc.Testing;
using PcShop.BL.Api.Models.Evaluation;
using PcShop.BL.Api.Models.Category;
using PcShop.BL.Api.Models.Product;
using System.Collections.Generic;
using FluentAssertions.Common;
using System.Threading.Tasks;
using PcShop.DAL.Entities;
using System.Diagnostics;
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
        private HttpClient client;

        private const string TOO_SHORT_NAME = "c";
        private const string TOO_LONG_NAME = "This name is too long for the model. This name is too long for the model. This name is too long for the model. " +
            "This name is too long for the model. This name is too long for the model. This name is too long for the model. ";

        private readonly ProductListModel[] PRODUCTS_LIST_MODEL =
        {
            new ProductListModel
            {
            Id = new Guid("0d4fa150-ad80-4d46-a511-4c666166ec5a"),
            Name = "Lattitude E6440",
            Photo = "path",
            ManufacturerName = "Dell",
            CategoryName = "Graphic design",
            Description = "...",
            },
            new ProductListModel
            {
            Id = new Guid("23b3902d-7d4f-4213-9cf0-112348f56233"),
            Name = "Thinkpad L580",
            Photo = "path",
            ManufacturerName = "Lenovo",
            CategoryName = "Professional",
            Description = "...",
            }
        };

        private readonly ProductDetailModel[] PRODUCTS_DETAIL_MODEL =
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

        private readonly ProductNewModel[] PRODUCTS_NEW_MODEL =
        {
            new ProductNewModel
            {
                Name = "Hp ProBook 6560b",
                Photo = "path",
                Description = "...",
                Price = 8000,
                Weight = 550,
                CountInStock = 50,
                RAM = null,
                CPU = null,
                GPU = null, 
                HDD = null, 
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
                RAM = null,
                CPU = null,
                GPU = null,
                HDD = null,
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
                RAM = null,
                CPU = null,
                GPU = null,
                HDD = null,
                ManufacturerId = new Guid("0d4fa150-ad80-4d46-a511-4c666166ec5e"),
                CategoryId = new Guid("fabde0cd-eefe-443f-baf6-3d96cc2cbf2e")
            }
        };

        private readonly ProductUpdateModel PRODUCT_UPDATE_MODEL = new ProductUpdateModel
        {
            Id = Guid.Empty,
            Name = "MacBook Air",
            Photo = "path",
            Description = "...",
            Price = 123456,
            Weight = 654321,
            CountInStock = 42,
            RAM = null,
            CPU = null,
            GPU = null,
            HDD = null,
            ManufacturerId = new Guid("0d4fa150-ad80-4d46-a511-4c666166ec5e"),
            CategoryId = new Guid("fabde0cd-eefe-443f-baf6-3d96cc2cbf2e"),
            Evaluations = new List<EvaluationUpdateModel>()
        };

        private ProductDetailModel[] productsDetailModelExpected =
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
                RAM = null,
                CPU = null,
                GPU = null,
                HDD = null, 
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
                RAM = null,
                CPU = null,
                GPU = null,
                HDD = null,
                ManufacturerName = "Dell",
                CategoryName = "Professional",
                Evaluations = new List<EvaluationListModel>()
            }
        };

        public ProductControllerTests(WebApplicationFactory<Startup> fixture)
        {
            client = fixture.CreateClient();
        }

        /*===============================    GetAll Tests    ===============================*/

        [Fact]
        public async Task GetAll_should_result_OK()
        {
            var response = await client.GetAsync("api/Product");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetAll_should_return_some_product()
        {
            var response = await client.GetAsync("api/Product");

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
            var response = await client.GetAsync($"api/Product/{PRODUCTS_DETAIL_MODEL[indexOfTestedProduct].Id}");

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var product = JsonConvert.DeserializeObject<ProductDetailModel>(await response.Content.ReadAsStringAsync());
            product.Should().NotBeNull();
        }

        [Fact]
        public async Task GetById_with_empty_Id_should_return_NotFound()
        {
            // Act
            var response = await client.GetAsync($"api/Product/{Guid.Empty}");

            // Assert
            response.Should().NotBeNull();
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        /*===============================    Create Tests    ===============================*/

        [Fact]
        public async Task Create_should_return_new_ID()
        {
            // Arrange
            var newProductSerialized = JsonConvert.SerializeObject(PRODUCTS_NEW_MODEL[0]);
            var stringContent = new StringContent(newProductSerialized, Encoding.UTF8, "application/json");

            // Act 
            var response = await client.PostAsync("api/Product", stringContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var newProductGuid = JsonConvert.DeserializeObject<Guid>(await response.Content.ReadAsStringAsync());
            newProductGuid.Should().NotBeEmpty();
        }

        [Fact]
        public async Task Create_should_create_findable_Product()
        {
            // Arrange
            var newProductSerialized = JsonConvert.SerializeObject(PRODUCTS_NEW_MODEL[0]);
            var stringContent = new StringContent(newProductSerialized, Encoding.UTF8, "application/json");

            // Act 
            var response = await client.PostAsync("api/Product", stringContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var newProductGuid = JsonConvert.DeserializeObject<Guid>(await response.Content.ReadAsStringAsync());
            newProductGuid.Should().NotBeEmpty();

            var response_GetById = await client.GetAsync($"api/Product/{newProductGuid}");
            response_GetById.StatusCode.Should().Be(HttpStatusCode.OK);
            var product = JsonConvert.DeserializeObject<ProductDetailModel>(await response_GetById.Content.ReadAsStringAsync());
            productsDetailModelExpected[0].Id = product.Id;
            product.Should().BeEquivalentTo(productsDetailModelExpected[0]);
        }

        [Theory]
        [InlineData(TOO_SHORT_NAME)]
        [InlineData(TOO_LONG_NAME)]
        public async Task Create_with_invalid_name_should_return_BadRequest(string wrongName)
        {
            // Arrange 
            PRODUCTS_NEW_MODEL[1].Name = wrongName;
            var newProductSerialized = JsonConvert.SerializeObject(PRODUCTS_NEW_MODEL[1]);
            var stringContent = new StringContent(newProductSerialized, Encoding.UTF8, "application/json");

            // Act
            var response = await client.PostAsync("api/Product", stringContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        /*===============================    Update Tests    ===============================*/

        [Fact]
        public async Task Update_should_update_existing_product()
        {
            // Create new product 
                // Arrange
            var newProductSerialized = JsonConvert.SerializeObject(PRODUCTS_NEW_MODEL[2]);
            var stringContent_create = new StringContent(newProductSerialized, Encoding.UTF8, "application/json");

                // Act 
            var response_create = await client.PostAsync("api/Product", stringContent_create);

                // Assert
            response_create.StatusCode.Should().Be(HttpStatusCode.OK);
            var newProductGuid = JsonConvert.DeserializeObject<Guid>(await response_create.Content.ReadAsStringAsync());
            newProductGuid.Should().NotBeEmpty();
            PRODUCT_UPDATE_MODEL.Id = newProductGuid;


            // Arange
            var productToUpdateSerialized = JsonConvert.SerializeObject(PRODUCT_UPDATE_MODEL);
            var stringContent = new StringContent(productToUpdateSerialized, Encoding.UTF8, "application/json");

            // Act
            var response = await client.PutAsync("api/Product?verison=3.0&culture=en", stringContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            
            // GetById
            var response_GetById = await client.GetAsync($"api/Product/{PRODUCT_UPDATE_MODEL.Id}");
            response_GetById.StatusCode.Should().Be(HttpStatusCode.OK);

            var product = JsonConvert.DeserializeObject<ProductDetailModel>(await response_GetById.Content.ReadAsStringAsync());
            productsDetailModelExpected[1].Id = product.Id;
            product.Should().BeEquivalentTo(productsDetailModelExpected[1]);
        }

        /*===============================    Delete Tests    ===============================*/

        [Fact]
        public async Task Delete_should_delete_product()
        {
            // Arrange - Create new product
            var newProductSerialized = JsonConvert.SerializeObject(PRODUCTS_NEW_MODEL[0]);
            var stringContent = new StringContent(newProductSerialized, Encoding.UTF8, "application/json");

                // Act 
            var response_create = await client.PostAsync("api/Product", stringContent);

                // Assert
            response_create.StatusCode.Should().Be(HttpStatusCode.OK);
            var newProductGuid = JsonConvert.DeserializeObject<Guid>(await response_create.Content.ReadAsStringAsync());
            newProductGuid.Should().NotBeEmpty();

            // Act
            var response = await client.DeleteAsync($"api/Product/{newProductGuid}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            // GetById deleted Product -> assert NotFound
                // Act
            var response_GetById = await client.GetAsync($"api/Product/{newProductGuid}");

            // Assert
            response_GetById.Should().NotBeNull();
            response_GetById.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Delete_empty_Id_should_return_BadRequest()
        {
            // Arrange 
            var newProductGuid = Guid.Empty;

            // Act
            var response = await client.DeleteAsync($"api/Product/{newProductGuid}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
