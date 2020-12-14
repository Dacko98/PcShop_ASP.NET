using AutoMapper;
using PcShop.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PcShop.DAL.Repositories
{
    public class ManufacturerRepository : IAppRepository<ManufacturerEntity>
    {
        private readonly IList<ManufacturerEntity> _manufacturers;
        private readonly IMapper _mapper;

        public ManufacturerRepository(
            Storage storage,
            IMapper mapper)
        {
            _manufacturers = storage.Manufacturers;
            this._mapper = mapper;
        }

        public IList<ManufacturerEntity> GetAll()
        {
            return _manufacturers;
        }

        public ManufacturerEntity GetById(Guid id)
        {
            return _manufacturers.SingleOrDefault(entity => entity.Id == id);
        }

        public Guid Insert(ManufacturerEntity manufacturer)
        {
            manufacturer.Id = Guid.NewGuid();
            _manufacturers.Add(manufacturer);
            return manufacturer.Id;
        }

        public Guid? Update(ManufacturerEntity manufacturerUpdated)
        {
            var manufacturerExisting = _manufacturers.SingleOrDefault(manufacturerInStorage => manufacturerInStorage.Id == manufacturerUpdated.Id);
            if (manufacturerExisting != null)
            {
                _mapper.Map(manufacturerUpdated, manufacturerExisting);
            }

            return manufacturerExisting?.Id;
        }

        public void Remove(Guid id)
        {
            var manufacturerToRemove = _manufacturers.Single(manufacturer => manufacturer.Id.Equals(id));
            _manufacturers.Remove(manufacturerToRemove);
        }
    }
}