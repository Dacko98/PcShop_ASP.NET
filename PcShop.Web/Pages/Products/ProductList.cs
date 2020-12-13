using System;
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
        public int priceStart { get; set; } = 0;
        public int priceEnd { get; set; } = Int32.MaxValue;

        public int weightStart { get; set; } = 0;
        public int weightEnd { get; set; } = Int32.MaxValue;

        public bool inStock { get; set; } = false;

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
            category = e.Value.ToString();
            applyFilters();
        }

        public void PriceStart(ChangeEventArgs e)
        {
            string val = e.Value.ToString();
            if (!val.Equals(""))
            {
                priceStart = Int32.Parse(val);
            }

            applyFilters();
        }
        public void PriceEnd(ChangeEventArgs e)
        {
            string val = e.Value.ToString();
            if (!val.Equals(""))
            {
                priceEnd = Int32.Parse(val);
            }

            applyFilters();
        }

        public void WeightStart(ChangeEventArgs e)
        {
            string val = e.Value.ToString();
            if (!val.Equals(""))
            {
                weightStart = Int32.Parse(val);
            }

            applyFilters();
        }
        public void WeightEnd(ChangeEventArgs e)
        {
            string val = e.Value.ToString();
            if (!val.Equals(""))
            {
                weightEnd = Int32.Parse(val);
            }

            applyFilters();
        }


        public void CheckBoxChecked(string aSelectedId, object aChecked)
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
            applyFilters();
        }

        public void Stock(object aChecked)
        {

            inStock = ((bool) aChecked);
            applyFilters();
        }

        public void applyFilters()
        {

            Products = category.Equals("All") ? AllProducts : AllProducts.Where(f => f.CategoryName.Equals(category)).ToList();
            Products = !SelectedValues.Any() ? Products : Products.Where(f => SelectedValues.Contains(f.ManufacturerName)).ToList();
            Products = Products.Where(f => f.Price <= priceEnd && f.Price >= priceStart).ToList();
            Products = Products.Where(f => f.Weight <= weightEnd && f.Weight >= weightStart).ToList();
            if (inStock)
            {
                Products = Products.Where(f => f.CountInStock > 0).ToList();
            }
        }
    }
}
