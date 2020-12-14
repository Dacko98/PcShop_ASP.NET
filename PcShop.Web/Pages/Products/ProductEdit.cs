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
        public Guid Id { get; set; } = Guid.Empty;

        public ProductDetailModel product { get; set; }

        [Inject]
        private ProductsFacade ProductFacade { get; set; }
        [Inject]
        private ManufacturersFacade ManufacturerFacade { get; set; }
        [Inject]
        private CategoriesFacade CategoryFacade { get; set; }
        [Inject]
        private EvaluationsFacade EvaluationsFacade { get; set; }

        private bool createNewProduct = false;
        private bool createNewCategory = false;
        private bool createNewManufacturer = false;
        // private bool createNewEvaluation = false;
        private EvaluationNewModel NewEvaluation = new EvaluationNewModel();
        private readonly EvaluationNewModel EMPTY_EVALUATION = new EvaluationNewModel();

        private ManufacturerNewModel newManufacturer = new ManufacturerNewModel();

        //private ICollection<ProductListModel> Products { get; set; } = new List<ProductListModel>();

        //private ICollection<ProductListModel> AllProducts { get; set; } = new List<ProductListModel>();
        private ICollection<ManufacturerListModel> Manufacturers { get; set; } = new List<ManufacturerListModel>();
        private ICollection<CategoryListModel> Categories { get; set; } = new List<CategoryListModel>();
        //private List<EvaluationListModel> Evaluations { get; set; } = new List<EvaluationListModel>();
        private List<EvaluationNewModel> EvaluationNews { get; set; } = new List<EvaluationNewModel>();

        protected override async Task OnInitializedAsync()
        {
            Manufacturers = await ManufacturerFacade.GetManufacturersAsync();
            Categories = await CategoryFacade.GetCategorysAsync();
            Debug.WriteLine("ManufacturersCount: " + Manufacturers.Count);
            

            if (Id == Guid.Empty)
            {
                Debug.WriteLine("if good.");

                var newProductId = await ProductFacade.CreateAsync(new ProductNewModel());

                product = new ProductDetailModel()
                {
                    Id = newProductId,
                    Name = "",
                    Photo = "default.jpg",
                    Description = "",
                    Price = 0,
                    Weight = 0,
                    CountInStock = 0,
                    Evaluations = new List<EvaluationListModel>()
                };

                createNewProduct = true;

                Debug.WriteLine("Photo: " + product.Photo);

            }
            else
            {
                Debug.WriteLine("else aaaaaaaa");
                product = await ProductFacade.GetProductAsync(Id);
            }

            EvaluationNews = DetailEvaluationsToNews();

            await base.OnInitializedAsync();
        }

        protected async Task SaveData()
        {
            Debug.WriteLine("Data should be saved");

            // evaluationNews pres product.evaluation (ktery pridat a ktery nechat) to evaluation upgrade

            // actualize all evaluations 
            // push the new evaluation if there is one
            // save new manufacturer name to product
            // push new category and manufacturer
            
            await ProductFacade.UpdateAsync(await UpdateProduct());

            createNewCategory = createNewManufacturer = false;
            Manufacturers = await ManufacturerFacade.GetManufacturersAsync();
            Categories = await CategoryFacade.GetCategorysAsync();
            product.ManufacturerName = newManufacturer.Name;
            // UpdateAsync(product)
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
                Evaluations = GetProductUpdateEvaluations()
            };
            return productUpdateModel;
        }

        public async Task<Guid> DecideNewManufacturer()
        {
            Manufacturers = await ManufacturerFacade.GetManufacturersAsync();

            if (!createNewManufacturer)
            {
                foreach(var manufacturer in Manufacturers)
                {
                    if (manufacturer.Name == product.ManufacturerName)
                        return manufacturer.Id;
                }
            }

            // create new Manufacturer
            var response = await ManufacturerFacade.CreateAsync(newManufacturer);

            Debug.WriteLine("NewManufacturer response: " + response.ToString());
            Debug.WriteLine("NewManufacturer name: " + product.CategoryName);
            Debug.WriteLine("ManufacturersCount: " + Manufacturers.Count);
            return response;
        }

        public async Task<Guid> DecideNewCategory()
        {
            foreach (var category in Categories)
            {
                if (category.Name == product.CategoryName)
                    return category.Id;
            }

            CategoryNewModel newCategoryModel = new CategoryNewModel() { Name = product.CategoryName };
            var response = await CategoryFacade.CreateAsync(newCategoryModel);

            Debug.WriteLine("NewCategory response: " + response.ToString());
            Debug.WriteLine("NewCategory name: " + product.CategoryName);
            return response;
        }

        public Guid FindManufacturerByName(string ManufacturerName)
        {
            foreach (var manufacturer in Manufacturers)
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

        public List<EvaluationUpdateModel> GetProductUpdateEvaluations()
        {
            // delete all evalutaions in product.evaluations and create new ones

            List<EvaluationUpdateModel> EvaluationUpdateList = new List<EvaluationUpdateModel>();

            product.Evaluations = new List<EvaluationListModel>();

            foreach (var evaluation in EvaluationNews)
            {
                // maybe first create this evaluation, idk...


                product.Evaluations.Add(new EvaluationListModel 
                { 
                    TextEvaluation = evaluation.TextEvaluation,
                    PercentEvaluation = evaluation.PercentEvaluation,
                    ProductName = product.Name
                });

                EvaluationUpdateList.Add(new EvaluationUpdateModel
                {
                    TextEvaluation = evaluation.TextEvaluation,
                    PercentEvaluation = evaluation.PercentEvaluation,
                    ProductId = product.Id
                });
            }
            return EvaluationUpdateList;
        }

        public void DeleteEvaluation(EvaluationNewModel evaluation)
        {
            var evaluationIndex = EvaluationNews.IndexOf(evaluation);
            EvaluationNews.RemoveAt(evaluationIndex);
            Debug.WriteLine("Deleting this evaluation");
        }

        public void AddEvaluation()
        {
            if(NewEvaluation.TextEvaluation != "")
            {
                EvaluationNews.Add(NewEvaluation);
                NewEvaluation = new EvaluationNewModel();
            }
        }

        public List<EvaluationNewModel> DetailEvaluationsToNews()
        {
            List<EvaluationNewModel> evaluationNewModels = new List<EvaluationNewModel>();

            foreach(var evaluation in product.Evaluations)
            {
                evaluationNewModels.Add(new EvaluationNewModel
                {
                    TextEvaluation = evaluation.TextEvaluation,
                    PercentEvaluation = evaluation.PercentEvaluation,
                    ProductId = product.Id,
                }) ;
            }

            return evaluationNewModels;
        }
    }
}
