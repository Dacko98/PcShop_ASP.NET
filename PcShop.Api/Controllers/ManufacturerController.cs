
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using NSwag.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using PcShop.BL.Api.Facades;
using PcShop.BL.Api.Models.Manufacturer;

namespace PcShop.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ManufacturerController : ControllerBase
    {
        private const string ApiOperationBaseName = "Manufacturer";
        private readonly ManufacturerFacade _manufacturerFacade;


        public ManufacturerController(
            ManufacturerFacade manufacturerFacade)
        {
            this._manufacturerFacade = manufacturerFacade;
        }

        [HttpGet]
        [OpenApiOperation(ApiOperationBaseName + nameof(GetAll))]
        public ActionResult<List<ManufacturerListModel>> GetAll()
        {
            return _manufacturerFacade.GetAll().ToList();
        }

        [HttpGet("{id}")]
        [OpenApiOperation(ApiOperationBaseName + nameof(GetById))]
        public ActionResult<ManufacturerDetailModel> GetById(Guid id)
        {
            var manufacturer = _manufacturerFacade.GetById(id);
            if (manufacturer == null)
            {
                return NotFound();
            }
            return manufacturer;
        }

        [HttpPost]
        [OpenApiOperation(ApiOperationBaseName + nameof(Create))]
        public ActionResult<Guid> Create(ManufacturerNewModel manufacturer)
        {
            return _manufacturerFacade.Create(manufacturer);
        }

        [HttpPut]
        [OpenApiOperation(ApiOperationBaseName + nameof(Update))]
        public ActionResult<Guid> Update(ManufacturerUpdateModel manufacturer)
        {
            var returnValue =  _manufacturerFacade.Update(manufacturer);

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
                _manufacturerFacade.Delete(id);
            }
            catch (Exception)
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}