using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using System.Collections.Generic;
using System.Linq;
using PcShop.BL.Api.Facades;
using PcShop.BL.Api.Models.Interfaces;

namespace PcShop.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SearchController : ControllerBase
    {
        private const string ApiOperationBaseName = "Search";
        private readonly SearchFacade searchFacade;


        public SearchController(
            SearchFacade searchFacade)
        {
            this.searchFacade = searchFacade;
        }

        [HttpGet("{searchedText}")]
        [OpenApiOperation(ApiOperationBaseName + nameof(GetAll))]
        public ActionResult<List<IListModel>> GetAll(string searchedText)
        {
            return searchFacade.GetAllContainingText(searchedText).ToList();
        }
    }
}