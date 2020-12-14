using PcShop.BL.Api.Models.Manufacturer;
using PcShop.BL.Api.Models.Evaluation;
using Microsoft.AspNetCore.Components;
using PcShop.BL.Api.Models.Category;
using PcShop.BL.Api.Models.Product;
using System.Collections.Generic;
using System.Threading.Tasks;
using PcShop.WEB.BL.Facades;
using System.Linq;
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
        [Inject] NavigationManager UriHelper { get; set; }

        private bool _save = true;
        private bool _createNewCategory = false;
        private bool _createNewManufacturer = false;
        private EvaluationNewModel _newEvaluation = new EvaluationNewModel();
        private readonly EvaluationNewModel _emptyEvaluation = new EvaluationNewModel();
        private string _newEvalutionMessage = "";

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
                    Evaluations = new List<EvaluationListModel>(),
                    ManufacturerName = Manufacturers.ToList().Any() ? Manufacturers.ToList().First().Name : "",
                    CategoryName = Categories.ToList().Any() ? Categories.ToList().First().Name : ""
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
            if (_save)
            {
                await ProductFacade.UpdateAsync(await UpdateProduct());
                UriHelper.NavigateTo("/product/" + Product.Id);
            }
            else
                _save = true;
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

            if (! _createNewManufacturer)
            {
                return Manufacturers.ToList().Find(manufacturer => manufacturer.Name == Product.ManufacturerName).Id;
            }

            // create new Manufacturer
            Guid response = await ManufacturerFacade.CreateAsync(_newManufacturer);
            return response;
        }

        public async Task<Guid> DecideNewCategory()
        {
            var OldCategory = Categories.ToList().Find(category => category.Name == Product.CategoryName);
            if (OldCategory != null)
            {
                return OldCategory.Id;
            }

            // create new category
            CategoryNewModel newCategoryModel = new CategoryNewModel() { Name = Product.CategoryName };
            var response = await CategoryFacade.CreateAsync(newCategoryModel);

            return response;
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
            if (_newEvaluation.PercentEvaluation >= 0 && 
                _newEvaluation.PercentEvaluation <= 100)
            {
                _newEvalutionMessage = "";
                EvaluationNews.Add(_newEvaluation);
                _newEvaluation = new EvaluationNewModel();
            }
            else
            {
                _newEvalutionMessage = "Evaluation out of range, insert number between 0 and 100";
            }
            _save = false;
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

        protected async Task DeleteEntity()
        {
            await ProductFacade.DeleteAsync(Product.Id);
            UriHelper.NavigateTo("/deleted/products");
        }
    }
}
