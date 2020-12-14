using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    public partial class Search : ComponentBase
    {
        [Inject] private SearchingFacade SearchFacade { get; set; }
        [Inject] private ManufacturersFacade ManufacturerFacade { get; set; }
        [Inject] private CategoriesFacade CategoryFacade { get; set; }
        [Inject] private ProductsFacade ProductFacade { get; set; }

        [Parameter] public string Phrase { get; set; } = "";
        
        public SearchResultModel FoundedEntities { get; set; } = new SearchResultModel();
        private ICollection<ProductListModel> Products { get; set; } = new List<ProductListModel>();
        private ICollection<ProductListModel> AllProducts { get; set; } = new List<ProductListModel>();
        private ICollection<ManufacturerListModel> AllManufacturers { get; set; } = new List<ManufacturerListModel>();
        private ICollection<CategoryListModel> AllCategories { get; set; } = new List<CategoryListModel>();

        public List<string> SelectedValues { get; set; } = new List<string>();
        public string CategoryVal = "All";
        public int PriceStartVal { get; set; } = 0;
        public int PriceEndVal { get; set; } = Int32.MaxValue;

        public int WeightStartVal { get; set; } = 0;
        public int WeightEndVal { get; set; } = Int32.MaxValue;

        public bool InStockVal { get; set; } = false;

        public async Task HandleSearchChange()
        {
            Debug.WriteLine("aaaa");
            Phrase = Phrase.Trim();

            FoundedEntities = Phrase == "" ? new SearchResultModel() : await SearchFacade.GetAllContainingText(Phrase);
            Products = FoundedEntities.ProductEntities;

            await base.OnInitializedAsync();
        }

        protected override async Task OnInitializedAsync()
        {
            Phrase = Phrase.Trim();

            FoundedEntities = Phrase == "" ? new SearchResultModel() : await SearchFacade.GetAllContainingText(Phrase);
            AllManufacturers = await ManufacturerFacade.GetManufacturersAsync();
            AllCategories = await CategoryFacade.GetCategorysAsync();
            AllProducts = await ProductFacade.GetProductsAsync();

            Products = FoundedEntities.ProductEntities;

            await base.OnInitializedAsync();
        }

        public void CategorySelect(ChangeEventArgs e)
        {
            CategoryVal = e.Value.ToString();
            ApplyFilters();
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
            Products = CategoryVal.Equals("All") ? FoundedEntities.ProductEntities : FoundedEntities.ProductEntities.Where(f => f.CategoryName.Equals(CategoryVal)).ToList();
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
