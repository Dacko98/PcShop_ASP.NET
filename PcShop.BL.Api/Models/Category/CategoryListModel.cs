using AutoMapper;
using PcShop.DAL.Entities;
using System;
using PcShop.DAL.Entities;

namespace PcShop.BL.Api.Models.Category
{
    public class CategoryListModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }

    public class CategoryListModelMapperProfile : Profile
    {
        public CategoryListModelMapperProfile()
        {
            CreateMap<CategoryEntity, CategoryListModel>();
        }
    }
}
