/* File:        CategoryControllerTests.cs
 * 
 * Solution:    PcShop
 * Project:     PcShop.Api.Test
 *
 * Team:        Team0011
 * Author:      Vojtech Vlach
 * Login:       xvlach22
 * Date:        30.10.2020
 * 
 * Description: This file contains API tests for CategoryController in PcShop.Api.
 *              Tests all main 4 methods (GET, PUT, POST, DELETE)
 * 
 * Installed NuGet packages: Microsoft.AspNetCore.Mvc.Testing, FluentAssertions
 * 
 * TODO:    1) Upravit ten jeden test, který padá, když se spouštěj všechny, ale passne, 
 *          když se spustí samostatně (nějaký update to byl myslím). Tak aby ho ostatní 
 *          testy neblokovali (takže možná spíš upravit ty delete testy... ??)
 *          Je to: (GetById_Should_return_Category_by_Id)
 */

using Microsoft.AspNetCore.Mvc.Testing;
using PcShop.BL.Api.Models.Category;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using System.Net;
using System;
using PcShop.BL.Api.Models.Product;
using Xunit;

namespace PcShop.Api.Tests
{
    [Collection(name: "CategoryControllerTests")]
    public class CategoryControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private HttpClient _client;
        // This should go to some sort of shared memory between tests: 
        private const string TOOSHORTNAME = "c";
        private const string TOOLONGNAME = "This name is too long for the model. This name is too long for the model. This name is too long for the model. " +
            "This name is too long for the model. This name is too long for the model. This name is too long for the model. ";
        private const string CATEGORYID_1 = "fabde0cd-eefe-443f-baf6-3d96cc2cbf2e";
        private const string CATEGORYID_2 = "23b3902d-7d4f-4213-9cf0-112348f56238";
        private readonly CategoryUpdateModel[] CATEGORIES_UPDATE =
        {
            new CategoryUpdateModel
            {
                Id = new Guid(CATEGORYID_1),
                Name = "For gamers",
                Product = new List<ProductOnlyIdUpdateModel>()
            },
            
            new CategoryUpdateModel 
            {
                Id = new Guid(CATEGORYID_2),
                Name = "Home Office",
                Product = new List<ProductOnlyIdUpdateModel>()
            }
        };
        // All this

        public CategoryControllerTests(WebApplicationFactory<Startup> fixture)
        {
            _client = fixture.CreateClient();
            var newId = Guid.Empty;        
        }

        /*===============================    GetAll Tests    ===============================*/

        /// <summary>
        /// Try Get all categories. Shoudl return Status Code OK.
        /// </summary>
        [Fact]
        public async Task GetAll_Should_result_OK()
        {
            var response = await _client.GetAsync("api/Category");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        /// <summary>
        /// Try get all categories. Should return non-empty field in response.content
        /// </summary>
        [Fact]
        public async Task GetAll_Should_return_some_categories()
        {
            var response = await _client.GetAsync("api/Category");

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var categories = JsonConvert.DeserializeObject<List<CategoryListModel>>(await response.Content.ReadAsStringAsync());
            categories.Should().HaveCountGreaterOrEqualTo(1);
        }

        /// <summary>
        /// Try get all categories. Check if the first two equals model.
        /// </summary>
        [Fact]
        public async Task GetAll_Should_return_Proffessional_and_Graphic_design()
        {
            // Act
            var response = await _client.GetAsync("api/Category");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var categories = JsonConvert.DeserializeObject<List<CategoryListModel>>(await response.Content.ReadAsStringAsync());

            categories.Should().HaveCountGreaterOrEqualTo(2);

            categories[0].Name.Should().Be("Professional");
            categories[0].Id.Should().Be(CATEGORYID_1);

            categories[1].Name.Should().Be("Graphic design");
            categories[1].Id.Should().Be(CATEGORYID_2);
        }

        /*===============================    GetById Tests    ===============================*/

        /// <summary>
        /// Try GetById one category. Should return StatusCode.OK and response.Content should not be null
        /// </summary>
        /// <param name="Id">ID of wanted category</param>
        [Theory]
        [InlineData(CATEGORYID_1)]
        [InlineData(CATEGORYID_2)]
        public async Task GetById_Should_return_something(string Id)
        {
            var response = await _client.GetAsync($"api/Category/{Id}");

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var category = JsonConvert.DeserializeObject<CategoryDetailModel>(await response.Content.ReadAsStringAsync());
            category.Should().NotBeNull();
        }


        /// <summary>
        /// Try GetById with empty ID (non-existing category). Should return BadRequest (400) 
        /// TODO - Test is failing because program throws InternalServerError Exception and two other errors.
        /// </summary>
        [Fact]
        public async Task GetById_With_empty_Id()
        {
            // Arrange 
            string EmptyId = Guid.Empty.ToString();

            // Act
            var response = await _client.GetAsync($"api/Category/{EmptyId}");

            // Assert
            response.Should().NotBeNull();
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        /*===============================    Create Tests    ===============================*/

        [Fact]
        public async Task Create_Should_return_new_ID()
        {
            // Arrange
            var newCategory = new CategoryNewModel { Name = "Do 10 000,-" };

            var newCategorySerialized = JsonConvert.SerializeObject(newCategory);
            var stringContent = new StringContent(newCategorySerialized, Encoding.UTF8, "application/json");

            // Act 
            var response = await _client.PostAsync("api/Category", stringContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var newCategoryGuid = JsonConvert.DeserializeObject<Guid>(await response.Content.ReadAsStringAsync());
            newCategoryGuid.Should().NotBeEmpty();
        }

        [Fact]
        public async Task Create_Should_create_findable_category()
        {
            // Arrange
            const string newCategoryName = "Do 10 000,-";
            var newCategory = new CategoryNewModel { Name = newCategoryName };

            var newCategorySerialized = JsonConvert.SerializeObject(newCategory);
            var stringContent = new StringContent(newCategorySerialized, Encoding.UTF8, "application/json");

            // Act 
            var response = await _client.PostAsync("api/Category", stringContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var newCategoryGuid = JsonConvert.DeserializeObject<Guid>(await response.Content.ReadAsStringAsync());
            newCategoryGuid.Should().NotBeEmpty();

            // GetById
            var response_GetById = await _client.GetAsync($"api/Category/{newCategoryGuid}");
            response_GetById.StatusCode.Should().Be(HttpStatusCode.OK);

            var category = JsonConvert.DeserializeObject<CategoryDetailModel>(await response_GetById.Content.ReadAsStringAsync());
            category.Name.Should().Be(newCategoryName);
            category.Id.Should().Be(newCategoryGuid);
        }

        [Fact]
        public async Task Create_Should_create_2_categories_with_unique_IDs()
        {
            // Arrange 
            var newCategory_1 = new CategoryNewModel{ Name = "do 100 000"};
            var newCategory_2 = new CategoryNewModel { Name = "nad 100 000" };
            
            var newCategorySerialized_1 = JsonConvert.SerializeObject(newCategory_1);
            var newCategorySerialized_2 = JsonConvert.SerializeObject(newCategory_2);

            var stringContent_1 = new StringContent(newCategorySerialized_1, Encoding.UTF8, "application/json");
            var stringContent_2 = new StringContent(newCategorySerialized_2, Encoding.UTF8, "application/json");

            // Act 
            var response_1 = await _client.PostAsync("api/Category", stringContent_1);
            var response_2 = await _client.PostAsync("api/Category", stringContent_2);

            // Assert
            response_1.StatusCode.Should().Be(HttpStatusCode.OK);
            response_2.StatusCode.Should().Be(HttpStatusCode.OK);
            var newCategoryGuid_1 = JsonConvert.DeserializeObject<Guid>(await response_1.Content.ReadAsStringAsync());
            var newCategoryGuid_2 = JsonConvert.DeserializeObject<Guid>(await response_2.Content.ReadAsStringAsync());
            newCategoryGuid_1.Should().NotBeEmpty();
            newCategoryGuid_2.Should().NotBeEmpty();

            newCategoryGuid_1.Should().NotBe(newCategoryGuid_2);
        }

        [Theory]
        [InlineData(TOOSHORTNAME)]
        [InlineData(TOOLONGNAME)]
        public async Task Create_With_invalid_name_should_return_BadRequest(string name)
        {
            var newCategory = new CategoryNewModel
            {
                Name = name
            };

            var newCategorytSerialized = JsonConvert.SerializeObject(newCategory);
            var stringContent = new StringContent(newCategorytSerialized, Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("api/Category", stringContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            //response.StatusCode.Should().Be(HttpStatusCode.NotFound);  - program actually returns NotFound (404)
        }

        /*===============================    Update Tests    ===============================*/

        /// <summary>
        /// Try update existing category, should return OK. Then GetById updated category
        /// !!! TODO !!! Test neprochází, protože CategoryUpdateModel má člen IList Product a vyžaduje ho pro updatování dané kategorie
        /// </summary>
        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public async Task Update_Should_update_existing_category(int index)
        {
            var categoryToUpdateSerialized = JsonConvert.SerializeObject(CATEGORIES_UPDATE[index]);
            var stringContent = new StringContent(categoryToUpdateSerialized, Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PutAsync("api/Category?verison=3.0&culture=en", stringContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            // GetById
            var response_GetById = await _client.GetAsync($"api/Category/{CATEGORIES_UPDATE[index].Id}");
            response_GetById.StatusCode.Should().Be(HttpStatusCode.OK);

            var category = JsonConvert.DeserializeObject<CategoryDetailModel>(await response_GetById.Content.ReadAsStringAsync());
            category.Should().BeEquivalentTo(CATEGORIES_UPDATE[index]);
        }

        [Fact]
        public async Task Update_Empty_Id()
        {
            // Arrange 
            var categoryToUpdate = new CategoryUpdateModel
            {
                Id = Guid.Empty,
                Name = "HomeOffice",
                Product = new List<ProductOnlyIdUpdateModel>()
            };

            var categoryToUpdateSerialized = JsonConvert.SerializeObject(categoryToUpdate);
            var stringContent = new StringContent(categoryToUpdateSerialized, Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PutAsync("api/Category?verison=3.0&culture=en", stringContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        /// <summary>
        /// Try update existing category with invalid name. Should return BadRequest (400)
        /// !!! TODO !!! Test neprochází, protože CategoryUpdateModel má člen IList Product a vyžaduje ho pro updatování dané kategorie
        /// !!!TODO !!! API hází 400 (nejspíš protože nemůžu aktualizovat prvek, když nemá dostatek dat, chybí: IList<...> Product
        /// </summary>
        [Theory]
        [InlineData(TOOSHORTNAME)]
        [InlineData(TOOLONGNAME)]
        public async Task Update_Valid_Id_with_invalid_name(string newName)
        {
            // Check if Category exists
            var response_GetById = await _client.GetAsync($"api/Category/{CATEGORYID_1}");
            response_GetById.StatusCode.Should().Be(HttpStatusCode.OK);

            // Arrange 
            var categoryToUpdate = new CategoryUpdateModel
            {
                Id = new Guid(CATEGORYID_1),
                Name = newName,
                Product = new List<ProductOnlyIdUpdateModel>()
            };

            var categoryToUpdateSerialized = JsonConvert.SerializeObject(categoryToUpdate);
            var stringContent = new StringContent(categoryToUpdateSerialized, Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PutAsync("api/Category?verison=3.0&culture=en", stringContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        /*===============================    Delete Tests    ===============================*/

        /// <summary>
        /// Try delete existing category and then find it. Should return NotFound.
        /// TODO - Test padá, protože program padá při vyhledání neexistujícího CategoryID
        /// </summary>
        [Fact]
        public async Task Delete_Should_delete_category()
        {
            // Arrange - Create new Category
            var newCategory = new CategoryNewModel { Name = "Do 10 000,-" };

            var newCategorySerialized = JsonConvert.SerializeObject(newCategory);
            var stringContent = new StringContent(newCategorySerialized, Encoding.UTF8, "application/json");

                // Act 
            var response_create = await _client.PostAsync("api/Category", stringContent);

                // Assert
            response_create.StatusCode.Should().Be(HttpStatusCode.OK);
            var newCategoryGuid = JsonConvert.DeserializeObject<Guid>(await response_create.Content.ReadAsStringAsync());
            newCategoryGuid.Should().NotBeEmpty();

            // Act
            var response = await _client.DeleteAsync($"api/Category/{newCategoryGuid}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            
            // GetById -> assert BadRequest
                // Act
            var response_GetById = await _client.GetAsync($"api/Category/{newCategoryGuid}");

                // Assert
            response_GetById.Should().NotBeNull();
            response_GetById.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        /// <summary>
        /// Try delete non-existing category. Should return BadRequest
        /// TODO - test padá, protože program hází InternalServerError místo BadRequest
        /// </summary>
        [Fact]
        public async Task Delete_Empty_Id()
        {
            // Arrange 
            var newCategoryGuid = Guid.Empty;

            // Act
            var response = await _client.DeleteAsync($"api/Category/{newCategoryGuid}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            //response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);  - It actually returns InternalServerError
        }
    }
}
