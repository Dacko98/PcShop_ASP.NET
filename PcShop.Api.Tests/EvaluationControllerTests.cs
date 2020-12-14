using Microsoft.AspNetCore.Mvc.Testing;
using PcShop.BL.Api.Models.Evaluation;
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
    [Collection(name: "EvaluationControllerTests")]
    public class EvaluationControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        private readonly EvaluationListModel[] _evaluationsListModel =
        {
            new EvaluationListModel()
            {
                Id = new Guid("df935095-8709-4040-a2bb-b6f97cb416dc"),
                TextEvaluation = "Good",
                PercentEvaluation = 80,
                ProductName = "Lattitude E6440",
            },
            new EvaluationListModel()
            {
                Id = new Guid("23b3902d-7d4f-4213-9cf0-112348f56230"),
                TextEvaluation = "Broke after one month",
                PercentEvaluation = 0,
                ProductName = "Thinkpad T560",
            }
        };

        private readonly EvaluationDetailModel[] _evaluationsDetailModel =
        {
            new EvaluationDetailModel
            {
                Id = new Guid("df935095-8709-4040-a2bb-b6f97cb416dc"),
                TextEvaluation = "Good",
                PercentEvaluation = 80,
                ProductId = new Guid("0d4fa150-ad80-4d46-a511-4c666166ec5a"),
                ProductName = "Lattitude E6440"
            },
            new EvaluationDetailModel
            {
                Id = new Guid("23b3902d-7d4f-4213-9cf0-112348f56230"),
                TextEvaluation = "Broke after one month",
                PercentEvaluation = 0,
                ProductId = new Guid("23b3902d-7d4f-4213-9cf0-112348f56244"),
                ProductName = "Thinkpad T560"
            }
        };

        private readonly EvaluationNewModel[] _evaluationsNewModel =
        {
            new EvaluationNewModel
            {
                TextEvaluation = "Soooo bad",
                PercentEvaluation = 1,
                ProductId = new Guid("0d4fa150-ad80-4d46-a511-4c666166ec5a"),
            },
            new EvaluationNewModel
            {
                TextEvaluation = "Very very bad",
                PercentEvaluation = 99,
                ProductId =  new Guid("23b3902d-7d4f-4213-9cf0-112348f56282"),
            },
            new EvaluationNewModel
            {
                TextEvaluation = "Very very good",
                PercentEvaluation = 50,
                ProductId = new Guid("df935095-8709-4040-a2bb-b6f97cb416bb")
            }
        };

        private readonly EvaluationDetailModel[] _evalutaionsExpectedDetailModel =
        {
            new EvaluationDetailModel
            {
                Id = new Guid("23b3902d-7d4f-4213-9cf0-112348f56240"),
                TextEvaluation = "Soooo bad",
                PercentEvaluation = 1,
                ProductId = new Guid("0d4fa150-ad80-4d46-a511-4c666166ec5a"),
                ProductName =  "Lattitude E6440"
            },
            new EvaluationDetailModel
            {
                TextEvaluation = "Very very",
                PercentEvaluation = 100,
                ProductId =  new Guid("df935095-8709-4040-a2bb-b6f97cb416bb"),
                ProductName = "XPS 6300",
                Id =  Guid.Empty

            }
        };

        private readonly EvaluationUpdateModel _evaluationUpdateModel = new EvaluationUpdateModel
        {
            TextEvaluation = "Very very",
            PercentEvaluation = 100,
            ProductId = new Guid("df935095-8709-4040-a2bb-b6f97cb416bb"),
            Id = Guid.Empty
        };

        public EvaluationControllerTests(WebApplicationFactory<Startup> fixture)
        {
            _client = fixture.CreateClient();
        }

        /*===============================    GetAll Tests    ===============================*/

        [Fact]
        public async Task GetAll_Should_result_OK()
        {
            var response = await _client.GetAsync("api/Evaluation");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetAll_Should_return_some_Evaluation()
        {
            var response = await _client.GetAsync("api/Evaluation");

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var evaluations = JsonConvert.DeserializeObject<List<EvaluationListModel>>(await response.Content.ReadAsStringAsync());
            evaluations.Should().HaveCountGreaterOrEqualTo(1);
        }

        [Fact]
        public async Task GetAll_should_return_4_check_first_and_last_()
        {
            // Act
            var response = await _client.GetAsync("api/Evaluation");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var evaluations = JsonConvert.DeserializeObject<List<EvaluationListModel>>(await response.Content.ReadAsStringAsync());

            evaluations.Should().HaveCountGreaterOrEqualTo(4);
            evaluations[0].Should().BeEquivalentTo(_evaluationsListModel[0]);
            evaluations[3].Should().BeEquivalentTo(_evaluationsListModel[1]);
        }

        /*===============================    GetById Tests    ===============================*/

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public async Task GetById_should_return_OK(int testedIndex)
        {
            var response = await _client.GetAsync($"api/Evaluation/{_evaluationsDetailModel[testedIndex].Id}");

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var evaluation = JsonConvert.DeserializeObject<EvaluationDetailModel>(await response.Content.ReadAsStringAsync());
            evaluation.Should().NotBeNull();
        }

        [Theory]
        [InlineData(0)]
        public async Task GetById_should_return_evaluation_by_Id(int index)
        {

            var response = await _client.GetAsync($"api/Evaluation/{_evaluationsDetailModel[index].Id}");

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var evaluation = JsonConvert.DeserializeObject<EvaluationDetailModel>(await response.Content.ReadAsStringAsync());
            evaluation.Should().NotBeNull();
            evaluation.Should().BeEquivalentTo(_evaluationsDetailModel[index]);
        }

        [Fact]
        public async Task GetById_with_empty_Id_should_return_NotFound()
        {
            // Act
            var response = await _client.GetAsync($"api/Evaluation/{Guid.Empty}");

            // Assert
            response.Should().NotBeNull();
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        /*===============================    Create Tests    ===============================*/

        [Fact]
        public async Task Create_should_return_new_ID()
        {
            // Arrange
            var newEvaluationSerialized = JsonConvert.SerializeObject(_evaluationsNewModel[0]);
            var stringContent = new StringContent(newEvaluationSerialized, Encoding.UTF8, "application/json");

            // Act 
            var response = await _client.PostAsync("api/Evaluation", stringContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var newEvaluationGuid = JsonConvert.DeserializeObject<Guid>(await response.Content.ReadAsStringAsync());
            newEvaluationGuid.Should().NotBeEmpty();
        }

        [Fact]
        public async Task Create_should_create_findable_evaluation()
        {
            // Arrange
            var newEvaluationSerialized = JsonConvert.SerializeObject(_evaluationsNewModel[0]);
            var stringContent = new StringContent(newEvaluationSerialized, Encoding.UTF8, "application/json");

            // Act 
            var response = await _client.PostAsync("api/Evaluation", stringContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var newEvaluationGuid = JsonConvert.DeserializeObject<Guid>(await response.Content.ReadAsStringAsync());
            newEvaluationGuid.Should().NotBeEmpty();

            var responseGetById = await _client.GetAsync($"api/Evaluation/{newEvaluationGuid}");
            responseGetById.StatusCode.Should().Be(HttpStatusCode.OK);

            var evaluation = JsonConvert.DeserializeObject<EvaluationDetailModel>(await responseGetById.Content.ReadAsStringAsync());
            _evalutaionsExpectedDetailModel[0].Id = evaluation.Id;
            evaluation.Should().BeEquivalentTo(_evalutaionsExpectedDetailModel[0]);
        }


        /*===============================    Update Tests    ===============================*/

        [Fact]
        public async Task Update_should_update_existing_evaluation_and_return_OK()
        {
            // Arange
            var newEvaluationSerialized = JsonConvert.SerializeObject(_evaluationsNewModel[2]) ?? throw new ArgumentNullException($"JsonConvert.SerializeObject(_evaluationsNewModel[2])");
            var stringContentCreate = new StringContent(newEvaluationSerialized, Encoding.UTF8, "application/json");
            var responseCreate = await _client.PostAsync("api/Evaluation", stringContentCreate);
            var newEvaluationGuid = JsonConvert.DeserializeObject<Guid>(await responseCreate.Content.ReadAsStringAsync());
            _evaluationUpdateModel.Id = newEvaluationGuid;

            var evaluationToUpdateSerialized = JsonConvert.SerializeObject(_evaluationUpdateModel);
            var stringContent = new StringContent(evaluationToUpdateSerialized, Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PutAsync("api/Evaluation?verison=3.0&culture=en", stringContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var responseGetById = await _client.GetAsync($"api/Evaluation/{_evaluationUpdateModel.Id}");

            var foundEvaluation = JsonConvert.DeserializeObject<EvaluationDetailModel>(await responseGetById.Content.ReadAsStringAsync());
            _evalutaionsExpectedDetailModel[1].Id = foundEvaluation.Id;
            foundEvaluation.Should().BeEquivalentTo(_evalutaionsExpectedDetailModel[1]);
        }

        /*===============================    Delete Tests    ===============================*/

        [Fact]
        public async Task Delete_should_delete_evaluation_and_return_NotFound()
        {
            var newEvaluationSerialized = JsonConvert.SerializeObject(_evaluationsNewModel[0]);
            var stringContent = new StringContent(newEvaluationSerialized, Encoding.UTF8, "application/json");
            var responseCreate = await _client.PostAsync("api/Evaluation", stringContent);
            responseCreate.StatusCode.Should().Be(HttpStatusCode.OK);

            var newEvaluationGuid = JsonConvert.DeserializeObject<Guid>(await responseCreate.Content.ReadAsStringAsync());

            // Act
            var response = await _client.DeleteAsync($"api/Evaluation/{newEvaluationGuid}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var responseGetById = await _client.GetAsync($"api/Category/{newEvaluationGuid}");
            responseGetById.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Delete_empty_Id_should_return_BadRequest()
        {
            // Arrange 
            var newEvaluationGuid = Guid.Empty;

            // Act
            var response = await _client.DeleteAsync($"api/Evaluation/{newEvaluationGuid}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}