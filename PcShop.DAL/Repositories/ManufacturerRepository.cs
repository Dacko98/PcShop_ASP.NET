using AutoMapper;
using PcShop.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PcShop.DAL.Repositories
{
    public class ManufacturerRepository : IAppRepository<ManufacturerEntity>
    {
        private readonly IList<ManufacturerEntity> manufacturers;
        private readonly IMapper mapper;

        public ManufacturerRepository(
            Storage storage,
            IMapper mapper)
        {
            manufacturers = storage.Manufacturers;
            this.mapper = mapper;
        }

        public IList<ManufacturerEntity> GetAll()
        {
            return manufacturers;
        }

        public ManufacturerEntity GetById(Guid id)
        {
            return manufacturers.SingleOrDefault(entity => entity.Id == id);
        }

        public Guid Insert(ManufacturerEntity manufacturer)
        {
            manufacturer.Id = Guid.NewGuid();
            manufacturers.Add(manufacturer);
            return manufacturer.Id;
        }

        public Guid? Update(ManufacturerEntity manufacturerUpdated)
        {
            var manufacturerExisting = manufacturers.SingleOrDefault(manufacturerInStorage => manufacturerInStorage.Id == manufacturerUpdated.Id);
            if (manufacturerExisting != null)
            {
                mapper.Map(manufacturerUpdated, manufacturerExisting);
            }

            return manufacturerExisting?.Id;
        }

        public void Remove(Guid id)
        {
            var manufacturerToRemove = manufacturers.Single(manufacturer => manufacturer.Id.Equals(id));
            manufacturers.Remove(manufacturerToRemove);
        }
    }
}