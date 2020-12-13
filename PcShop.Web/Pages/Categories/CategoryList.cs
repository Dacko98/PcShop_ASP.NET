using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;
using PcShop.WEB.BL.Facades;
using PcShop.BL.Api.Models.Category;

namespace PcShop.Web.Pages.Categories
{
    public partial class CategoryList
    {
        [Inject]
        private CategoriesFacade CategoryFacade { get; set; }

        private ICollection<CategoryListModel> Category { get; set; } = new List<CategoryListModel>();
        
        protected override async Task OnInitializedAsync()
        {
            await LoadData();

            await base.OnInitializedAsync();
        }

        private async Task LoadData()
        {
            Category = await CategoryFacade.GetCategorysAsync();
        }
    }
}
