using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using PcShop.BL.Api.Facades;
using PcShop.BL.Api.Models.Search;

namespace PcShop.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SearchController : ControllerBase
    {
        private const string ApiOperationBaseName = "Search";
        private readonly SearchFacade _searchFacade;


        public SearchController(
            SearchFacade searchFacade)
        {
            this._searchFacade = searchFacade;
        }

        [HttpGet("{searchedText}")]
        [OpenApiOperation(ApiOperationBaseName + nameof(GetAll))]
        public ActionResult<SearchResultModel> GetAll(string searchedText)
        {
            return _searchFacade.GetAllContainingText(searchedText);
        }
    }
}