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
using Newtonsoft.Json;
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

        private bool createNewCategory = false;
        private bool createNewManufacturer = false;
        private bool createNewEvaluation = false;
        private EvaluationNewModel NewEvaluation = new EvaluationNewModel();
        private ManufacturerNewModel newManufacturer = new ManufacturerNewModel();

        //private ICollection<ProductListModel> Products { get; set; } = new List<ProductListModel>();

        //private ICollection<ProductListModel> AllProducts { get; set; } = new List<ProductListModel>();
        private ICollection<ManufacturerListModel> Manufacturers { get; set; } = new List<ManufacturerListModel>();
        private ICollection<CategoryListModel> Categories { get; set; } = new List<CategoryListModel>();
        private ICollection<EvaluationListModel> Evaluations { get; set; } = new List<EvaluationListModel>();

        protected override async Task OnInitializedAsync()
        {
            if (Id == Guid.Empty)
                product = new ProductDetailModel();
            else
                product = await ProductFacade.GetProductAsync(Id);

            Manufacturers = await ManufacturerFacade.GetManufacturersAsync();
            Categories = await CategoryFacade.GetCategorysAsync();
            Evaluations = await EvaluationsFacade.GetEvaluationsAsync();

            await base.OnInitializedAsync();
        }

        protected async Task SaveData()
        {
            Debug.WriteLine("Data should be saved");

            // UpdateAsync(product) or something
            // actualize all evaluations 
            // push the new evaluation if there is one
            // save new manufacturer name to product
            // push new category and manufacturer
            await ProductFacade.UpdateAsync(await UpdateProduct());
        }

        public void categorySelect(ChangeEventArgs e)
        {
            if (e.Value.ToString() == "new category")
                createNewCategory = true;
            else
            {
                createNewCategory = false;
                product.CategoryName = e.Value.ToString();
            }
        }

        public void manufacturerSelect(ChangeEventArgs e)
        {
            if (e.Value.ToString() == "new manufacturer")
            {
                createNewManufacturer = true;
                newManufacturer = new ManufacturerNewModel();
            }
            else
            {
                createNewManufacturer = false;
                product.ManufacturerName = e.Value.ToString();
            }
        }

        public void AddEvaluation()
        {
            // TODO - tadyto nefunguje, ten btn odkazuje na SaveData, idk why...

            Debug.WriteLine("Adding evaluation is working");
            if (createNewEvaluation)
            {
                Debug.WriteLine("Evaluation percent is: " + NewEvaluation.ProductId + ".");

                // There is one already, should be pushed or something.
            }
            else
            {
                createNewEvaluation = true;
            }
        }

        public async Task<ProductUpdateModel> UpdateProduct()
        {
            ProductUpdateModel productUpdateModel = new ProductUpdateModel()
            {
                Id = product.Id,
                Name = product.Name,
                Photo = product.Photo,
                Description = product.Description,
                Price = product.Price,
                Weight = product.Weight,
                CountInStock = product.CountInStock,
                RAM = product.RAM,
                CPU = product.CPU,
                GPU = product.GPU,
                HDD = product.HDD,
                ManufacturerId = await DecideNewManufacturer(),
                CategoryId = await DecideNewCategory(),
                Evaluations = 
                new List<EvaluationUpdateModel>()
                // Evaluations = product.Evaluations
                //Evaluations = ListOfEvalutaion()
            };
            return productUpdateModel;
        }

        public async Task<Guid> DecideNewManufacturer()
        {
            foreach (var manufacturer in Manufacturers)
            {
                if (manufacturer.Name == product.ManufacturerName)
                    return manufacturer.Id;
            }

            // create new Manufacturer 

            return Guid.Empty;
        }

        public async Task<Guid> DecideNewCategory()
        {
            foreach (var category in Categories)
            {
                if (category.Name == product.CategoryName)
                    return category.Id;
            }

            CategoryNewModel newCategoryModel = new CategoryNewModel() { Name = product.CategoryName };
            var response =  await CategoryFacade.CreateAsync(newCategoryModel);

            Debug.WriteLine("NewCategory response: " + response.ToString());
            Debug.WriteLine("NewCategory name: " + product.CategoryName);
            return response;
        }

        public Guid FindManufacturerByName(string ManufacturerName)
        {
            foreach(var manufacturer in Manufacturers)
            {
                if (manufacturer.Name == ManufacturerName)
                    return manufacturer.Id;
            }

            return Guid.Empty;  // shouldn't come to this
        }

        public Guid FindCategoryByName(string CategoryName)
        {
            foreach (var category in Categories)
            {
                if (category.Name == CategoryName)
                    return category.Id;
            }

            return Guid.Empty;  // shouldn't come to this
        }
    }
}
