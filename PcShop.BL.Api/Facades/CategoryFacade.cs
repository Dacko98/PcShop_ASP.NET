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
        private readonly CategoryRepository categoryRepository;
        private readonly ProductRepository productRepository;
        private readonly ProductFacade productFacade;
        private readonly IMapper mapper;

        public CategoryFacade(
            ProductRepository productRepository,
            CategoryRepository categoryRepository,
            ProductFacade productFacade,
            IMapper mapper)
        {
            this.categoryRepository = categoryRepository;
            this.mapper = mapper;
            this.productFacade = productFacade;
            this.productRepository = productRepository;
        }

        public List<CategoryListModel> GetAll()
        {
            return mapper.Map<List<CategoryListModel>>(categoryRepository.GetAll());
        }

        public CategoryDetailModel GetById(Guid id)
        {
            var categoryEntity = categoryRepository.GetById(id);
            if (categoryEntity == null)
            {
                return null;
            }
            categoryEntity.Product = productRepository.GetByCategoryId(id);
            return mapper.Map<CategoryDetailModel>(categoryEntity);
        }

        public Guid Create(CategoryNewModel category)
        {
            var categoryEntity = mapper.Map<CategoryEntity>(category);
            return categoryRepository.Insert(categoryEntity);
        }

        public Guid? Update(CategoryUpdateModel categoryUpdateModel)
        {
            var categoryEntityExisting = categoryRepository.GetById(categoryUpdateModel.Id);
            if (categoryEntityExisting == null)
            {
                return null;
            }
            categoryEntityExisting.Product = productRepository.GetByCategoryId(categoryUpdateModel.Id);
            UpdateCategory(categoryUpdateModel, categoryEntityExisting);

            var categoryEntityUpdated = mapper.Map<CategoryEntity>(categoryUpdateModel);
            return categoryRepository.Update(categoryEntityUpdated);
        }

        private void UpdateCategory(CategoryUpdateModel categoryUpdateModel, CategoryEntity categoryEntity)
        {
            var productToRemove = categoryEntity.Product.Where(product =>
                !categoryUpdateModel.Product.Any(products => products.Id == product.Id));
        
            foreach (var product in productToRemove)
            {
                product.CategoryId = Guid.Empty;
                product.Category = null;
                productRepository.Update(product);
            }
        
            var productToAdd = categoryUpdateModel.Product.Where(
                product => !categoryEntity.Product.Any(products => products.Id == product.Id));


            foreach (var product in productToAdd)
            {
                var goodEntity = productRepository.GetById(product.Id);
                goodEntity.CategoryId = categoryUpdateModel.Id;
                productRepository.Update(goodEntity);
            }
            
        }

        public void Delete(Guid id)
        {
            var categoryEntity = categoryRepository.GetById(id);
            categoryEntity.Product = productRepository.GetByCategoryId(id);
            foreach (var product in categoryEntity.Product)
            {
                product.CategoryId = Guid.Empty;
                product.Category = null;
            }
            categoryRepository.Remove(id);
        }
    }
}