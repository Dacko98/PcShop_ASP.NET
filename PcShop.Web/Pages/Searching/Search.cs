using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using PcShop.BL.Api.Models.Category;
using PcShop.BL.Api.Models.Manufacturer;
using PcShop.BL.Api.Models.Product;
using PcShop.BL.Api.Models.Search;
using PcShop.WEB.BL.Facades;

namespace PcShop.Web.Pages.Searching
{
    public partial class Search : ComponentBase
    {
        [Inject] private SearchingFacade SearchFacade { get; set; }
        [Inject] private ManufacturersFacade ManufacturerFacade { get; set; }
        [Inject] private CategoriesFacade CategoryFacade { get; set; }
        [Inject] private ProductsFacade ProductFacade { get; set; }

        [Parameter] public string Phrase { get; set; } = "";
        
        public SearchResultModel FoundedEntities { get; set; } = new SearchResultModel();
        private ICollection<ProductListModel> AllProducts { get; set; } = new List<ProductListModel>();
        public Func<Task> a;
        public async Task HandleSearchChange()
        {
            Phrase = Phrase.Trim();
            FoundedEntities = Phrase == "" ? new SearchResultModel() : await SearchFacade.GetAllContainingText(Phrase);
            //todo set on one click
            StateHasChanged();
        }

        protected override async Task OnInitializedAsync()
        {
            AllProducts = await ProductFacade.GetProductsAsync();

            Phrase = Phrase.Trim();
            FoundedEntities = Phrase == "" ? new SearchResultModel() : await SearchFacade.GetAllContainingText(Phrase);

            await base.OnInitializedAsync();
        }
    }
}
