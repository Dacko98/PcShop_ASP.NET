using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using PcShop.BL.Api.Models.Search;
using PcShop.WEB.BL.Facades;

namespace PcShop.Web.Pages.Searching
{
    public partial class Search : ComponentBase
    {
        [Inject] private SearchingFacade SearchFacade { get; set; }

        [Parameter] public string Phrase { get; set; } = "";
        
        public SearchResultModel FoundedEntities { get; set; } = new SearchResultModel();

        public async Task HandleSearchChange()
        {
            Phrase = Phrase.Trim();
            FoundedEntities = Phrase == "" ? new SearchResultModel() : await SearchFacade.GetAllContainingText(Phrase);
        }

        protected override async Task OnInitializedAsync()
        {
            await HandleSearchChange();

            await base.OnInitializedAsync();
        }
    }
}
