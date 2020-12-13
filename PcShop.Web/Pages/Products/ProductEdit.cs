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
        private readonly EvaluationNewModel EMPTY_EVALUATION = new EvaluationNewModel();

        private ManufacturerNewModel newManufacturer = new ManufacturerNewModel();

        //private ICollection<ProductListModel> Products { get; set; } = new List<ProductListModel>();

        //private ICollection<ProductListModel> AllProducts { get; set; } = new List<ProductListModel>();
        private ICollection<ManufacturerListModel> Manufacturers { get; set; } = new List<ManufacturerListModel>();
        private ICollection<CategoryListModel> Categories { get; set; } = new List<CategoryListModel>();
        private ICollection<EvaluationListModel> Evaluations { get; set; } = new List<EvaluationListModel>();
        private List<EvaluationNewModel> EvaluationNews { get; set; } = new List<EvaluationNewModel>();

        protected override async Task OnInitializedAsync()
        {
            if (Id == Guid.Empty)
                product = new ProductDetailModel();
            else
                product = await ProductFacade.GetProductAsync(Id);

            Manufacturers = await ManufacturerFacade.GetManufacturersAsync();
            Categories = await CategoryFacade.GetCategorysAsync();
            //Evaluations = await EvaluationsFacade.GetEvaluationsAsync();
            EvaluationNews = DetailEvaluationsToNews();

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
            // evaluationNews pres product.evaluation (ktery pridat a ktery nechat) to evaluation upgrade
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

        /*public async void AddEvaluation()
        {
            Debug.WriteLine("Adding evaluation is working");
            Debug.WriteLine("Evaluations.Count: " + Evaluations.Count);
            if (createNewEvaluation)
            {
                Debug.WriteLine("Evaluation percent is: " + NewEvaluation.PercentEvaluation + ".");

                if (NewEvaluation.TextEvaluation != EMPTY_EVALUATION.TextEvaluation)
                {
                    // There is one already, should be pushed and saved to the list
                    Debug.WriteLine("The new vec is here");
                    Debug.WriteLine("text of the new vec: " + NewEvaluation.TextEvaluation + ".");
                    Debug.WriteLine("percent of the new vec: " + NewEvaluation.PercentEvaluation + ".");
                    NewEvaluation.ProductId = product.Id;

                    Guid newEvaluationId = await EvaluationsFacade.CreateAsync(NewEvaluation);

                    // EvaluationDetailModel newEvaluationDetail = await EvaluationsFacade.GetEvaluationAsync(newEvaluationId);

                    // actualize list of all evaluations
                    Evaluations = await EvaluationsFacade.GetEvaluationsAsync();

                    Debug.WriteLine("Evaluations.Count: " + Evaluations.Count);

                    NewEvaluation = new EvaluationNewModel();
                }
                else
                {
                    Debug.WriteLine("else aaaaaaaaaaaaaaaa");
                }
            }
            else
            {
                createNewEvaluation = true;
            }
        }*/

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
            List<EvaluationUpdateModel> listEvaluationUpdate = new List<EvaluationUpdateModel>();
            foreach (var evaluation in product.Evaluations)
            {
                listEvaluationUpdate.Add(new EvaluationUpdateModel
                {
                    Id = evaluation.Id,
                    TextEvaluation = evaluation.TextEvaluation,
                    PercentEvaluation = evaluation.PercentEvaluation,
                    ProductId = product.Id
                });
            }
            return listEvaluationUpdate;
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
