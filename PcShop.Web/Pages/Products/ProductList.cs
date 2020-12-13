using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
        private CategoriesFacade CategoryFacade { get; set; }


        private ICollection<ProductListModel> Products { get; set; } = new List<ProductListModel>();

        private ICollection<ProductListModel> AllProducts { get; set; } = new List<ProductListModel>();
        private ICollection<ManufacturerListModel> Manufacturers { get; set; } = new List<ManufacturerListModel>();
        private ICollection<CategoryListModel> Category { get; set; } = new List<CategoryListModel>();

        public List<string> SelectedValues { get; set; } = new List<string>();
        public string category = "All";

        protected override async Task OnInitializedAsync()
        {
            await LoadData();

            await base.OnInitializedAsync();
        }

        private async Task LoadData()
        {

            Products = await ProductFacade.GetProductsAsync();
            AllProducts = Products;
            Manufacturers = await ManufacturerFacade.GetManufacturersAsync();
            Category = await CategoryFacade.GetCategorysAsync();
        }

        public void categorySelect(ChangeEventArgs e)
        {

            Debug.WriteLine("My debug string");
            category = e.Value.ToString();
            Products = category.Equals("All") ? AllProducts : AllProducts.Where(f => f.CategoryName.Equals(category)).ToList();

        }


        public void checkBoxChecked(string aSelectedId, object aChecked)
        {
            {
                if ((bool)aChecked)
                {
                    if (!SelectedValues.Contains(aSelectedId))
                    {
                        SelectedValues.Add(aSelectedId);
                    }
                }
                else
                {
                    if (SelectedValues.Contains(aSelectedId))
                    {
                        SelectedValues.Remove(aSelectedId);
                    }
                }
            }
            Products = AllProducts.Where(f => SelectedValues.Contains(f.ManufacturerName)).ToList();
        }

    }
}
