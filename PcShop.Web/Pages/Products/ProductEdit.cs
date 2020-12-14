using PcShop.BL.Api.Models.Manufacturer;
using PcShop.BL.Api.Models.Evaluation;
using Microsoft.AspNetCore.Components;
using PcShop.BL.Api.Models.Category;
using PcShop.BL.Api.Models.Product;
using System.Collections.Generic;
using System.Threading.Tasks;
using PcShop.WEB.BL.Facades;
using System;

namespace PcShop.Web.Pages.Products
{
    public partial class ProductEdit : ComponentBase
    {
        [Parameter]
        public Guid Id { get; set; } = Guid.Empty;

        private ProductDetailModel Product { get; set; }

        [Inject] private ProductsFacade ProductFacade { get; set; }
        [Inject] private ManufacturersFacade ManufacturerFacade { get; set; }
        [Inject] private CategoriesFacade CategoryFacade { get; set; }

        private bool _createNewCategory = false;
        private bool _createNewManufacturer = false;
        private EvaluationNewModel _newEvaluation = new EvaluationNewModel();
        private readonly EvaluationNewModel _emptyEvaluation = new EvaluationNewModel();

        private ManufacturerNewModel _newManufacturer = new ManufacturerNewModel();
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
                Product = new ProductDetailModel()
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
                Product = await ProductFacade.GetProductAsync(Id);
            }
            EvaluationNews = DetailEvaluationsToNews();

            await base.OnInitializedAsync();
        }

        protected async Task SaveData()
        {
            await ProductFacade.UpdateAsync(await UpdateProduct());

            _createNewCategory = _createNewManufacturer = false;
            Manufacturers = await ManufacturerFacade.GetManufacturersAsync();
            Categories = await CategoryFacade.GetCategorysAsync();
            Product.ManufacturerName = _newManufacturer.Name;
        }

        public void CategorySelect(ChangeEventArgs e)
        {
            if (e.Value.ToString() == "new category")
                _createNewCategory = true;
            else
            {
                _createNewCategory = false;
                Product.CategoryName = e.Value.ToString();
            }
        }

        public void ManufacturerSelect(ChangeEventArgs e)
        {
            if (e.Value.ToString() == "new manufacturer")
            {
                _createNewManufacturer = true;
                _newManufacturer = new ManufacturerNewModel {Logo = "default_manufacturer_logo.jpg"};
            }
            else
            {
                _createNewManufacturer = false;
                Product.ManufacturerName = e.Value.ToString();
            }
        }

        public async Task<ProductUpdateModel> UpdateProduct()
        {
            ProductUpdateModel productUpdateModel = new ProductUpdateModel()
            {
                Id = Product.Id,
                Name = Product.Name,
                Photo = Product.Photo,
                Description = Product.Description,
                Price = Product.Price,
                Weight = Product.Weight,
                CountInStock = Product.CountInStock,
                Ram = Product.Ram,
                Cpu = Product.Cpu,
                Gpu = Product.Gpu,
                Hdd = Product.Hdd,
                ManufacturerId = await DecideNewManufacturer(),
                CategoryId = await DecideNewCategory(),
                Evaluations = GetProductUpdateEvaluations()
            };
            return productUpdateModel;
        }

        public async Task<Guid> DecideNewManufacturer()
        {
            Manufacturers = await ManufacturerFacade.GetManufacturersAsync();

            if (!_createNewManufacturer)
            {
                foreach (var manufacturer in Manufacturers)
                {
                    if (manufacturer.Name == Product.ManufacturerName)
                        return manufacturer.Id;
                }
            }

            // create new Manufacturer
            Guid response = await ManufacturerFacade.CreateAsync(_newManufacturer);
            return response;
        }

        public async Task<Guid> DecideNewCategory()
        {
            foreach (var category in Categories)
            {
                if (category.Name == Product.CategoryName)
                    return category.Id;
            }

            CategoryNewModel newCategoryModel = new CategoryNewModel() { Name = Product.CategoryName };
            var response = await CategoryFacade.CreateAsync(newCategoryModel);

            return response;
        }

        public Guid FindManufacturerByName(string manufacturerName)
        {
            foreach (var manufacturer in Manufacturers)
            {
                if (manufacturer.Name == manufacturerName)
                    return manufacturer.Id;
            }
            return Guid.Empty;  // shouldn't come to this
        }

        public Guid FindCategoryByName(string categoryName)
        {
            foreach (var category in Categories)
            {
                if (category.Name == categoryName)
                    return category.Id;
            }
            return Guid.Empty;  // shouldn't come to this
        }

        public List<EvaluationUpdateModel> GetProductUpdateEvaluations()
        {
            List<EvaluationUpdateModel> evaluationUpdateList = new List<EvaluationUpdateModel>();

            Product.Evaluations = new List<EvaluationListModel>();

            foreach (var evaluation in EvaluationNews)
            {
                Product.Evaluations.Add(new EvaluationListModel
                {
                    TextEvaluation = evaluation.TextEvaluation,
                    PercentEvaluation = evaluation.PercentEvaluation,
                    ProductName = Product.Name
                });
                evaluationUpdateList.Add(new EvaluationUpdateModel
                {
                    TextEvaluation = evaluation.TextEvaluation,
                    PercentEvaluation = evaluation.PercentEvaluation,
                    ProductId = Product.Id
                });
            }
            return evaluationUpdateList;
        }

        public void DeleteEvaluation(EvaluationNewModel evaluation)
        {
            var evaluationIndex = EvaluationNews.IndexOf(evaluation);
            EvaluationNews.RemoveAt(evaluationIndex);
        }

        public void AddEvaluation()
        {
            if (_newEvaluation.TextEvaluation != "")
            {
                EvaluationNews.Add(_newEvaluation);
                _newEvaluation = new EvaluationNewModel();
            }
        }

        public List<EvaluationNewModel> DetailEvaluationsToNews()
        {
            List<EvaluationNewModel> evaluationNewModels = new List<EvaluationNewModel>();

            foreach (var evaluation in Product.Evaluations)
            {
                evaluationNewModels.Add(new EvaluationNewModel
                {
                    TextEvaluation = evaluation.TextEvaluation,
                    PercentEvaluation = evaluation.PercentEvaluation,
                    ProductId = Product.Id,
                });
            }

            return evaluationNewModels;
        }
    }
}
