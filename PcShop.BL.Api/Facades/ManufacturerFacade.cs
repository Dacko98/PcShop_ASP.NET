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
        private readonly ManufacturerRepository _manufacturerRepository;
        private readonly IMapper _mapper;
        private readonly ProductRepository _productRepository;
        private readonly ProductFacade _productFacade;

        public ManufacturerFacade(
            ProductRepository productRepository,
            ManufacturerRepository manufacturerRepository,
            ProductFacade productFacade,
            IMapper mapper)
        {
            this._manufacturerRepository = manufacturerRepository;
            this._mapper = mapper;
            this._productFacade = productFacade;
            this._productRepository = productRepository;
        }

        public List<ManufacturerListModel> GetAll()
        {
            return _mapper.Map<List<ManufacturerListModel>>(_manufacturerRepository.GetAll());
        }

        public ManufacturerDetailModel GetById(Guid id)
        {
            var manufacturerEntity = _manufacturerRepository.GetById(id);
            if (manufacturerEntity == null)
            {
                return null;
            }
            manufacturerEntity.Product = _productRepository.GetByManufacturerId(id);
            return _mapper.Map<ManufacturerDetailModel>(manufacturerEntity);
        }

        public Guid Create(ManufacturerNewModel manufacturer)
        {
            var manufacturerEntity = _mapper.Map<ManufacturerEntity>(manufacturer);
            return _manufacturerRepository.Insert(manufacturerEntity);
        }

        public Guid? Update(ManufacturerUpdateModel manufacturerUpdateModel)
        {
            var manufacturerEntityExisting = _manufacturerRepository.GetById(manufacturerUpdateModel.Id);
            if (manufacturerEntityExisting == null)
            {
                return null;
            }
            manufacturerEntityExisting.Product = _productRepository.GetByManufacturerId(manufacturerUpdateModel.Id);
            UpdateManufacturer(manufacturerUpdateModel, manufacturerEntityExisting);

            var manufacturerEntityUpdated = _mapper.Map<ManufacturerEntity>(manufacturerUpdateModel);
            return _manufacturerRepository.Update(manufacturerEntityUpdated);
        }

        private void UpdateManufacturer(ManufacturerUpdateModel manufacturerUpdateModel, ManufacturerEntity manufacturerEntity)
        {
            var productToRemove = manufacturerEntity.Product.Where(product =>
                !manufacturerUpdateModel.Product.Any(products => products.Id == product.Id));

            foreach (var product in productToRemove)
            {
                product.ManufacturerId = Guid.Empty;
                product.Manufacturer = null;
                _productRepository.Update(product);
            }

            var productToAdd = manufacturerUpdateModel.Product.Where(
                product => !manufacturerEntity.Product.Any(products => products.Id == product.Id));


            foreach (var product in productToAdd)
            {
                var goodEntity = _productRepository.GetById(product.Id);
                goodEntity.ManufacturerId = manufacturerUpdateModel.Id;
                _productRepository.Update(goodEntity);
            }

        }

        public void Delete(Guid id)
        {
            var manufacturerEntity = _manufacturerRepository.GetById(id);
            manufacturerEntity.Product = _productRepository.GetByManufacturerId(id);
            foreach (var product in manufacturerEntity.Product)
            {
                product.ManufacturerId = Guid.Empty;
                product.Manufacturer = null;
            }
            _manufacturerRepository.Remove(id);
        }
    }
}