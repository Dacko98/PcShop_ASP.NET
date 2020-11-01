using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using PcShop.BL.Api.Models.Category;
using PcShop.BL.Api.Models.Interfaces;
using PcShop.BL.Api.Models.Product;
using Xunit;

namespace PcShop.Api.Tests
{
    [Collection(name: "SearchControllerTests")]
    public class SearchControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private HttpClient _client;
        private const string _searchedString = "Searched";

        private readonly ProductNewModel[] products_ContainsTwoWithSearchedString =
        {
            new ProductNewModel
            {
                Name = "this is seArChed",
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
                Name = "this is not seArC hed",
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
            _client = fixture.CreateClient();
        }

        /// <summary>
        /// Search request with parameter should return status code OK.
        /// </summary>
        [Fact]
        public async Task Search_Should_result_OK()
        {
            var response = await _client.GetAsync("api/Search/{searchedString}");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        /// <summary>
        /// Search request without parameter should return status code NotFound.
        /// </summary>
        [Fact]
        public async Task Search_Should_result_NotFound()
        {
            var response = await _client.GetAsync("api/Search");

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
