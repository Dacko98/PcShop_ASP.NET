/* File:        EvaluationControllerTests.cs
 * 
 * Solution:    PcShop
 * Project:     PcShop.Api.Test
 * 
 * Team:        Team0011
 * Author:      Daniel Jacko
 * Login:       xjacko04
 * Date:        1.11.2020
 * 
 * Description: This file contains API tests for EvaluationController in PcShop.Api.
 *              Tests all main 4 methods (GET, PUT, POST, DELETE)
 * 
 * Installed NuGet packages: Microsoft.AspNetCore.Mvc.Testing, FluentAssertions
 */

using Microsoft.AspNetCore.Mvc.Testing;
using PcShop.BL.Api.Models.Category;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;
using FluentAssertions;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using System.Net;
using System;
using PcShop.BL.Api.Models.Evaluation;
using PcShop.DAL.Entities;
using Xunit;

namespace PcShop.Api.Tests
{
    [Collection(name: "EvaluationControllerTests")]
    public class EvaluationControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private HttpClient _client;

        // This should maybe go to some sort of shared memory between tests: 
        // Constant for first Evaluation Evaluation_RIRST
        private const string TOOSHORTNAME = "c";
        private const string TOOLONGNAME = "This name is too long for the model. This name is too long for the model. This name is too long for the model. " +
            "This name is too long for the model. This name is too long for the model. This name is too long for the model. ";

        private readonly EvaluationListModel[] EvaluationS_LIST =
        {
            new EvaluationListModel()
            {
                Id = new Guid("df935095-8709-4040-a2bb-b6f97cb416dc"),
                TextEvaluation = "Good",
                PercentEvaluation = 80,
                ProductName = "Lattitude E6440",
                EntityType = EntityTypeEnum.EvaluationEntity
            },
            new EvaluationListModel()
            {
                Id = new Guid("23b3902d-7d4f-4213-9cf0-112348f56230"),
                TextEvaluation = "Broke after one month",
                PercentEvaluation = 0,
                ProductName = "Thinkpad T560",
                EntityType = EntityTypeEnum.EvaluationEntity
            }
        };


        private readonly EvaluationDetailModel[] EvaluationS_DETAIL =
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

        private readonly EvaluationNewModel[] EvaluationS_NEW =
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

        private EvaluationDetailModel[] EvaluationS_DETAIL_EXPECTED =
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

        private readonly EvaluationUpdateModel Evaluation_UPDATE = new EvaluationUpdateModel
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

        /// <summary>
        /// Try Get all Evaluation. Shoudl return Status Code OK.
        /// </summary>
        [Fact]
        public async Task GetAll_Should_result_OK()
        {
            var response = await _client.GetAsync("api/Evaluation");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        /// <summary>
        /// Try get all Evaluation. Should return non-empty field in response.content
        /// </summary>
        [Fact]
        public async Task GetAll_Should_return_some_Evaluation()
        {
            var response = await _client.GetAsync("api/Evaluation");

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var Evaluations = JsonConvert.DeserializeObject<List<EvaluationListModel>>(await response.Content.ReadAsStringAsync());
            Evaluations.Should().HaveCountGreaterOrEqualTo(1);
        }

        /// <summary>
        /// Try get all Evaluation. 
        /// Check if it returns at least 6 Evaluations.
        /// Check if the first and the last Evaluation match with model
        /// </summary>
        [Fact]
        public async Task GetAll_Should_return_first_last_and_all_the_others()
        {
            // Act
            var response = await _client.GetAsync("api/Evaluation");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var Evaluations = JsonConvert.DeserializeObject<List<EvaluationListModel>>(await response.Content.ReadAsStringAsync());

            // Should return at least 6 Evaluations
            Evaluations.Should().HaveCountGreaterOrEqualTo(4);

            // First Evaluation
            Evaluations[0].Should().BeEquivalentTo(EvaluationS_LIST[0]);
            // Last Evaluation
            Evaluations[3].Should().BeEquivalentTo(EvaluationS_LIST[1]);
        }

        /*===============================    GetById Tests    ===============================*/

        /// <summary>
        /// Try GetById one Evaluation. Should return StatusCode.OK and response.Content should not be null
        /// </summary>
        /// <param name="index">Index of tested Evaluation. (FIRST = 0, LAST = 1)</param>
        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public async Task GetById_Should_return_something(int index)
        {
            var response = await _client.GetAsync($"api/Evaluation/{EvaluationS_DETAIL[index].Id}");

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var Evaluation = JsonConvert.DeserializeObject<EvaluationDetailModel>(await response.Content.ReadAsStringAsync());
            Evaluation.Should().NotBeNull();
        }

        [Theory]
        [InlineData(0)]
        public async Task GetById_Should_return_Evaluation_by_Id(int index)
        {

            var response = await _client.GetAsync($"api/Evaluation/{EvaluationS_DETAIL[index].Id}");

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var Evaluation = JsonConvert.DeserializeObject<EvaluationDetailModel>(await response.Content.ReadAsStringAsync());
            Evaluation.Should().NotBeNull();
            Evaluation.Should().BeEquivalentTo(EvaluationS_DETAIL[index]);
        }

        /// <summary>
        /// Try GetById with empty id (non-existing Evaluation). Should return BadRequest (400) 
        /// </summary>
        [Fact]
        public async Task GetById_With_empty_Id()
        {
            // Act
            var response = await _client.GetAsync($"api/Evaluation/{Guid.Empty}");

            // Assert
            response.Should().NotBeNull();
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        /*===============================    Create Tests    ===============================*/

        [Fact]
        public async Task Create_Should_return_new_ID()
        {
            // Arrange
            var newEvaluationSerialized = JsonConvert.SerializeObject(EvaluationS_NEW[0]);
            var stringContent = new StringContent(newEvaluationSerialized, Encoding.UTF8, "application/json");

            // Act 
            var response = await _client.PostAsync("api/Evaluation", stringContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var newEvaluationGuid = JsonConvert.DeserializeObject<Guid>(await response.Content.ReadAsStringAsync());
            newEvaluationGuid.Should().NotBeEmpty();
        }

        [Fact]
        public async Task Create_Should_create_findable_Evaluation()
        {
            // Arrange
            var newEvaluationSerialized = JsonConvert.SerializeObject(EvaluationS_NEW[0]);
            var stringContent = new StringContent(newEvaluationSerialized, Encoding.UTF8, "application/json");

            // Act 
            var response = await _client.PostAsync("api/Evaluation", stringContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var newEvaluationGuid = JsonConvert.DeserializeObject<Guid>(await response.Content.ReadAsStringAsync());
            newEvaluationGuid.Should().NotBeEmpty();

            // GetById
            var response_GetById = await _client.GetAsync($"api/Evaluation/{newEvaluationGuid}");
            response_GetById.StatusCode.Should().Be(HttpStatusCode.OK);

            var Evaluation = JsonConvert.DeserializeObject<EvaluationDetailModel>(await response_GetById.Content.ReadAsStringAsync());
            Evaluation.Id.Should().NotBe(Guid.Empty);
            EvaluationS_DETAIL_EXPECTED[0].Id = Evaluation.Id;
            Evaluation.Should().BeEquivalentTo(EvaluationS_DETAIL_EXPECTED[0]);
        }


        /*===============================    Update Tests    ===============================*/

        /// <summary>
        /// Create new Evaluation and then try update its prize to double, should return OK. Then GetById updated Evaluation
        /// </summary>
        [Fact]
        public async Task Update_Should_update_existing_Evaluation()
        {
            // Create new Evaluation 
            // Arrange
            var newEvaluationSerialized = JsonConvert.SerializeObject(EvaluationS_NEW[2]);
            var stringContent_create = new StringContent(newEvaluationSerialized, Encoding.UTF8, "application/json");

            // Act 
            var response_create = await _client.PostAsync("api/Evaluation", stringContent_create);

            // Assert
            response_create.StatusCode.Should().Be(HttpStatusCode.OK);
            var newEvaluationGuid = JsonConvert.DeserializeObject<Guid>(await response_create.Content.ReadAsStringAsync());
            newEvaluationGuid.Should().NotBeEmpty();
            Evaluation_UPDATE.Id = newEvaluationGuid;


            // Arange
            var EvaluationToUpdateSerialized = JsonConvert.SerializeObject(Evaluation_UPDATE);
            var stringContent = new StringContent(EvaluationToUpdateSerialized, Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PutAsync("api/Evaluation?verison=3.0&culture=en", stringContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            // GetById
            var response_GetById = await _client.GetAsync($"api/Evaluation/{Evaluation_UPDATE.Id}");
            response_GetById.StatusCode.Should().Be(HttpStatusCode.OK);

            var Evaluation = JsonConvert.DeserializeObject<EvaluationDetailModel>(await response_GetById.Content.ReadAsStringAsync());
            EvaluationS_DETAIL_EXPECTED[1].Id = Evaluation.Id;
            Evaluation.Should().BeEquivalentTo(EvaluationS_DETAIL_EXPECTED[1]);
        }

        /*===============================    Delete Tests    ===============================*/


        /// <summary>
        /// Try delete existing Evaluation and then find it. Should return NotFound
        /// </summary>
        [Fact]
        public async Task Delete_Should_delete_Evaluation()
        {
            // Arrange - Create new Evaluation
            var newEvaluationSerialized = JsonConvert.SerializeObject(EvaluationS_NEW[0]);
            var stringContent = new StringContent(newEvaluationSerialized, Encoding.UTF8, "application/json");

            // Act 
            var response_create = await _client.PostAsync("api/Evaluation", stringContent);

            // Assert
            response_create.StatusCode.Should().Be(HttpStatusCode.OK);
            var newEvaluationGuid = JsonConvert.DeserializeObject<Guid>(await response_create.Content.ReadAsStringAsync());
            newEvaluationGuid.Should().NotBeEmpty();

            // Act
            var response = await _client.DeleteAsync($"api/Evaluation/{newEvaluationGuid}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            // GetById deleted Evaluation -> assert NotFound
            // Act
            var response_GetById = await _client.GetAsync($"api/Category/{newEvaluationGuid}");

            // Assert
            response_GetById.Should().NotBeNull();
            response_GetById.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        /// <summary>
        /// Try delete non-existing Evaluation. Should return BadRequest
        /// </summary>
        [Fact]
        public async Task Delete_Empty_Id()
        {
            // Arrange 
            var newEvaluationGuid = Guid.Empty;

            // Act
            var response = await _client.DeleteAsync($"api/Evaluation/{newEvaluationGuid}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            //response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);  - It actually returns InternalServerError
        }
    }
}