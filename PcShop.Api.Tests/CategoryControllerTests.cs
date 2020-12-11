using Microsoft.AspNetCore.Mvc.Testing;
using PcShop.BL.Api.Models.Category;
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
    [Collection(name: "CategoryControllerTests")]
    public class CategoryControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private HttpClient client;

        private const string TOO_SHORT_NAME = "c";
        private const string TOO_LONG_NAME = "This name is too long for the model. This name is too long for the model. This name is too long for the model. " +
            "This name is too long for the model. This name is too long for the model. This name is too long for the model. ";
        private const string CATEGORY_ID_1 = "fabde0cd-eefe-443f-baf6-3d96cc2cbf2e";
        private const string CATEGORY_ID_2 = "23b3902d-7d4f-4213-9cf0-112348f56238";

        private readonly CategoryUpdateModel[] CATEGORIES_UPDATE =
        {
            new CategoryUpdateModel
            {
                Id = new Guid(CATEGORY_ID_1),
                Name = "For gamers",
                Product = new List<ProductOnlyIdUpdateModel>()
            },
            
            new CategoryUpdateModel 
            {
                Id = new Guid(CATEGORY_ID_2),
                Name = "Home Office",
                Product = new List<ProductOnlyIdUpdateModel>()
            }
        };

        public CategoryControllerTests(WebApplicationFactory<Startup> fixture)
        {
            client = fixture.CreateClient();
            var newId = Guid.Empty;        
        }

        /*===============================    GetAll Tests    ===============================*/

        [Fact]
        public async Task GetAll_should_result_OK()
        {
            var response = await client.GetAsync("api/Category");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetAll_should_return_some_categories()
        {
            var response = await client.GetAsync("api/Category");

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var categories = JsonConvert.DeserializeObject<List<CategoryListModel>>(await response.Content.ReadAsStringAsync());
            categories.Should().HaveCountGreaterOrEqualTo(1);
        }

        [Fact]
        public async Task GetAll_should_return_Proffessional_and_Graphic_design()
        {
            // Act
            var response = await client.GetAsync("api/Category");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var categories = JsonConvert.DeserializeObject<List<CategoryListModel>>(await response.Content.ReadAsStringAsync());

            categories.Should().HaveCountGreaterOrEqualTo(2);

            categories[0].Name.Should().Be("Professional");
            categories[0].Id.Should().Be(CATEGORY_ID_1);

            categories[1].Name.Should().Be("Graphic design");
            categories[1].Id.Should().Be(CATEGORY_ID_2);
        }

        /*===============================    GetById Tests    ===============================*/

        [Theory]
        [InlineData(CATEGORY_ID_1)]
        [InlineData(CATEGORY_ID_2)]
        public async Task GetById_should_return_something(string wantedId)
        {
            var response = await client.GetAsync($"api/Category/{wantedId}");

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var category = JsonConvert.DeserializeObject<CategoryDetailModel>(await response.Content.ReadAsStringAsync());
            category.Should().NotBeNull();
        }

        [Fact]
        public async Task GetById_with_empty_Id_should_return_NotFound()
        {
            // Arrange 
            string EmptyId = Guid.Empty.ToString();

            // Act
            var response = await client.GetAsync($"api/Category/{EmptyId}");

            // Assert
            response.Should().NotBeNull();
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        /*===============================    Create Tests    ===============================*/

        [Fact]
        public async Task Create_should_return_new_ID()
        {
            // Arrange
            var newCategory = new CategoryNewModel { Name = "Do 10 000,-" };

            var newCategorySerialized = JsonConvert.SerializeObject(newCategory);
            var stringContent = new StringContent(newCategorySerialized, Encoding.UTF8, "application/json");

            // Act 
            var response = await client.PostAsync("api/Category", stringContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var newCategoryGuid = JsonConvert.DeserializeObject<Guid>(await response.Content.ReadAsStringAsync());
            newCategoryGuid.Should().NotBeEmpty();
        }

        [Fact]
        public async Task Create_should_create_findable_category()
        {
            // Arrange
            const string newCategoryName = "Do 10 000,-";
            var newCategory = new CategoryNewModel { Name = newCategoryName };

            var newCategorySerialized = JsonConvert.SerializeObject(newCategory);
            var stringContent = new StringContent(newCategorySerialized, Encoding.UTF8, "application/json");

            // Act 
            var response = await client.PostAsync("api/Category", stringContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var newCategoryGuid = JsonConvert.DeserializeObject<Guid>(await response.Content.ReadAsStringAsync());
            newCategoryGuid.Should().NotBeEmpty();

            // GetById
            var response_GetById = await client.GetAsync($"api/Category/{newCategoryGuid}");
            response_GetById.StatusCode.Should().Be(HttpStatusCode.OK);

            var category = JsonConvert.DeserializeObject<CategoryDetailModel>(await response_GetById.Content.ReadAsStringAsync());
            category.Name.Should().Be(newCategoryName);
            category.Id.Should().Be(newCategoryGuid);
        }

        [Fact]
        public async Task Create_should_create_2_categories_with_unique_IDs()
        {
            // Arrange 
            var newCategory_1 = new CategoryNewModel{ Name = "do 100 000"};
            var newCategory_2 = new CategoryNewModel { Name = "nad 100 000" };
            
            var newCategorySerialized_1 = JsonConvert.SerializeObject(newCategory_1);
            var newCategorySerialized_2 = JsonConvert.SerializeObject(newCategory_2);

            var stringContent_1 = new StringContent(newCategorySerialized_1, Encoding.UTF8, "application/json");
            var stringContent_2 = new StringContent(newCategorySerialized_2, Encoding.UTF8, "application/json");

            // Act 
            var response_1 = await client.PostAsync("api/Category", stringContent_1);
            var response_2 = await client.PostAsync("api/Category", stringContent_2);

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
        [InlineData(TOO_SHORT_NAME)]
        [InlineData(TOO_LONG_NAME)]
        public async Task Create_with_invalid_name_should_return_BadRequest(string name)
        {
            var newCategory = new CategoryNewModel
            {
                Name = name
            };

            var newCategorytSerialized = JsonConvert.SerializeObject(newCategory);
            var stringContent = new StringContent(newCategorytSerialized, Encoding.UTF8, "application/json");

            // Act
            var response = await client.PostAsync("api/Category", stringContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        /*===============================    Update Tests    ===============================*/

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public async Task Update_Should_update_existing_category(int index)
        {
            var categoryToUpdateSerialized = JsonConvert.SerializeObject(CATEGORIES_UPDATE[index]);
            var stringContent = new StringContent(categoryToUpdateSerialized, Encoding.UTF8, "application/json");

            // Act
            var response = await client.PutAsync("api/Category?verison=3.0&culture=en", stringContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            // GetById check
            var response_GetById = await client.GetAsync($"api/Category/{CATEGORIES_UPDATE[index].Id}");
            response_GetById.StatusCode.Should().Be(HttpStatusCode.OK);

            var category = JsonConvert.DeserializeObject<CategoryDetailModel>(await response_GetById.Content.ReadAsStringAsync());
            category.Should().BeEquivalentTo(CATEGORIES_UPDATE[index]);
        }

        [Fact]
        public async Task Update_empty_Id()
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
            var response = await client.PutAsync("api/Category?verison=3.0&culture=en", stringContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Theory]
        [InlineData(TOO_SHORT_NAME)]
        [InlineData(TOO_LONG_NAME)]
        public async Task Update_valid_Id_with_invalid_name_should_return_BadRequest(string newName)
        {
            // Check if Category exists
            var response_GetById = await client.GetAsync($"api/Category/{CATEGORY_ID_1}");
            response_GetById.StatusCode.Should().Be(HttpStatusCode.OK);

            // Arrange 
            var categoryToUpdate = new CategoryUpdateModel
            {
                Id = new Guid(CATEGORY_ID_1),
                Name = newName,
                Product = new List<ProductOnlyIdUpdateModel>()
            };

            var categoryToUpdateSerialized = JsonConvert.SerializeObject(categoryToUpdate);
            var stringContent = new StringContent(categoryToUpdateSerialized, Encoding.UTF8, "application/json");

            // Act
            var response = await client.PutAsync("api/Category?verison=3.0&culture=en", stringContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        /*===============================    Delete Tests    ===============================*/

        [Fact]
        public async Task Delete_existing_category_then_find_it_should_return_NotFound()
        {
            // Arrange - Create new Category
            var newCategory = new CategoryNewModel { Name = "Do 10 000,-" };

            var newCategorySerialized = JsonConvert.SerializeObject(newCategory);
            var stringContent = new StringContent(newCategorySerialized, Encoding.UTF8, "application/json");

                // Act 
            var response_create = await client.PostAsync("api/Category", stringContent);

                // Assert
            response_create.StatusCode.Should().Be(HttpStatusCode.OK);
            var newCategoryGuid = JsonConvert.DeserializeObject<Guid>(await response_create.Content.ReadAsStringAsync());
            newCategoryGuid.Should().NotBeEmpty();

            // Act
            var response = await client.DeleteAsync($"api/Category/{newCategoryGuid}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            
            // GetById -> assert BadRequest
                // Act
            var response_GetById = await client.GetAsync($"api/Category/{newCategoryGuid}");

                // Assert
            response_GetById.Should().NotBeNull();
            response_GetById.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Delete_empty_Id_should_return_BedRequest()
        {
            // Arrange 
            var newCategoryGuid = Guid.Empty;

            // Act
            var response = await client.DeleteAsync($"api/Category/{newCategoryGuid}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
