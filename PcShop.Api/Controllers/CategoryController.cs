
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using NSwag.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using PcShop.BL.Api.Facades;
using PcShop.BL.Api.Models.Category;

namespace PcShop.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private const string ApiOperationBaseName = "Category";
        private readonly CategoryFacade categoryFacade;
        

        public CategoryController(
            CategoryFacade categoryFacade)
        {
            this.categoryFacade = categoryFacade;
        }

        [HttpGet]
        [OpenApiOperation(ApiOperationBaseName + nameof(GetAll))]
        public ActionResult<List<CategoryListModel>> GetAll()
        {
            return categoryFacade.GetAll().ToList();
        }

        [HttpGet("{id}")]
        [OpenApiOperation(ApiOperationBaseName + nameof(GetById))]
        public ActionResult<CategoryDetailModel> GetById(Guid id)
        {
            var category = categoryFacade.GetById(id);
            if (category == null)
            {
                return NotFound();
            }
            return category;
        }

        [HttpPost]
        [OpenApiOperation(ApiOperationBaseName + nameof(Create))]
        public ActionResult<Guid> Create(CategoryNewModel category)
        {

            return categoryFacade.Create(category);
        }

        [HttpPut]
        [OpenApiOperation(ApiOperationBaseName + nameof(Update))]
        public ActionResult<Guid> Update(CategoryUpdateModel category)
        {
            var returnValue =  categoryFacade.Update(category);

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
                categoryFacade.Delete(id);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}