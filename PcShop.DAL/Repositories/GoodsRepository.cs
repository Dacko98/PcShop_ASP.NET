using AutoMapper;
using PcShop.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PcShop.DAL.Repositories
{
    public class GoodsRepository : IAppRepository<GoodsEntity>
    {
        private readonly IList<GoodsEntity> moregoods;
        private readonly IMapper mapper;

        public GoodsRepository(
            Storage storage,
            IMapper mapper)
        {
            moregoods = storage.Goods;
            this.mapper = mapper;
        }

        public IList<GoodsEntity> GetAll()
        {
            return moregoods;
        }

        public GoodsEntity GetById(Guid id)
        {
            return moregoods.SingleOrDefault(entity => entity.Id == id);
        }

        public Guid Insert(GoodsEntity goods)
        {
            goods.Id = Guid.NewGuid();
            moregoods.Add(goods);
            return goods.Id;
        }
        public IList<GoodsEntity> GetByCategoryId(Guid categoryId)
        {
            return moregoods.Where(goods => goods.CategoryId == categoryId).ToList();
        }

        public IList<GoodsEntity> GetByManufacturerId(Guid manufacturerId)
        {
            return moregoods.Where(goods => goods.ManufacturerId == manufacturerId).ToList();
        }

        public Guid? Update(GoodsEntity goodsUpdated)
        {
            var goodsExisting = moregoods.SingleOrDefault(goodsInStorage => goodsInStorage.Id == goodsUpdated.Id);
            if (goodsExisting != null)
            {
                mapper.Map(goodsUpdated, goodsExisting);
            }

            return goodsExisting?.Id;
        }

        public void Remove(Guid id)
        {
            var goodsToRemove = moregoods.Single(goods => goods.Id.Equals(id));
            moregoods.Remove(goodsToRemove);
        }
    }
}