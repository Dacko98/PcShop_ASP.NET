using AutoMapper;
using PcShop.DAL.Entities;
using PcShop.DAL.Repositories;
using System;
using System.Collections.Generic;
using PcShop.BL.Api.Facades.Interfaces;
using PcShop.BL.Api.Models.Goods;
using PcShop.DAL.Entities;

namespace PcShop.BL.Api.Facades
{
    public class GoodsFacade : IAppFacade
    {
        private readonly GoodsRepository goodsRepository;
        private readonly IMapper mapper;

        public GoodsFacade(
            GoodsRepository goodsRepository,
            IMapper mapper)
        {
            this.goodsRepository = goodsRepository;
            this.mapper = mapper;
        }

        public List<GoodsListModel> GetAll()
        {
            return mapper.Map<List<GoodsListModel>>(goodsRepository.GetAll());
        }

        public GoodsDetailModel GetById(Guid id)
        {
            return mapper.Map<GoodsDetailModel>(goodsRepository.GetById(id));
        }

        public Guid Create(GoodsNewModel goods)
        {
            var goodsEntity = mapper.Map<GoodsEntity>(goods);
            return goodsRepository.Insert(goodsEntity);
        }

        public Guid? Update(GoodsUpdateModel goods)
        {
            var goodsEntity = mapper.Map<GoodsEntity>(goods);
            return goodsRepository.Update(goodsEntity);
        }

        public void Delete(Guid id)
        {
            goodsRepository.Remove(id);
        }
    }
}