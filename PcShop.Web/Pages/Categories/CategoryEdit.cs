using Microsoft.AspNetCore.Components;
using PcShop.BL.Api.Models.Category;
using System.Threading.Tasks;
using PcShop.WEB.BL.Facades;
using System;
using System.Collections.Generic;
using PcShop.BL.Api.Models.Product;

namespace PcShop.Web.Pages.Categories
{
    public partial class CategoryEdit : ComponentBase
    {
        [Inject] private CategoriesFacade CategoryFacade { get; set; }
        [Inject] private ProductsFacade ProductFacade { get; set; }

        [Parameter]
        public Guid Id { get; set; }

        public CategoryDetailModel Category { get; set; }
        private ICollection<ProductListModel> Products { get; set; } = new List<ProductListModel>();


        protected override async Task OnInitializedAsync()
        {
            if(Id == Guid.Empty)
                Category = new CategoryDetailModel();
            else
                Category = await CategoryFacade.GetCategoryAsync(Id);
            Products = await ProductFacade.GetProductsAsync();

            await base.OnInitializedAsync();
        }

        protected async Task SaveData()
        {
            // todo
        }
    }
}
