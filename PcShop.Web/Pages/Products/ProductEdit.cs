using Microsoft.AspNetCore.Components.Rendering;
using PcShop.BL.Api.Models.Manufacturer;
using PcShop.BL.Api.Models.Evaluation;
using Microsoft.AspNetCore.Components;
using PcShop.BL.Api.Models.Category;
using PcShop.BL.Api.Models.Product;
using System.Collections.Generic;
using System.Threading.Tasks;
using PcShop.WEB.BL.Facades;
using System.Net.Http.Json;
using System.Diagnostics;
using System.Net.Http;
using System.Linq;
using System;

namespace PcShop.Web.Pages.Products
{
    public partial class ProductEdit : ComponentBase
    {
        [Parameter]
        public Guid Id { get; set; }

        public ProductDetailModel product { get; set; }

        [Inject]
        private ProductsFacade ProductFacade { get; set; }
        [Inject]
        private ManufacturersFacade ManufacturerFacade { get; set; }
        [Inject]
        private CategoriesFacade CategoryFacade { get; set; }
        [Inject]
        private EvaluationsFacade EvaluationsFacade { get; set; }

        private bool NewCategory = false;
        private bool NewManufacturer = false;
        private bool createNewEvaluation = false;
        private EvaluationNewModel NewEvaluation = new EvaluationNewModel();

        //private ICollection<ProductListModel> Products { get; set; } = new List<ProductListModel>();

        //private ICollection<ProductListModel> AllProducts { get; set; } = new List<ProductListModel>();
        private ICollection<ManufacturerListModel> Manufacturers { get; set; } = new List<ManufacturerListModel>();
        private ICollection<CategoryListModel> Category { get; set; } = new List<CategoryListModel>();
        private ICollection<EvaluationListModel> Evaluations { get; set; } = new List<EvaluationListModel>();

        protected override async Task OnInitializedAsync()
        {
            if(Id == Guid.Empty)
                product = new ProductDetailModel();
            else
                product = await ProductFacade.GetProductAsync(Id);

            Manufacturers = await ManufacturerFacade.GetManufacturersAsync();
            Category = await CategoryFacade.GetCategorysAsync();
            Evaluations = await EvaluationsFacade.GetEvaluationsAsync();
            
            await base.OnInitializedAsync();
        }

        protected async Task SaveData()
        {
            Debug.WriteLine("Data should be saved");
            // UpdateAsync(product) or something
            // actualize all evaluations 
            // push the new evaluation if there is one
        }

        public void categorySelect(ChangeEventArgs e)
        {
            if (e.Value.ToString() == "new category")
                NewCategory = true;
            else
            {
                NewCategory = false;
                product.CategoryName = e.Value.ToString();
            }
        }

        public void manufacturerSelect(ChangeEventArgs e)
        {
            if (e.Value.ToString() == "new manufacturer")
                NewManufacturer = true;
            else
            {
                NewManufacturer = false;
                product.ManufacturerName = e.Value.ToString();
            }
        }

        protected void AddEvaluation()
        {
            // TODO - tadyto nefunguje, ten btn odkazuje na SaveData, idk why...
            Debug.WriteLine("it is working");
            if (createNewEvaluation)
            {
                // There is one already, should be pushed or something.
            }
            else
            {
                createNewEvaluation = true;
            }
        }
    }
}
