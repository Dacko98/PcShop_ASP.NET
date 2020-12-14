using AutoMapper;
using PcShop.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PcShop.DAL.Repositories
{
    public class ProductRepository : IAppRepository<ProductEntity>
    {
        private readonly IList<ProductEntity> _products;
        private readonly IMapper _mapper;

        public ProductRepository(
            Storage storage,
            IMapper mapper)
        {
            _products = storage.Product;
            this._mapper = mapper;
        }

        public IList<ProductEntity> GetAll()
        {
            return _products;
        }

        public ProductEntity GetById(Guid id)
        {
            return _products.FirstOrDefault(entity => entity.Id == id);
        }

        public Guid Insert(ProductEntity product)
        {
            product.Id = Guid.NewGuid();
            _products.Add(product);
            return product.Id;
        }
        public IList<ProductEntity> GetByCategoryId(Guid categoryId)
        {
            return _products.Where(product => product.CategoryId == categoryId).ToList();
        }

        public IList<ProductEntity> GetByManufacturerId(Guid manufacturerId)
        {
            return _products.Where(product => product.ManufacturerId == manufacturerId).ToList();
        }

        public Guid? Update(ProductEntity productUpdated)
        {
            var productExisting = _products.SingleOrDefault(productInStorage => productInStorage.Id == productUpdated.Id);
            if (productExisting != null)
            {
                _mapper.Map(productUpdated, productExisting);
            }

            return productExisting?.Id;
        }

        public void Remove(Guid id)
        {
            var productToRemove = _products.Single(product => product.Id.Equals(id));
            _products.Remove(productToRemove);
        }
    }
}