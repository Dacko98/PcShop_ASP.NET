using AutoMapper;
using PcShop.DAL.Entities;
using PcShop.DAL.Repositories;
using System;
using System.Collections.Generic;
using PcShop.BL.Api.Facades.Interfaces;
using PcShop.BL.Api.Models.Manufacturer;
using PcShop.DAL.Entities;

namespace PcShop.BL.Api.Facades
{
    public class ManufacturerFacade : IAppFacade
    {
        private readonly ManufacturerRepository manufacturerRepository;
        private readonly IMapper mapper;

        public ManufacturerFacade(
            ManufacturerRepository manufacturerRepository,
            IMapper mapper)
        {
            this.manufacturerRepository = manufacturerRepository;
            this.mapper = mapper;
        }

        public List<ManufacturerListModel> GetAll()
        {
            return mapper.Map<List<ManufacturerListModel>>(manufacturerRepository.GetAll());
        }

        public ManufacturerDetailModel GetById(Guid id)
        {
            return mapper.Map<ManufacturerDetailModel>(manufacturerRepository.GetById(id));
        }

        public Guid Create(ManufacturerNewModel manufacturer)
        {
            var manufacturerEntity = mapper.Map<ManufacturerEntity>(manufacturer);
            return manufacturerRepository.Insert(manufacturerEntity);
        }

        public Guid? Update(ManufacturerUpdateModel manufacturer)
        {
            var manufacturerEntity = mapper.Map<ManufacturerEntity>(manufacturer);
            return manufacturerRepository.Update(manufacturerEntity);
        }

        public void Delete(Guid id)
        {
            manufacturerRepository.Remove(id);
        }
    }
}