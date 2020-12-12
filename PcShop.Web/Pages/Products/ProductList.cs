using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using PcShop.WEB.BL.Facades;
using PcShop.BL.Api.Models.Product;
using PcShop.BL.Api.Models.Manufacturer;
using PcShop.BL.Api.Models.Category;


namespace PcShop.Web.Pages.Products
{
    public partial class ProductList
    {

        [Inject]
        private ProductsFacade ProductFacade { get; set; }
        [Inject]
        private ManufacturersFacade ManufacturerFacade { get; set; }
        [Inject]
        private CategorysFacade CategoryFacade { get; set; }

        private ICollection<ProductListModel> Products { get; set; } = new List<ProductListModel>();
        private ICollection<ManufacturerListModel> Manufacturers { get; set; } = new List<ManufacturerListModel>();
        private ICollection<CategoryListModel> Category { get; set; } = new List<CategoryListModel>();

        protected override async Task OnInitializedAsync()
        {
            await LoadData();

            await base.OnInitializedAsync();
        }

        private async Task LoadData()
        {
            Products = await ProductFacade.GetProductsAsync();
            Manufacturers = await ManufacturerFacade.GetManufacturersAsync();
            Category = await CategoryFacade.GetCategorysAsync();
        }
    }
}
