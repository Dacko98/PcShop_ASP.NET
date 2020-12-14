using System;
using System.Collections.Generic;
using AutoMapper;

namespace PcShop.DAL.Entities
{
    public class CategoryEntity : IEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<ProductEntity> Product { get; set; } = new List<ProductEntity>();

    }
    public class CategoryEntityMapperProfile : Profile
    {
        public CategoryEntityMapperProfile()
        {
            CreateMap<CategoryEntity, CategoryEntity>();
        }
    }
}
