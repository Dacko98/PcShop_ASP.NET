﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using PcShop.BL.Api.Models.Manufacturer;
using PcShop.BL.Api.Models.Product;
using Xunit;

namespace PcShop.Api.Tests
{
    [Collection(name: "SearchControllerTests")]
    public class SearchControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private HttpClient _client;
        private const string _searchedString = "Searched";

        private readonly ProductNewModel[] _productsContainsTwoWithSearchedString =
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
            _client = fixture.CreateClient();
        }

        /// <summary>
        /// Search request with parameter should return status code OK.
        /// </summary>
        [Fact]
        public async Task Search_Should_result_OK()
        {
            var response = await _client.GetAsync($"api/Search/{_searchedString}");

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

        /// <summary>
        /// Search request should find first two products from products_ContainsTwoWithSearchedString
        /// </summary>
        [Fact]
        public async Task Search_Should_find_two_products()
        {

            foreach (var product in _productsContainsTwoWithSearchedString)
            {
                var newProductSerialized = JsonConvert.SerializeObject(product);
                var stringContent = new StringContent(newProductSerialized, Encoding.UTF8, "application/json");
                await _client.PostAsync("api/Product", stringContent);
            }

            var response = await _client.GetAsync($"api/Search/{_searchedString}");

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var products = JsonConvert.DeserializeObject<List<ProductListModel>>(await response.Content.ReadAsStringAsync());

            for (int i = 0; i < 2; ++i)
            {
                bool productFound = false;
                foreach (var product in products)
                {
                    if (product.Name == _productsContainsTwoWithSearchedString[i].Name
                        && product.Description == _productsContainsTwoWithSearchedString[i].Description)
                        productFound = true;
                }
                productFound.Should().BeTrue();
            }
        }

        /// <summary>
        /// Search request should find manufacturer
        /// </summary>
        [Fact]
        public async Task Search_Should_find_manufacturer()
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
            await _client.PostAsync("api/Manufacturer", stringContent);
            
            var response = await _client.GetAsync($"api/Search/{_searchedString}");

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var products = JsonConvert.DeserializeObject<List<ManufacturerListModel>>(await response.Content.ReadAsStringAsync());
            
            products[0].Name.Should().Be(newManufacturer.Name);
            products[0].Description.Should().Be(newManufacturer.Description);
            products[0].CountryOfOrigin.Should().Be(newManufacturer.CountryOfOrigin);
        }

    }
}
