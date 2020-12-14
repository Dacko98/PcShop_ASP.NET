using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PcShop.BL.Api.Facades.Interfaces;
using PcShop.BL.Api.Models.Product;
using PcShop.Console.Api;

namespace PcShop.WEB.BL.Facades
{
    public class ProductsFacade : IAppFacade
    {
        private readonly IProductClient _productClient;

        public ProductsFacade(IProductClient productClient)
        {
            this._productClient = productClient;
        }

        public async Task<ICollection<ProductListModel>> GetProductsAsync()
        {
            return await _productClient.ProductGetAsync();
        }

        public async Task<ProductDetailModel> GetProductAsync(Guid id)
        {
            return await _productClient.ProductGetAsync(id);
        }

        public async Task<Guid> CreateAsync(ProductNewModel data)
        {
            return await _productClient.ProductPostAsync(data);
        }

        public async Task<Guid> UpdateAsync(ProductUpdateModel data)
        {
            return await _productClient.ProductPutAsync(data);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _productClient.ProductDeleteAsync(id);
        }
    }
}