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
        private readonly HttpClient _client;

        private const string TooShortName = "c";
        private const string TooLongName = "This name is too long for the model. This name is too long for the model. This name is too long for the model. " +
            "This name is too long for the model. This name is too long for the model. This name is too long for the model. ";
        private const string CategoryId1 = "fabde0cd-eefe-443f-baf6-3d96cc2cbf2e";
        private const string CategoryId2 = "23b3902d-7d4f-4213-9cf0-112348f56238";

        private readonly CategoryUpdateModel[] _categoriesUpdate =
        {
            new CategoryUpdateModel
            {
                Id = new Guid(CategoryId1),
                Name = "For gamers",
                Product = new List<ProductOnlyIdUpdateModel>()
            },
            
            new CategoryUpdateModel 
            {
                Id = new Guid(CategoryId2),
                Name = "Home Office",
                Product = new List<ProductOnlyIdUpdateModel>()
            }
        };

        public CategoryControllerTests(WebApplicationFactory<Startup> fixture)
        {
            _client = fixture.CreateClient();
        }

        /*===============================    GetAll Tests    ===============================*/

        [Fact]
        public async Task GetAll_should_result_OK()
        {
            var response = await _client.GetAsync("api/Category");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetAll_should_return_some_categories()
        {
            var response = await _client.GetAsync("api/Category");

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var categories = JsonConvert.DeserializeObject<List<CategoryListModel>>(await response.Content.ReadAsStringAsync());
            categories.Should().HaveCountGreaterOrEqualTo(1);
        }

        [Fact]
        public async Task GetAll_should_return_Proffessional_and_Graphic_design()
        {
            // Act
            var response = await _client.GetAsync("api/Category");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var categories = JsonConvert.DeserializeObject<List<CategoryListModel>>(await response.Content.ReadAsStringAsync());

            categories.Should().HaveCountGreaterOrEqualTo(2);

            categories[0].Name.Should().Be("Professional");
            categories[0].Id.Should().Be(CategoryId1);

            categories[1].Name.Should().Be("Graphic design");
            categories[1].Id.Should().Be(CategoryId2);
        }

        /*===============================    GetById Tests    ===============================*/

        [Theory]
        [InlineData(CategoryId1)]
        [InlineData(CategoryId2)]
        public async Task GetById_should_return_something(string wantedId)
        {
            var response = await _client.GetAsync($"api/Category/{wantedId}");

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var category = JsonConvert.DeserializeObject<CategoryDetailModel>(await response.Content.ReadAsStringAsync());
            category.Should().NotBeNull();
        }

        [Fact]
        public async Task GetById_with_empty_Id_should_return_NotFound()
        {
            // Arrange 
            string emptyId = Guid.Empty.ToString();

            // Act
            var response = await _client.GetAsync($"api/Category/{emptyId}");

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
            var response = await _client.PostAsync("api/Category", stringContent);

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
            var response = await _client.PostAsync("api/Category", stringContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var newCategoryGuid = JsonConvert.DeserializeObject<Guid>(await response.Content.ReadAsStringAsync());
            newCategoryGuid.Should().NotBeEmpty();

            var responseGetById = await _client.GetAsync($"api/Category/{newCategoryGuid}");
            var category = JsonConvert.DeserializeObject<CategoryDetailModel>(await responseGetById.Content.ReadAsStringAsync());
            category.Name.Should().Be(newCategoryName);
            category.Id.Should().Be(newCategoryGuid);
        }

        [Fact]
        public async Task Create_should_create_2_categories_with_unique_IDs()
        {
            // Arrange 
            var newCategory1 = new CategoryNewModel{ Name = "do 100 000"};
            var newCategory2 = new CategoryNewModel { Name = "nad 100 000" };
            
            var newCategorySerialized1 = JsonConvert.SerializeObject(newCategory1);
            var newCategorySerialized2 = JsonConvert.SerializeObject(newCategory2);

            var stringContent1 = new StringContent(newCategorySerialized1, Encoding.UTF8, "application/json");
            var stringContent2 = new StringContent(newCategorySerialized2, Encoding.UTF8, "application/json");

            // Act 
            var response1 = await _client.PostAsync("api/Category", stringContent1);
            var response2 = await _client.PostAsync("api/Category", stringContent2);

            // Assert
            response1.StatusCode.Should().Be(HttpStatusCode.OK);
            response2.StatusCode.Should().Be(HttpStatusCode.OK);
            var newCategoryGuid1 = JsonConvert.DeserializeObject<Guid>(await response1.Content.ReadAsStringAsync());
            var newCategoryGuid2 = JsonConvert.DeserializeObject<Guid>(await response2.Content.ReadAsStringAsync());
            newCategoryGuid1.Should().NotBeEmpty();
            newCategoryGuid2.Should().NotBeEmpty();

            newCategoryGuid1.Should().NotBe(newCategoryGuid2);
        }

        [Theory]
        [InlineData(TooShortName)]
        [InlineData(TooLongName)]
        public async Task Create_with_invalid_name_should_return_BadRequest(string name)
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
        }

        /*===============================    Update Tests    ===============================*/

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public async Task Update_Should_update_existing_category(int index)
        {
            // Arrange
            var categoryToUpdateSerialized = JsonConvert.SerializeObject(_categoriesUpdate[index]);
            var stringContent = new StringContent(categoryToUpdateSerialized, Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PutAsync("api/Category?verison=3.0&culture=en", stringContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var responseGetById = await _client.GetAsync($"api/Category/{_categoriesUpdate[index].Id}");
            var category = JsonConvert.DeserializeObject<CategoryDetailModel>(await responseGetById.Content.ReadAsStringAsync());
            category.Should().BeEquivalentTo(_categoriesUpdate[index]);
        }

        [Fact]
        public async Task Update_empty_Id_should_return_NotFound()
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

        [Theory]
        [InlineData(TooShortName)]
        [InlineData(TooLongName)]
        public async Task Update_valid_Id_with_invalid_name_should_return_BadRequest(string newName)
        {
            // Check if Category exists
            var responseGetById = await _client.GetAsync($"api/Category/{CategoryId1}");
            responseGetById.StatusCode.Should().Be(HttpStatusCode.OK);

            // Arrange 
            var categoryToUpdate = new CategoryUpdateModel
            {
                Id = new Guid(CategoryId1),
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

        [Fact]
        public async Task Delete_existing_category_then_find_it_should_return_NotFound()
        {
            // Arrange - Create new category
            var newCategory = new CategoryNewModel { Name = "Do 10 000,-" };
            var newCategorySerialized = JsonConvert.SerializeObject(newCategory);
            var stringContent = new StringContent(newCategorySerialized, Encoding.UTF8, "application/json");
            var responseCreate = await _client.PostAsync("api/Category", stringContent);
            var newCategoryGuid = JsonConvert.DeserializeObject<Guid>(await responseCreate.Content.ReadAsStringAsync());

            // Act
            var response = await _client.DeleteAsync($"api/Category/{newCategoryGuid}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            
            var responseGetById = await _client.GetAsync($"api/Category/{newCategoryGuid}");
            responseGetById.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Delete_empty_Id_should_return_BedRequest()
        {
            // Arrange 
            var newCategoryGuid = Guid.Empty;

            // Act
            var response = await _client.DeleteAsync($"api/Category/{newCategoryGuid}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
