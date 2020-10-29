using AutoMapper;
using PcShop.DAL.Entities;
using PcShop.DAL.Repositories;
using System;
using System.Collections.Generic;
using PcShop.BL.Api.Facades.Interfaces;
using PcShop.BL.Api.Models.Category;
using PcShop.DAL.Entities;

namespace PcShop.BL.Api.Facades
{
    public class CategoryFacade : IAppFacade
    {
        private readonly CategoryRepository categoryRepository;
        private readonly IMapper mapper;

        public CategoryFacade(
            CategoryRepository categoryRepository,
            IMapper mapper)
        {
            this.categoryRepository = categoryRepository;
            this.mapper = mapper;
        }

        public List<CategoryListModel> GetAll()
        {
            return mapper.Map<List<CategoryListModel>>(categoryRepository.GetAll());
        }

        public CategoryDetailModel GetById(Guid id)
        {
            return mapper.Map<CategoryDetailModel>(categoryRepository.GetById(id));
        }

        public Guid Create(CategoryNewModel category)
        {
            var categoryEntity = mapper.Map<CategoryEntity>(category);
            return categoryRepository.Insert(categoryEntity);
        }

        public Guid? Update(CategoryUpdateModel category)
        {
            var categoryEntity = mapper.Map<CategoryEntity>(category);
            return categoryRepository.Update(categoryEntity);
        }

        public void Delete(Guid id)
        {
            categoryRepository.Remove(id);
        }
    }
}