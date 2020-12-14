using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using PcShop.BL.Api.Models.Category;
using PcShop.BL.Api.Models.Manufacturer;
using PcShop.BL.Api.Models.Product;
using PcShop.WEB.BL.Facades;

namespace PcShop.Web.Pages.Categories
{
    public partial class CategoryDetail : ComponentBase
    {
        [Inject] private CategoriesFacade CategoryFacade { get; set; }
        [Inject] private ProductsFacade ProductFacade { get; set; }
        [Inject] private ManufacturersFacade ManufacturerFacade { get; set; }

        [Parameter] public Guid Id { get; set; }

        public CategoryDetailModel Category { get; set; }
        private ICollection<ProductListModel> Products { get; set; } = new List<ProductListModel>();

        private ICollection<ProductListModel> AllProducts { get; set; } = new List<ProductListModel>();
        private ICollection<ManufacturerListModel> Manufacturers { get; set; } = new List<ManufacturerListModel>();

        public List<string> SelectedValues { get; set; } = new List<string>();

        public string CategoryVal= "All";
        public int PriceStartVal { get; set; } = 0;
        public int PriceEndVal { get; set; } = Int32.MaxValue;

        public int WeightStartVal { get; set; } = 0;
        public int WeightEndVal { get; set; } = Int32.MaxValue;

        public bool InStockVal { get; set; } = false;

        protected override async Task OnInitializedAsync()
        {
            await LoadData();

            await base.OnInitializedAsync();
        }

        private async Task LoadData()
        {
            Category = await CategoryFacade.GetCategoryAsync(Id);
            Products = (await ProductFacade.GetProductsAsync())
                .ToList().FindAll(p=>p.CategoryName == Category.Name);

            AllProducts = Products;
            Manufacturers = await ManufacturerFacade.GetManufacturersAsync();
        }

        public void PriceStart(ChangeEventArgs e)
        {
            string val = e.Value.ToString();
            PriceStartVal = val.Equals("") ? 0 : Int32.Parse(val);

            ApplyFilters();
        }
        public void PriceEnd(ChangeEventArgs e)
        {
            string val = e.Value.ToString();
            PriceEndVal = val.Equals("") ? Int32.MaxValue : Int32.Parse(val);

            ApplyFilters();
        }

        public void WeightStart(ChangeEventArgs e)
        {
            string val = e.Value.ToString();

            WeightStartVal = val.Equals("") ? 0 : Int32.Parse(val);

            ApplyFilters();
        }
        public void WeightEnd(ChangeEventArgs e)
        {
            string val = e.Value.ToString();
            WeightEndVal = val.Equals("") ? Int32.MaxValue : Int32.Parse(val);

            ApplyFilters();
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
            ApplyFilters();
        }

        public void Stock(object aChecked)
        {

            InStockVal = ((bool)aChecked);
            ApplyFilters();
        }

        public void ApplyFilters()
        {

            Products = CategoryVal.Equals("All") ? AllProducts : AllProducts.Where(f => f.CategoryName.Equals(CategoryVal)).ToList();
            Products = !SelectedValues.Any() ? Products : Products.Where(f => SelectedValues.Contains(f.ManufacturerName)).ToList();
            Products = Products.Where(f => f.Price <= PriceEndVal && f.Price >= PriceStartVal).ToList();
            Products = Products.Where(f => f.Weight <= WeightEndVal && f.Weight >= WeightStartVal).ToList();
            if (InStockVal)
            {
                Products = Products.Where(f => f.CountInStock > 0).ToList();
            }
        }
    }
}
