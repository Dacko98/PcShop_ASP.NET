using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PcShop.BL.Api.Facades.Interfaces;
using PcShop.BL.Api.Models.Product;
using PcShop.Console.Api;

namespace PcShop.WEB.BL.Facades
{
    public class ProductsFacade : IAppFacade
    {
        private readonly IProductClient productClient;

        public ProductsFacade(IProductClient productClient)
        {
            this.productClient = productClient;
        }

        public async Task<ICollection<ProductListModel>> GetProductsAsync()
        {
            return await productClient.ProductGetAsync();
        }

        public async Task<ProductDetailModel> GetProductAsync(Guid id)
        {
            return await productClient.ProductGetAsync(id);
        }

        public async Task<Guid> CreateAsync(ProductNewModel data)
        {
            return await productClient.ProductPostAsync(data);
        }

        public async Task<Guid> UpdateAsync(ProductUpdateModel data)
        {
            return await productClient.ProductPutAsync(data);
        }

        public async Task DeleteAsync(Guid id)
        {
            await productClient.ProductDeleteAsync(id);
        }
    }
}