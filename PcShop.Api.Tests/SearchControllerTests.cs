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
    [Collection(name: "SearchControllerTests")]
    public class SearchControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private HttpClient client;
        private const string SEARCHED_STRING = "Searched";

        private readonly ProductNewModel[] PRODUCTS_CONTAINS_TWO_WITH_SEARCHED_STRING =
        {
            new ProductNewModel
            {
                Name = "this is seARched",
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
                Name = "this is seArC hed",
                Photo = "path",
                Description = "searched",
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
                Name = "this is not sear_ched 46135",
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
                Name = "this is not sear ched",
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

        public SearchControllerTests(WebApplicationFactory<Startup> fixture)
        {
            client = fixture.CreateClient();
        }

        [Fact]
        public async Task Search_with_parameter_should_result_OK()
        {
            var response = await client.GetAsync($"api/Search/{SEARCHED_STRING}");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Search_without_parameter_should_result_NotFound()
        {
            var response = await client.GetAsync("api/Search");

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Search_should_find_two_products()
        {

            foreach (var product in PRODUCTS_CONTAINS_TWO_WITH_SEARCHED_STRING)
            {
                var newProductSerialized = JsonConvert.SerializeObject(product);
                var stringContent = new StringContent(newProductSerialized, Encoding.UTF8, "application/json");
                await client.PostAsync("api/Product", stringContent);
            }

            var response = await client.GetAsync($"api/Search/{SEARCHED_STRING}");

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var products = JsonConvert.DeserializeObject<List<ProductListModel>>(await response.Content.ReadAsStringAsync());

            for (int i = 0; i < 2; ++i)
            {
                bool productFound = false;
                foreach (var product in products)
                {
                    if (product.Name == PRODUCTS_CONTAINS_TWO_WITH_SEARCHED_STRING[i].Name
                        && product.Description == PRODUCTS_CONTAINS_TWO_WITH_SEARCHED_STRING[i].Description)
                        productFound = true;
                }
                productFound.Should().BeTrue();
            }
        }

        [Fact]
        public async Task Search_should_find_manufacturer()
        {
            ManufacturerNewModel newManufacturer = new ManufacturerNewModel
            {
                Name = "HPcseaRchedko",
                Description = "...",
                Logo = "path",
                CountryOfOrigin = "UK"
            };
            
            var newManufacturerSerialized = JsonConvert.SerializeObject(newManufacturer);
            var stringContent = new StringContent(newManufacturerSerialized, Encoding.UTF8, "application/json");
            await client.PostAsync("api/Manufacturer", stringContent);
            
            var response = await client.GetAsync($"api/Search/{SEARCHED_STRING}");

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var products = JsonConvert.DeserializeObject<List<ManufacturerListModel>>(await response.Content.ReadAsStringAsync());
            
            products[0].Name.Should().Be(newManufacturer.Name);
            products[0].Description.Should().Be(newManufacturer.Description);
            products[0].CountryOfOrigin.Should().Be(newManufacturer.CountryOfOrigin);
        }
    }
}
