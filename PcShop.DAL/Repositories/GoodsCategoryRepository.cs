using AutoMapper;
using PcShop.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using PcShop.DAL.Entities;

namespace PcShop.DAL.Repositories
{
    public class GoodsCategoryRepository : IAppRepository<GoodsCategoryEntity>
    {
        private readonly IList<GoodsCategoryEntity> goodsCategories;
        private readonly IMapper mapper;

        public GoodsCategoryRepository(
            Storage storage,
            IMapper mapper)
        {
           // goodsCategories = storage.GoodsCategories;
            this.mapper = mapper;
        }

        public IList<GoodsCategoryEntity> GetAll()
        {
            return goodsCategories;
        }

        public GoodsCategoryEntity GetById(Guid id)
        {
            return goodsCategories.SingleOrDefault(entity => entity.Id == id);
        }

        public Guid Insert(GoodsCategoryEntity goodsCategory)
        {
            goodsCategory.Id = Guid.NewGuid();
            goodsCategories.Add(goodsCategory);
            return goodsCategory.Id;
        }

        public Guid? Update(GoodsCategoryEntity goodsCategoryUpdated)
        {
            var goodsCategoryExisting = goodsCategories.SingleOrDefault(goodsCategoryInStorage => goodsCategoryInStorage.Id == goodsCategoryUpdated.Id);
            if (goodsCategoryExisting != null)
            {
                mapper.Map(goodsCategoryUpdated, goodsCategoryExisting);
            }

            return goodsCategoryExisting?.Id;
        }

        public void Remove(Guid id)
        {
            var goodsCategoryToRemove = goodsCategories.Single(goodsCategory => goodsCategory.Id.Equals(id));
            goodsCategories.Remove(goodsCategoryToRemove);
        }
    }
}