using System;
using System.Collections.Generic;
using AutoMapper;

namespace PcShop.DAL.Entities
{
    public class ManufacturerEntity : IEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string Logo { get; set; }
        public string CountryOfOrigin { get; set; }

        public virtual ICollection<GoodsEntity> Goods { get; set; } = new List<GoodsEntity>();

    }

    public class ManufacturerEntityMapperProfile : Profile
    {
        public ManufacturerEntityMapperProfile()
        {
            CreateMap<ManufacturerEntity, ManufacturerEntity>();
        }
    }
}
