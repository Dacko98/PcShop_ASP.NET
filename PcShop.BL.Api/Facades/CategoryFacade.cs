using AutoMapper;
using PcShop.DAL.Entities;
using PcShop.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using PcShop.BL.Api.Facades.Interfaces;
using PcShop.BL.Api.Models.Category;
using PcShop.BL.Api.Models.Product;

namespace PcShop.BL.Api.Facades
{
    public class CategoryFacade : IAppFacade
    {
        private readonly CategoryRepository _categoryRepository;
        private readonly ProductRepository _productRepository;
        private readonly ProductFacade _productFacade;
        private readonly IMapper _mapper;

        public CategoryFacade(
            ProductRepository productRepository,
            CategoryRepository categoryRepository,
            ProductFacade productFacade,
            IMapper mapper)
        {
            this._categoryRepository = categoryRepository;
            this._mapper = mapper;
            this._productFacade = productFacade;
            this._productRepository = productRepository;
        }

        public List<CategoryListModel> GetAll()
        {
            return _mapper.Map<List<CategoryListModel>>(_categoryRepository.GetAll());
        }

        public CategoryDetailModel GetById(Guid id)
        {
            var categoryEntity = _categoryRepository.GetById(id);
            if (categoryEntity == null)
            {
                return null;
            }
            categoryEntity.Product = _productRepository.GetByCategoryId(id);
            return _mapper.Map<CategoryDetailModel>(categoryEntity);
        }

        public Guid Create(CategoryNewModel category)
        {
            var categoryEntity = _mapper.Map<CategoryEntity>(category);
            return _categoryRepository.Insert(categoryEntity);
        }

        public Guid? Update(CategoryUpdateModel categoryUpdateModel)
        {
            var categoryEntityExisting = _categoryRepository.GetById(categoryUpdateModel.Id);
            if (categoryEntityExisting == null)
            {
                return null;
            }
            categoryEntityExisting.Product = _productRepository.GetByCategoryId(categoryUpdateModel.Id);
            UpdateCategory(categoryUpdateModel, categoryEntityExisting);

            var categoryEntityUpdated = _mapper.Map<CategoryEntity>(categoryUpdateModel);
            return _categoryRepository.Update(categoryEntityUpdated);
        }

        private void UpdateCategory(CategoryUpdateModel categoryUpdateModel, CategoryEntity categoryEntity)
        {
            var productToRemove = categoryEntity.Product.Where(product =>
                !categoryUpdateModel.Product.Any(products => products.Id == product.Id));
        
            foreach (var product in productToRemove)
            {
                product.CategoryId = Guid.Empty;
                product.Category = null;
                _productRepository.Update(product);
            }
        
            var productToAdd = categoryUpdateModel.Product.Where(
                product => !categoryEntity.Product.Any(products => products.Id == product.Id));


            foreach (var product in productToAdd)
            {
                var goodEntity = _productRepository.GetById(product.Id);
                goodEntity.CategoryId = categoryUpdateModel.Id;
                _productRepository.Update(goodEntity);
            }
            
        }

        public void Delete(Guid id)
        {
            var categoryEntity = _categoryRepository.GetById(id);
            categoryEntity.Product = _productRepository.GetByCategoryId(id);
            foreach (var product in categoryEntity.Product)
            {
                product.CategoryId = Guid.Empty;
                product.Category = null;
            }
            _categoryRepository.Remove(id);
        }
    }
}