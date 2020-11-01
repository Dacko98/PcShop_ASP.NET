/* File:        GoodsControllerTests.cs
 * 
 * Solution:    PcShop
 * Project:     PcShop.Api.Test
 *
 * Team:        Team0011
 * Author:      Vojtech Vlach
 * Login:       xvlach22
 * Date:        30.10.2020
 * 
 * Description: This file contains API tests for GoodsController in PcShop.Api.
 *              Tests all main 4 methods (GET, PUT, POST, DELETE)
 * 
 * Installed NuGet packages: Microsoft.AspNetCore.Mvc.Testing, FluentAssertions
 */

using Microsoft.AspNetCore.Mvc.Testing;
using PcShop.BL.Api.Models.Category;
using PcShop.BL.Api.Models.Product;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;
using FluentAssertions;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using System.Net;
using System;
using Xunit;
using FluentAssertions.Common;
using PcShop.BL.Api.Models.Evaluation;

namespace PcShop.Api.Tests
{
    [Collection(name: "GoodsControllerTests")]
    public class GoodsControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private HttpClient _client;

        // This should maybe go to some sort of shared memory between tests: 
        // Constant for first product PRODUCT_RIRST
        private const string TOOSHORTNAME = "c";
        private const string TOOLONGNAME = "This name is too long for the model. This name is too long for the model. This name is too long for the model. " +
            "This name is too long for the model. This name is too long for the model. This name is too long for the model. ";

        private readonly ProductListModel[] PRODUCTS_LIST =
        {
            new ProductListModel
            {
            Id = new Guid("0d4fa150-ad80-4d46-a511-4c666166ec5a"),
            Name = "Lattitude E6440",
            Photo = "path",
            ManufacturerName = "Dell",
            CategoryName = "Graphic design"
            },
            new ProductListModel
            {
            Id = new Guid("23b3902d-7d4f-4213-9cf0-112348f56233"),
            Name = "Thinkpad L580",
            Photo = "path",
            ManufacturerName = "Lenovo",
            CategoryName = "Professional"
            }
        };

        private readonly ProductDetailModel[] PRODUCTS_DETAIL =
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
                CategoryName = "Graphic design"
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
                CategoryName = "Professional"
            }
        };

        private readonly ProductNewModel[] PRODUCTS_NEW =
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

        private ProductDetailModel[] PRODUCTS_DETAIL_EXPECTED =
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

        private readonly ProductUpdateModel PRODUCT_UPDATE = new ProductUpdateModel
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

        public GoodsControllerTests(WebApplicationFactory<Startup> fixture)
        {
            _client = fixture.CreateClient();
        }

        /*===============================    GetAll Tests    ===============================*/

        /// <summary>
        /// Try Get all goods. Shoudl return Status Code OK.
        /// </summary>
        [Fact]
        public async Task GetAll_Should_result_OK()
        {
            var response = await _client.GetAsync("api/Product");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        /// <summary>
        /// Try get all goods. Should return non-empty field in response.content
        /// </summary>
        [Fact]
        public async Task GetAll_Should_return_some_goods()
        {
            var response = await _client.GetAsync("api/Product");

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var products = JsonConvert.DeserializeObject<List<ProductListModel>>(await response.Content.ReadAsStringAsync());
            products.Should().HaveCountGreaterOrEqualTo(1);
        }

        /// <summary>
        /// Try get all goods. 
        /// Check if it returns at least 6 products.
        /// Check if the first and the last product match with model
        /// </summary>
        [Fact]
        public async Task GetAll_Should_return_first_last_and_all_the_others()
        {
            // Act
            var response = await _client.GetAsync("api/Product");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var products = JsonConvert.DeserializeObject<List<ProductListModel>>(await response.Content.ReadAsStringAsync());

            // Should return at least 6 products
            products.Should().HaveCountGreaterOrEqualTo(6);

            // First product
            products[0].Should().BeEquivalentTo(PRODUCTS_LIST[0]);
            // Last product
            products[5].Should().BeEquivalentTo(PRODUCTS_LIST[1]);
        }

        /*===============================    GetById Tests    ===============================*/

        /// <summary>
        /// Try GetById one product. Should return StatusCode.OK and response.Content should not be null
        /// </summary>
        /// <param name="index">Index of tested product. (FIRST = 0, LAST = 1)</param>
        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public async Task GetById_Should_return_something(int index)
        {
            var response = await _client.GetAsync($"api/Product/{PRODUCTS_DETAIL[index].Id}");

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var product = JsonConvert.DeserializeObject<ProductDetailModel>(await response.Content.ReadAsStringAsync());
            product.Should().NotBeNull();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public async Task GetById_Should_return_product_by_Id(int index)
        {

            var response = await _client.GetAsync($"api/Product/{PRODUCTS_DETAIL[index].Id}");

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var product = JsonConvert.DeserializeObject<ProductDetailModel>(await response.Content.ReadAsStringAsync());
            product.Should().NotBeNull();
            product.Should().BeEquivalentTo(PRODUCTS_LIST[index]);
        }

        /// <summary>
        /// Try GetById with empty id (non-existing product). Should return BadRequest (400) 
        /// </summary>
        [Fact]
        public async Task GetById_With_empty_Id()
        {
            // Act
            var response = await _client.GetAsync($"api/Product/{Guid.Empty}");

            // Assert
            response.Should().NotBeNull();
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        /*===============================    Create Tests    ===============================*/

        [Fact]
        public async Task Create_Should_return_new_ID()
        {
            // Arrange
            var newProductSerialized = JsonConvert.SerializeObject(PRODUCTS_NEW[0]);
            var stringContent = new StringContent(newProductSerialized, Encoding.UTF8, "application/json");

            // Act 
            var response = await _client.PostAsync("api/Product", stringContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var newProductGuid = JsonConvert.DeserializeObject<Guid>(await response.Content.ReadAsStringAsync());
            newProductGuid.Should().NotBeEmpty();
        }

        [Fact]
        public async Task Create_Should_create_findable_Product()
        {
            // Arrange
            var newProductSerialized = JsonConvert.SerializeObject(PRODUCTS_NEW[0]);
            var stringContent = new StringContent(newProductSerialized, Encoding.UTF8, "application/json");

            // Act 
            var response = await _client.PostAsync("api/Product", stringContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var newProductGuid = JsonConvert.DeserializeObject<Guid>(await response.Content.ReadAsStringAsync());
            newProductGuid.Should().NotBeEmpty();

            // GetById
            var response_GetById = await _client.GetAsync($"api/Product/{newProductGuid}");
            response_GetById.StatusCode.Should().Be(HttpStatusCode.OK);

            var product = JsonConvert.DeserializeObject<ProductDetailModel>(await response_GetById.Content.ReadAsStringAsync());
            product.Id.Should().NotBe(Guid.Empty);
            PRODUCTS_DETAIL_EXPECTED[0].Id = product.Id;
            product.Should().BeEquivalentTo(PRODUCTS_DETAIL_EXPECTED[0]);
        }

        /// <summary>
        /// Expect BadRequest by creating products with invalid names
        /// </summary>
        /// <param name="name">Name to cause error in creating new product</param>
        [Theory]
        [InlineData(TOOSHORTNAME)]
        [InlineData(TOOLONGNAME)]
        public async Task Create_With_invalid_name_should_return_BadRequest(string name)
        {
            // Arrange 
            PRODUCTS_NEW[1].Name = name;
            var newProductSerialized = JsonConvert.SerializeObject(PRODUCTS_NEW[1]);
            var stringContent = new StringContent(newProductSerialized, Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("api/Product", stringContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        /*===============================    Update Tests    ===============================*/

        /// <summary>
        /// Create new Product and then try update its prize to double, should return OK. Then GetById updated Product
        /// </summary>
        [Fact]
        public async Task Update_Should_update_existing_product()
        {
            // Create new product 
                // Arrange
            var newProductSerialized = JsonConvert.SerializeObject(PRODUCTS_NEW[2]);
            var stringContent_create = new StringContent(newProductSerialized, Encoding.UTF8, "application/json");

                // Act 
            var response_create = await _client.PostAsync("api/Product", stringContent_create);

                // Assert
            response_create.StatusCode.Should().Be(HttpStatusCode.OK);
            var newProductGuid = JsonConvert.DeserializeObject<Guid>(await response_create.Content.ReadAsStringAsync());
            newProductGuid.Should().NotBeEmpty();
            PRODUCT_UPDATE.Id = newProductGuid;


            // Arange
            var productToUpdateSerialized = JsonConvert.SerializeObject(PRODUCT_UPDATE);
            var stringContent = new StringContent(productToUpdateSerialized, Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PutAsync("api/Product?verison=3.0&culture=en", stringContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            
            // GetById
            var response_GetById = await _client.GetAsync($"api/Product/{PRODUCT_UPDATE.Id}");
            response_GetById.StatusCode.Should().Be(HttpStatusCode.OK);

            var product = JsonConvert.DeserializeObject<ProductDetailModel>(await response_GetById.Content.ReadAsStringAsync());
            PRODUCTS_DETAIL_EXPECTED[1].Id = product.Id;
            product.Should().BeEquivalentTo(PRODUCTS_DETAIL_EXPECTED[1]);
        }

        /*===============================    Delete Tests    ===============================*/


        /// <summary>
        /// Try delete existing product and then find it. Should return NotFound
        /// </summary>
        [Fact]
        public async Task Delete_Should_delete_product()
        {
            // Arrange - Create new product
            var newProductSerialized = JsonConvert.SerializeObject(PRODUCTS_NEW[0]);
            var stringContent = new StringContent(newProductSerialized, Encoding.UTF8, "application/json");

                // Act 
            var response_create = await _client.PostAsync("api/Product", stringContent);

                // Assert
            response_create.StatusCode.Should().Be(HttpStatusCode.OK);
            var newProductGuid = JsonConvert.DeserializeObject<Guid>(await response_create.Content.ReadAsStringAsync());
            newProductGuid.Should().NotBeEmpty();

            // Act
            var response = await _client.DeleteAsync($"api/Product/{newProductGuid}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            // GetById deleted Product -> assert NotFound
                // Act
            var response_GetById = await _client.GetAsync($"api/Category/{newProductGuid}");

            // Assert
            response_GetById.Should().NotBeNull();
            response_GetById.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        /// <summary>
        /// Try delete non-existing product. Should return BadRequest
        /// </summary>
        [Fact]
        public async Task Delete_Empty_Id()
        {
            // Arrange 
            var newProductGuid = Guid.Empty;

            // Act
            var response = await _client.DeleteAsync($"api/Product/{newProductGuid}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            //response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);  - It actually returns InternalServerError
        }
    }
}
