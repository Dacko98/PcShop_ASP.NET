using AutoMapper;
using PcShop.DAL.Entities;
using PcShop.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using PcShop.BL.Api.Facades.Interfaces;
using PcShop.BL.Api.Models.Category;
using PcShop.BL.Api.Models.Goods;
using PcShop.DAL.Entities;

namespace PcShop.BL.Api.Facades
{
    public class CategoryFacade : IAppFacade
    {
        private readonly CategoryRepository categoryRepository;
        private readonly GoodsRepository goodsRepository;
        private readonly GoodsFacade goodsFacade;
        private readonly IMapper mapper;

        public CategoryFacade(
            GoodsRepository goodsRepository,
            CategoryRepository categoryRepository,
            GoodsFacade goodsFacade,
            IMapper mapper)
        {
            this.categoryRepository = categoryRepository;
            this.mapper = mapper;
            this.goodsFacade = goodsFacade;
            this.goodsRepository = goodsRepository;
        }

        public List<CategoryListModel> GetAll()
        {
            return mapper.Map<List<CategoryListModel>>(categoryRepository.GetAll());
        }

        public CategoryDetailModel GetById(Guid id)
        {
            var categoryEntity = categoryRepository.GetById(id);
            categoryEntity.Goods = goodsRepository.GetByCategoryId(id);
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
            categoryEntityExisting.Goods = goodsRepository.GetByCategoryId(categoryUpdateModel.Id);
            UpdateCategory(categoryUpdateModel, categoryEntityExisting);

            var categoryEntityUpdated = mapper.Map<CategoryEntity>(categoryUpdateModel);
            return categoryRepository.Update(categoryEntityUpdated);
        }

        private void UpdateCategory(CategoryUpdateModel categoryUpdateModel, CategoryEntity categoryEntity)
        {
            var goodsToRemove = categoryEntity.Goods.Where(goods =>
                !categoryUpdateModel.Goods.Any(goodss => goodss.Id == goods.Id));
        
            foreach (var goods in goodsToRemove)
            {
                goods.CategoryId = Guid.Empty;
                goods.Category = null;
                goodsRepository.Update(goods);
            }
        
            var goodsToAdd = categoryUpdateModel.Goods.Where(
                goods => !categoryEntity.Goods.Any(goodss => goodss.Id == goods.Id));


            foreach (var goods in goodsToAdd)
            {
                var goodEntity = goodsRepository.GetById(goods.Id);
                goodEntity.CategoryId = categoryUpdateModel.Id;
                goodsRepository.Update(goodEntity);
            }
            
        }

        public void Delete(Guid id)
        {
            var categoryEntity = categoryRepository.GetById(id);
            categoryEntity.Goods = goodsRepository.GetByCategoryId(id);
            foreach (var goods in categoryEntity.Goods)
            {
                goods.CategoryId = Guid.Empty;
                goods.Category = null;
            }
            categoryRepository.Remove(id);
        }
    }
}