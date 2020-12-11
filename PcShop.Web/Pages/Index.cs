using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PcShop.WEB.BL.Facades;
using Microsoft.AspNetCore.Components;
using PcShop.BL.Api.Models.Product;

namespace PcShop.Web.Pages
{
    public partial class Index
    {
        [Inject]
        private ProductsFacade ProductFacade { get; set; }

        private ICollection<ProductListModel> Products { get; set; } = new List<ProductListModel>();

        protected override async Task OnInitializedAsync()
        {
            await LoadData();

            await base.OnInitializedAsync();
        }

        private async Task LoadData()
        {
            Products = await ProductFacade.GetProductsAsync();
        }
    }
}
