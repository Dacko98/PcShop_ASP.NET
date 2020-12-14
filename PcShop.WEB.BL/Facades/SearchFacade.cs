using System.Threading.Tasks;
using PcShop.BL.Api.Facades.Interfaces;
using PcShop.BL.Api.Models.Search;
using PcShop.Console.Api;

namespace PcShop.WEB.BL.Facades
{
    public class SearchingFacade : IAppFacade
    {
        private readonly ISearchClient _searchingClient;

        public SearchingFacade(ISearchClient searchingClient)
        {
            this._searchingClient = searchingClient;
        }

        public async Task<SearchResultModel> GetAllContainingText(string searchedText)
        {
            return await _searchingClient.SearchAsync(searchedText);
        }
    }
}
