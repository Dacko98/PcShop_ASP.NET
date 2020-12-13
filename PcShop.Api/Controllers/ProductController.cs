
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using NSwag.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using PcShop.BL.Api.Facades;
using PcShop.BL.Api.Models.Product;

namespace PcShop.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private const string ApiOperationBaseName = "Product";
        private readonly ProductFacade _productFacade;
        

        public ProductController(
            ProductFacade productFacade)
        {
            this._productFacade = productFacade;
        }

        [HttpGet]
        [OpenApiOperation(ApiOperationBaseName + nameof(GetAll))]
        public ActionResult<List<ProductListModel>> GetAll()
        {
            return _productFacade.GetAll().ToList();
        }

        [HttpGet("{id}")]
        [OpenApiOperation(ApiOperationBaseName + nameof(GetById))]
        public ActionResult<ProductDetailModel> GetById(Guid id)
        {
            var product = _productFacade.GetById(id);
            if (product == null)
            {
                return NotFound();
            }
            return product;
        }

        [HttpPost]
        [OpenApiOperation(ApiOperationBaseName + nameof(Create))]
        public ActionResult<Guid> Create(ProductNewModel product)
        {
            return _productFacade.Create(product);
        }

        [HttpPut]
        [OpenApiOperation(ApiOperationBaseName + nameof(Update))]
        public ActionResult<Guid> Update(ProductUpdateModel product)
        {
            var returnValue =  _productFacade.Update(product);

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
            try {
                _productFacade.Delete(id);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
            return Ok();
}
    }
}