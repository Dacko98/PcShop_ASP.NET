using AutoMapper;
using System;
using System.Collections.Generic;
using PcShop.DAL.Entities;

namespace PcShop.BL.Api.Models.Category
{
    public class CategoryDetailModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<GoodsEntity> Goods { get; set; } = new List<GoodsEntity>();
    }

    public class CategoryDetailModelMapperProfile : Profile
    {
        public CategoryDetailModelMapperProfile()
        {
            CreateMap<CategoryEntity, CategoryDetailModel>();
        }
    }
}