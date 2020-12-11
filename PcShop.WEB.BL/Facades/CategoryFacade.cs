using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PcShop.BL.Api.Facades.Interfaces;
using PcShop.BL.Api.Models.Category;
using PcShop.Console.Api;

namespace PcShop.WEB.BL.Facades
{
    public class CategorysFacade : IAppFacade
    {
        private readonly ICategoryClient categoryClient;

        public CategorysFacade(ICategoryClient categoryClient)
        {
            this.categoryClient = categoryClient;
        }

        public async Task<ICollection<CategoryListModel>> GetCategorysAsync()
        {
            return await categoryClient.CategoryGetAsync();
        }

        public async Task<CategoryDetailModel> GetCategoryAsync(Guid id)
        {
            return await categoryClient.CategoryGetAsync(id);
        }

        public async Task<Guid> CreateAsync(CategoryNewModel data)
        {
            return await categoryClient.CategoryPostAsync(data);
        }

        public async Task<Guid> UpdateAsync(CategoryUpdateModel data)
        {
            return await categoryClient.CategoryPutAsync(data);
        }

        public async Task DeleteAsync(Guid id)
        {
            await categoryClient.CategoryDeleteAsync(id);
        }
    }
}