
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using NSwag.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using PcShop.BL.Api.Facades;
using PcShop.BL.Api.Models.Goods;

namespace PcShop.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GoodsController : ControllerBase
    {
        private const string ApiOperationBaseName = "Goods";
        private readonly GoodsFacade goodsFacade;
        

        public GoodsController(
            GoodsFacade goodsFacade)
        {
            this.goodsFacade = goodsFacade;
        }

        [HttpGet]
        [OpenApiOperation(ApiOperationBaseName + nameof(GetAll))]
        public ActionResult<List<GoodsListModel>> GetAll()
        {
            return goodsFacade.GetAll().ToList();
        }

        [HttpGet("{id}")]
        [OpenApiOperation(ApiOperationBaseName + nameof(GetById))]
        public ActionResult<GoodsDetailModel> GetById(Guid id)
        {
            var goods = goodsFacade.GetById(id);

            return goods;
        }

        [HttpPost]
        [OpenApiOperation(ApiOperationBaseName + nameof(Create))]
        public ActionResult<Guid> Create(GoodsNewModel goods)
        {
            return goodsFacade.Create(goods);
        }

        [HttpPut]
        [OpenApiOperation(ApiOperationBaseName + nameof(Update))]
        public ActionResult<Guid> Update(GoodsUpdateModel goods)
        {
            return goodsFacade.Update(goods);
        }

        [HttpDelete("{id}")]
        [OpenApiOperation(ApiOperationBaseName + nameof(Delete))]
        public IActionResult Delete(Guid id)
        {
            goodsFacade.Delete(id);
            return Ok();
        }
    }
}