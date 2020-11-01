using AutoMapper;
using PcShop.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PcShop.DAL.Repositories
{
    public class ProductRepository : IAppRepository<ProductEntity>
    {
        private readonly IList<ProductEntity> products;
        private readonly IMapper mapper;

        public ProductRepository(
            Storage storage,
            IMapper mapper)
        {
            products = storage.Product;
            this.mapper = mapper;
        }

        public IList<ProductEntity> GetAll()
        {
            return products;
        }

        public ProductEntity GetById(Guid id)
        {
            return products.SingleOrDefault(entity => entity.Id == id);
        }

        public Guid Insert(ProductEntity product)
        {
            product.Id = Guid.NewGuid();
            products.Add(product);
            return product.Id;
        }
        public IList<ProductEntity> GetByCategoryId(Guid categoryId)
        {
            return products.Where(product => product.CategoryId == categoryId).ToList();
        }

        public IList<ProductEntity> GetByManufacturerId(Guid manufacturerId)
        {
            return products.Where(product => product.ManufacturerId == manufacturerId).ToList();
        }

        public Guid? Update(ProductEntity productUpdated)
        {
            var productExisting = products.SingleOrDefault(productInStorage => productInStorage.Id == productUpdated.Id);
            if (productExisting != null)
            {
                mapper.Map(productUpdated, productExisting);
            }

            return productExisting?.Id;
        }

        public void Remove(Guid id)
        {
            var productToRemove = products.Single(product => product.Id.Equals(id));
            products.Remove(productToRemove);
        }
    }
}