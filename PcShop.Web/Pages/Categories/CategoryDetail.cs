using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using PcShop.BL.Api.Models.Category;
using PcShop.BL.Api.Models.Product;
using PcShop.WEB.BL.Facades;

namespace PcShop.Web.Pages.Categories
{
    public partial class CategoryDetail : ComponentBase
    {
        [Inject] private CategoriesFacade CategoryFacade { get; set; }
        [Inject] private ProductsFacade ProductFacade { get; set; }

        [Parameter] public Guid Id { get; set; }

        public CategoryDetailModel Category { get; set; }
        private ICollection<ProductListModel> Products { get; set; } = new List<ProductListModel>();
        
        protected override async Task OnInitializedAsync()
        {
            Category = await CategoryFacade.GetCategoryAsync(Id);
            Products = await ProductFacade.GetProductsAsync();
            await base.OnInitializedAsync();
        }
    }
}
