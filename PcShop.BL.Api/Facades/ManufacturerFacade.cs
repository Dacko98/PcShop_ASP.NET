using AutoMapper;
using PcShop.DAL.Entities;
using PcShop.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using PcShop.BL.Api.Facades.Interfaces;
using PcShop.BL.Api.Models.Manufacturer;

namespace PcShop.BL.Api.Facades
{
    public class ManufacturerFacade : IAppFacade
    {
        private readonly ManufacturerRepository manufacturerRepository;
        private readonly IMapper mapper;
        private readonly ProductRepository productRepository;
        private readonly ProductFacade productFacade;

        public ManufacturerFacade(
            ProductRepository productRepository,
            ManufacturerRepository manufacturerRepository,
            ProductFacade productFacade,
            IMapper mapper)
        {
            this.manufacturerRepository = manufacturerRepository;
            this.mapper = mapper;
            this.productFacade = productFacade;
            this.productRepository = productRepository;
        }

        public List<ManufacturerListModel> GetAll()
        {
            return mapper.Map<List<ManufacturerListModel>>(manufacturerRepository.GetAll());
        }

        public ManufacturerDetailModel GetById(Guid id)
        {
            var manufacturerEntity = manufacturerRepository.GetById(id);
            if (manufacturerEntity == null)
            {
                return null;
            }
            manufacturerEntity.Product = productRepository.GetByManufacturerId(id);
            return mapper.Map<ManufacturerDetailModel>(manufacturerEntity);
        }

        public Guid Create(ManufacturerNewModel manufacturer)
        {
            var manufacturerEntity = mapper.Map<ManufacturerEntity>(manufacturer);
            return manufacturerRepository.Insert(manufacturerEntity);
        }

        public Guid? Update(ManufacturerUpdateModel manufacturerUpdateModel)
        {
            var manufacturerEntityExisting = manufacturerRepository.GetById(manufacturerUpdateModel.Id);
            if (manufacturerEntityExisting == null)
            {
                return null;
            }
            manufacturerEntityExisting.Product = productRepository.GetByManufacturerId(manufacturerUpdateModel.Id);
            UpdateManufacturer(manufacturerUpdateModel, manufacturerEntityExisting);

            var manufacturerEntityUpdated = mapper.Map<ManufacturerEntity>(manufacturerUpdateModel);
            return manufacturerRepository.Update(manufacturerEntityUpdated);
        }

        private void UpdateManufacturer(ManufacturerUpdateModel manufacturerUpdateModel, ManufacturerEntity manufacturerEntity)
        {
            var productToRemove = manufacturerEntity.Product.Where(product =>
                !manufacturerUpdateModel.Product.Any(products => products.Id == product.Id));

            foreach (var product in productToRemove)
            {
                product.ManufacturerId = Guid.Empty;
                product.Manufacturer = null;
                productRepository.Update(product);
            }

            var productToAdd = manufacturerUpdateModel.Product.Where(
                product => !manufacturerEntity.Product.Any(products => products.Id == product.Id));


            foreach (var product in productToAdd)
            {
                var goodEntity = productRepository.GetById(product.Id);
                goodEntity.ManufacturerId = manufacturerUpdateModel.Id;
                productRepository.Update(goodEntity);
            }

        }

        public void Delete(Guid id)
        {
            var manufacturerEntity = manufacturerRepository.GetById(id);
            manufacturerEntity.Product = productRepository.GetByManufacturerId(id);
            foreach (var product in manufacturerEntity.Product)
            {
                product.ManufacturerId = Guid.Empty;
                product.Manufacturer = null;
            }
            manufacturerRepository.Remove(id);
        }
    }
}