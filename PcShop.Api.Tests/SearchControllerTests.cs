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
using PcShop.BL.Api.Models.Evaluation;
using PcShop.BL.Api.Models.Search;
using Xunit;

namespace PcShop.Api.Tests
{
    [Collection(name: "SearchControllerTests")]
    public class SearchControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;
        private const string SearchedString = "Searched";

        private readonly ProductNewModel[] _productArray =
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

        private readonly EvaluationNewModel[] _evaluationArray =
        {
            new EvaluationNewModel
            {
                PercentEvaluation = 80,
                TextEvaluation = "Try"
            },
            new EvaluationNewModel
            {
                PercentEvaluation = 80,
                TextEvaluation = "Searched"
            },
            new EvaluationNewModel
            {
                PercentEvaluation = 80,
                TextEvaluation = "Searched and something more"
            }
        };

        private readonly ManufacturerNewModel[] _manufacturerArray =
        {
            new ManufacturerNewModel
            {
                Name = "HPcseaRchedko",
                Description = "...",
                Logo = "path",
                CountryOfOrigin = "UK"
            },
            new ManufacturerNewModel
            {
                Name = "searched",
                Description = "...",
                Logo = "path",
                CountryOfOrigin = "UK"
            },
            new ManufacturerNewModel
            {
                Name = "HPcseaasdasRchedko",
                Description = "..searched.",
                Logo = "path",
                CountryOfOrigin = "UK"
            },
            new ManufacturerNewModel
            {
                Name = "HPcseaRdddddchedko",
                Description = ".ddddddddddddddddddddd..",
                Logo = "path",
                CountryOfOrigin = "UK"
            },
        };

        public SearchControllerTests(WebApplicationFactory<Startup> fixture)
        {
            _client = fixture.CreateClient();
        }

        [Fact]
        public async Task Search_with_parameter_should_result_OK()
        {
            var response = await _client.GetAsync($"api/Search/{SearchedString}");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Search_without_parameter_should_result_NotFound()
        {
            var response = await _client.GetAsync("api/Search");

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        private void CheckFoundedProducts(SearchResultModel result)
        {
            foreach (var searchedEntity in _productArray)
            {
                if (searchedEntity.Description.ToLower().Contains(SearchedString.ToLower())
                    || searchedEntity.Name.ToLower().Contains(SearchedString.ToLower()))
                {
                    var entity = result.ProductEntities
                        .Find(p => p.Name == searchedEntity.Name
                                   && p.Description == searchedEntity.Description);
                    entity.Should().NotBe(null);
                }
            }
        }

        private void CheckFoundedEvaluations(SearchResultModel result)
        {
            foreach (var searchedEntity in _evaluationArray)
            {
                if (searchedEntity.TextEvaluation.ToLower().Contains(SearchedString.ToLower()))
                {
                    var entity = result.EvaluationEntities
                        .Find(e => e.TextEvaluation == searchedEntity.TextEvaluation);

                    entity.Should().NotBe(null);
                }
            }
        }

        private void CheckFoundedManufacturers(SearchResultModel result)
        {
            foreach (var searchedEntity in _manufacturerArray)
            {
                if (searchedEntity.Name.ToLower().Contains(SearchedString.ToLower())
                    || searchedEntity.Description.ToLower().Contains(SearchedString.ToLower())
                    || searchedEntity.CountryOfOrigin.ToLower().Contains(SearchedString.ToLower()))
                {
                    var entity = result.ManufacturerEntities
                        .Find(m => m.Name == searchedEntity.Name
                                   && m.Description == searchedEntity.Description
                                   && m.CountryOfOrigin == searchedEntity.CountryOfOrigin);

                    entity.Should().NotBe(null);
                }
            }
        }

        [Fact]
        public async Task Search_should_find_products()
        {
            foreach (var product in _productArray)
            {
                var newProductSerialized = JsonConvert.SerializeObject(product);
                var stringContent = new StringContent(newProductSerialized, Encoding.UTF8, "application/json");
                await _client.PostAsync("api/Product", stringContent);
            }

            var response = await _client.GetAsync($"api/Search/{SearchedString}");

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var result = JsonConvert.DeserializeObject<SearchResultModel>(await response.Content.ReadAsStringAsync());

            CheckFoundedProducts(result);
        }

        [Fact]
        public async Task Search_should_find_manufacturer()
        {
            foreach (var manufacturer in _manufacturerArray)
            {
                var newManufacturerSerialized = JsonConvert.SerializeObject(manufacturer);
                var stringContent = new StringContent(newManufacturerSerialized, Encoding.UTF8, "application/json");
                await _client.PostAsync("api/Manufacturer", stringContent);
            }

            var response = await _client.GetAsync($"api/Search/{SearchedString}");

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var result = JsonConvert.DeserializeObject<SearchResultModel>(await response.Content.ReadAsStringAsync());

            CheckFoundedManufacturers(result);
        }

        [Fact]
        public async Task Search_should_find_evaluation()
        {
            foreach (var evaluation in _evaluationArray)
            {
                var newManufacturerSerialized = JsonConvert.SerializeObject(evaluation);
                var stringContent = new StringContent(newManufacturerSerialized, Encoding.UTF8, "application/json");
                await _client.PostAsync("api/Manufacturer", stringContent);
            }

            var response = await _client.GetAsync($"api/Search/{SearchedString}");

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var result = JsonConvert.DeserializeObject<SearchResultModel>(await response.Content.ReadAsStringAsync());

            CheckFoundedEvaluations(result);
        }

        [Fact]
        public async Task Search_should_find_all_entities()
        {
            foreach (var product in _productArray)
            {
                var newProductSerialized = JsonConvert.SerializeObject(product);
                var stringContent = new StringContent(newProductSerialized, Encoding.UTF8, "application/json");
                await _client.PostAsync("api/Product", stringContent);
            }
            foreach (var manufacturer in _manufacturerArray)
            {
                var newManufacturerSerialized = JsonConvert.SerializeObject(manufacturer);
                var stringContent = new StringContent(newManufacturerSerialized, Encoding.UTF8, "application/json");
                await _client.PostAsync("api/Manufacturer", stringContent);
            }
            foreach (var evaluation in _evaluationArray)
            {
                var newManufacturerSerialized = JsonConvert.SerializeObject(evaluation);
                var stringContent = new StringContent(newManufacturerSerialized, Encoding.UTF8, "application/json");
                await _client.PostAsync("api/Manufacturer", stringContent);
            }

            var response = await _client.GetAsync($"api/Search/{SearchedString}");

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var result = JsonConvert.DeserializeObject<SearchResultModel>(await response.Content.ReadAsStringAsync());

            CheckFoundedProducts(result);
            CheckFoundedManufacturers(result);
            CheckFoundedEvaluations(result);
        }
    }
}
