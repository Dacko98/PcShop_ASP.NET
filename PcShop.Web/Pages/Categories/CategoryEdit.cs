using Microsoft.AspNetCore.Components;
using PcShop.BL.Api.Models.Category;
using System.Threading.Tasks;
using PcShop.WEB.BL.Facades;
using System;
using System.Diagnostics;

namespace PcShop.Web.Pages.Categories
{
    public partial class CategoryEdit : ComponentBase
    {
        [Inject] private CategoriesFacade CategoryFacade { get; set; }

        [Parameter] public Guid Id { get; set; }

        public CategoryDetailModel Category { get; set; }

        private bool NewCategory { get; set; }

        protected override async Task OnInitializedAsync()
        {
            NewCategory = Id == Guid.Empty;
            Category = NewCategory ? new CategoryDetailModel() : await CategoryFacade.GetCategoryAsync(Id);

            await base.OnInitializedAsync();
        }

        protected async Task SaveData()
        {
            Guid response;
            if (NewCategory)
                response = await CategoryFacade.CreateAsync(new CategoryNewModel {Name = Category.Name});
            else
                response = await CategoryFacade.UpdateAsync(new CategoryUpdateModel {Name = Category.Name, Id = Id});

            Debug.WriteLine("Data should be saved    " + Id + "   " + Category.Name);
            Debug.WriteLine("Saving returns:   " + response);
        }
    }
}
