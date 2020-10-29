using AutoMapper;
using System;
using PcShop.DAL.Entities;

namespace PcShop.BL.Api.Models.Category
{
    public class CategoryDetailModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }

    public class CategoryDetailModelMapperProfile : Profile
    {
        public CategoryDetailModelMapperProfile()
        {
            CreateMap<CategoryEntity, CategoryDetailModel>();
        }
    }
}