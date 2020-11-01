
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using NSwag.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using PcShop.BL.Api.Facades;
using PcShop.BL.Api.Models.Evaluation;

namespace PcShop.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EvaluationController : ControllerBase
    {
        private const string ApiOperationBaseName = "Evaluation";
        private readonly EvaluationFacade evaluationFacade;


        public EvaluationController(
            EvaluationFacade evaluationFacade)
        {
            this.evaluationFacade = evaluationFacade;
        }

        [HttpGet]
        [OpenApiOperation(ApiOperationBaseName + nameof(GetAll))]
        public ActionResult<List<EvaluationListModel>> GetAll()
        {
            return evaluationFacade.GetAll().ToList();
        }

        [HttpGet("{id}")]
        [OpenApiOperation(ApiOperationBaseName + nameof(GetById))]
        public ActionResult<EvaluationDetailModel> GetById(Guid id)
        {
            var evaluation = evaluationFacade.GetById(id);
            if (evaluation == null)
            {
                return NotFound();
            }

            return evaluation;
        }

        [HttpPost]
        [OpenApiOperation(ApiOperationBaseName + nameof(Create))]
        public ActionResult<Guid> Create(EvaluationNewModel evaluation)
        {
            return evaluationFacade.Create(evaluation);
        }

        [HttpPut]
        [OpenApiOperation(ApiOperationBaseName + nameof(Update))]
        public ActionResult<Guid> Update(EvaluationUpdateModel evaluation)
        {
            var returnValue = evaluationFacade.Update(evaluation);

            if (returnValue == null)
            {
                return NotFound();
            }

            return returnValue;
        }


        [HttpDelete("{id}")]
        [OpenApiOperation(ApiOperationBaseName + nameof(Delete))]
        public IActionResult Delete(Guid id)
        {
            try
            {
                evaluationFacade.Delete(id);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}