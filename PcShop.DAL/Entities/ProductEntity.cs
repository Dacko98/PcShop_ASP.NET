using System;
using System.Collections.Generic;
using AutoMapper;

namespace PcShop.DAL.Entities
{
    public class ProductEntity : IEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string Photo { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int Weight { get; set; }
        public int CountInStock { get; set; }

        public string Ram { get; set; }

        public string Cpu  { get; set; }

        public string Gpu { get; set; }

        public string Hdd { get; set; }

        public int AverageScore { get; set; }

        public Guid ManufacturerId { get; set; }
        public virtual ManufacturerEntity Manufacturer { get; set; }

        public Guid CategoryId { get; set; }
        public virtual CategoryEntity Category { get; set; }

        public virtual ICollection<EvaluationEntity> Evaluations { get; set; } = new List<EvaluationEntity>();

    }
    public class ProductEntityMapperProfile : Profile
    {
        public ProductEntityMapperProfile()
        {
            CreateMap<ProductEntity, ProductEntity>();
        }
    }

}
