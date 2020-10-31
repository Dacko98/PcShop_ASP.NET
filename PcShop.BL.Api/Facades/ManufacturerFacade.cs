using AutoMapper;
using PcShop.DAL.Entities;
using PcShop.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using PcShop.BL.Api.Facades.Interfaces;
using PcShop.BL.Api.Models.Manufacturer;
using PcShop.DAL.Entities;

namespace PcShop.BL.Api.Facades
{
    public class ManufacturerFacade : IAppFacade
    {
        private readonly ManufacturerRepository manufacturerRepository;
        private readonly IMapper mapper;
        private readonly GoodsRepository goodsRepository;
        private readonly GoodsFacade goodsFacade;

        public ManufacturerFacade(
            GoodsRepository goodsRepository,
            ManufacturerRepository manufacturerRepository,
            GoodsFacade goodsFacade,
            IMapper mapper)
        {
            this.manufacturerRepository = manufacturerRepository;
            this.mapper = mapper;
            this.goodsFacade = goodsFacade;
            this.goodsRepository = goodsRepository;
        }

        public List<ManufacturerListModel> GetAll()
        {
            return mapper.Map<List<ManufacturerListModel>>(manufacturerRepository.GetAll());
        }

        public ManufacturerDetailModel GetById(Guid id)
        {
            var categoryEntity = manufacturerRepository.GetById(id);
            categoryEntity.Goods = goodsRepository.GetByManufacturerId(id);
            return mapper.Map<ManufacturerDetailModel>(categoryEntity);
        }

        public Guid Create(ManufacturerNewModel manufacturer)
        {
            var manufacturerEntity = mapper.Map<ManufacturerEntity>(manufacturer);
            return manufacturerRepository.Insert(manufacturerEntity);
        }

        public Guid? Update(ManufacturerUpdateModel manufacturerUpdateModel)
        {
            var manufacturerEntityExisting = manufacturerRepository.GetById(manufacturerUpdateModel.Id);
            manufacturerEntityExisting.Goods = goodsRepository.GetByManufacturerId(manufacturerUpdateModel.Id);
            UpdateManufacturer(manufacturerUpdateModel, manufacturerEntityExisting);

            var manufacturerEntityUpdated = mapper.Map<ManufacturerEntity>(manufacturerUpdateModel);
            return manufacturerRepository.Update(manufacturerEntityUpdated);
        }

        private void UpdateManufacturer(ManufacturerUpdateModel manufacturerUpdateModel, ManufacturerEntity manufacturerEntity)
        {
            var goodsToRemove = manufacturerEntity.Goods.Where(goods =>
                !manufacturerUpdateModel.Goods.Any(goodss => goodss.Id == goods.Id));

            foreach (var goods in goodsToRemove)
            {
                goods.ManufacturerId = Guid.Empty;
                goods.Manufacturer = null;
                goodsRepository.Update(goods);
            }

            var goodsToAdd = manufacturerUpdateModel.Goods.Where(
                goods => !manufacturerEntity.Goods.Any(goodss => goodss.Id == goods.Id));


            foreach (var goods in goodsToAdd)
            {
                var goodEntity = goodsRepository.GetById(goods.Id);
                goodEntity.ManufacturerId = manufacturerUpdateModel.Id;
                goodsRepository.Update(goodEntity);
            }

        }

        public void Delete(Guid id)
        {
            var manufacturerEntity = manufacturerRepository.GetById(id);
            manufacturerEntity.Goods = goodsRepository.GetByManufacturerId(id);
            foreach (var goods in manufacturerEntity.Goods)
            {
                goods.ManufacturerId = Guid.Empty;
                goods.Manufacturer = null;
            }
            manufacturerRepository.Remove(id);
        }
    }
}