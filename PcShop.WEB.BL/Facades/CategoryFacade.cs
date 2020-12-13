using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PcShop.BL.Api.Facades.Interfaces;
using PcShop.BL.Api.Models.Category;
using PcShop.Console.Api;

namespace PcShop.WEB.BL.Facades
{
    public class CategoriesFacade : IAppFacade
    {
        private readonly ICategoryClient _categoryClient;

        public CategoriesFacade(ICategoryClient categoryClient)
        {
            this._categoryClient = categoryClient;
        }

        public async Task<ICollection<CategoryListModel>> GetCategorysAsync()
        {
            return await _categoryClient.CategoryGetAsync();
        }

        public async Task<CategoryDetailModel> GetCategoryAsync(Guid id)
        {
            return await _categoryClient.CategoryGetAsync(id);
        }

        public async Task<Guid> CreateAsync(CategoryNewModel data)
        {
            return await _categoryClient.CategoryPostAsync(data);
        }

        public async Task<Guid> UpdateAsync(CategoryUpdateModel data)
        {
            return await _categoryClient.CategoryPutAsync(data);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _categoryClient.CategoryDeleteAsync(id);
        }
    }
}