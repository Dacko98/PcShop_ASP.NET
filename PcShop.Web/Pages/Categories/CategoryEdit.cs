using Microsoft.AspNetCore.Components;
using PcShop.BL.Api.Models.Category;
using System.Threading.Tasks;
using PcShop.WEB.BL.Facades;
using System;
using System.Linq;
using PcShop.BL.Api.Models.Product;

namespace PcShop.Web.Pages.Categories
{
    public partial class CategoryEdit : ComponentBase
    {
        [Inject] private CategoriesFacade CategoryFacade { get; set; }
        [Inject] private ProductsFacade ProductFacade { get; set; }
        [Inject] NavigationManager UriHelper { get; set; }

        [Parameter] public Guid Id { get; set; }

        private string CategoryOldName { get; set; }
        public CategoryDetailModel Category { get; set; }

        private bool NewCategory { get; set; }

        protected override async Task OnInitializedAsync()
        {
            NewCategory = Id == Guid.Empty;
            Category = NewCategory ? new CategoryDetailModel() : await CategoryFacade.GetCategoryAsync(Id);
            CategoryOldName = Category.Name;

            await base.OnInitializedAsync();
        }

        
        protected async Task DeleteEntity()
        {
            if (!NewCategory)
                await CategoryFacade.DeleteAsync(Id);
            UriHelper.NavigateTo("/deleted/categories");
        }

        protected async Task SaveData()
        {
            Guid response;
            if (NewCategory)
                response=await CategoryFacade.CreateAsync(new CategoryNewModel {Name = Category.Name});
            else
            {
                var products = (await ProductFacade.GetProductsAsync())
                    .ToList().FindAll(p => p.CategoryName == CategoryOldName);

                response=await CategoryFacade.UpdateAsync(new CategoryUpdateModel
                {
                    Name = Category.Name, 
                    Id = Id,
                    Product = products.Select(product => new ProductOnlyIdUpdateModel { Id = product.Id }).ToList()
                });
            }

            UriHelper.NavigateTo("/category/" + response);
        }
    }
}