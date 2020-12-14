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

        private ProductDetailModel product { get; set; }

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
        private EvaluationNewModel NewEvaluation = new EvaluationNewModel();
        private readonly EvaluationNewModel EMPTY_EVALUATION = new EvaluationNewModel();

        private ManufacturerNewModel newManufacturer = new ManufacturerNewModel();
        private ICollection<ManufacturerListModel> Manufacturers { get; set; } = new List<ManufacturerListModel>();
        private ICollection<CategoryListModel> Categories { get; set; } = new List<CategoryListModel>();
        private List<EvaluationNewModel> EvaluationNews { get; set; } = new List<EvaluationNewModel>();

        protected override async Task OnInitializedAsync()
        {
            Manufacturers = await ManufacturerFacade.GetManufacturersAsync();
            Categories = await CategoryFacade.GetCategorysAsync();
            
            if (Id == Guid.Empty)
            {
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
            }
            else
            {
                product = await ProductFacade.GetProductAsync(Id);
            }
            EvaluationNews = DetailEvaluationsToNews();

            await base.OnInitializedAsync();
        }

        protected async Task SaveData()
        {
            await ProductFacade.UpdateAsync(await UpdateProduct());

            createNewCategory = createNewManufacturer = false;
            Manufacturers = await ManufacturerFacade.GetManufacturersAsync();
            Categories = await CategoryFacade.GetCategorysAsync();
            if(createNewManufacturer)
                product.ManufacturerName = newManufacturer.Name;
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
            Guid response = await ManufacturerFacade.CreateAsync(newManufacturer);
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
            List<EvaluationUpdateModel> EvaluationUpdateList = new List<EvaluationUpdateModel>();

            product.Evaluations = new List<EvaluationListModel>();

            foreach (var evaluation in EvaluationNews)
            {
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
