using AutoMapper;
using PcShop.DAL.Entities;
using System;
using PcShop.DAL.Entities;

namespace PcShop.BL.Api.Models.Category
{
    public class CategoryUpdateModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }


    public class CategoryUpdateModelMapperProfile : Profile
    {
        public CategoryUpdateModelMapperProfile()
        {
            CreateMap<CategoryUpdateModel, CategoryEntity>();
        }
    }
}
