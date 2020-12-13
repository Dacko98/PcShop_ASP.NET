using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using PcShop.BL.Api.Models.Category;
using PcShop.BL.Api.Models.Manufacturer;
using PcShop.BL.Api.Models.Product;
using PcShop.BL.Api.Models.Search;
using PcShop.WEB.BL.Facades;

namespace PcShop.Web.Pages.Searching
{
    public partial class ProductDetail : ComponentBase
    {
        [Inject] private SearchingFacade SearchFacade { get; set; }
        [Inject] private ManufacturersFacade ManufacturerFacade { get; set; }
        [Inject] private CategoriesFacade CategoryFacade { get; set; }

        [Parameter] public string Phrase { get; set; } = "";
        
        public SearchResultModel FoundedEntities { get; set; } = new SearchResultModel();
        private ICollection<ProductListModel> Products { get; set; } = new List<ProductListModel>();
        private ICollection<ManufacturerListModel> AllManufacturers { get; set; } = new List<ManufacturerListModel>();
        private ICollection<CategoryListModel> AllCategories { get; set; } = new List<CategoryListModel>();

        public List<string> SelectedValues { get; set; } = new List<string>();
        public string category = "All";
        public int priceStart { get; set; } = 0;
        public int priceEnd { get; set; } = Int32.MaxValue;

        public int weightStart { get; set; } = 0;
        public int weightEnd { get; set; } = Int32.MaxValue;

        public bool inStock { get; set; } = false;
        protected override async Task OnInitializedAsync()
        {
            if (Phrase.Trim() != "")
            {
                FoundedEntities = await SearchFacade.GetAllContainingText(Phrase);
                AllManufacturers = await ManufacturerFacade.GetManufacturersAsync();
                AllCategories = await CategoryFacade.GetCategorysAsync();
            }

            await base.OnInitializedAsync();
        }








        public void categorySelect(ChangeEventArgs e)
        {
            category = e.Value.ToString();
            applyFilters();
        }

        public void PriceStart(ChangeEventArgs e)
        {
            string val = e.Value.ToString();
            priceStart = val.Equals("") ? 0 : Int32.Parse(val);

            applyFilters();
        }
        public void PriceEnd(ChangeEventArgs e)
        {
            string val = e.Value.ToString();
            priceEnd = val.Equals("") ? Int32.MaxValue : Int32.Parse(val);

            applyFilters();
        }

        public void WeightStart(ChangeEventArgs e)
        {
            string val = e.Value.ToString();

            weightStart = val.Equals("") ? 0 : Int32.Parse(val);


            applyFilters();
        }
        public void WeightEnd(ChangeEventArgs e)
        {
            string val = e.Value.ToString();
            weightEnd = val.Equals("") ? Int32.MaxValue : Int32.Parse(val);

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

            inStock = ((bool)aChecked);
            applyFilters();
        }

        public void applyFilters()
        {

            Products = category.Equals("All") ? FoundedEntities.ProductEntities : FoundedEntities.ProductEntities.Where(f => f.CategoryName.Equals(category)).ToList();
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
