using AutoMapper;
using PcShop.DAL.Entities;
using System;
using PcShop.BL.Api.Models.Interfaces;

namespace PcShop.BL.Api.Models.Category
{
    public class CategoryListModel : IListModel
    {
        public Guid Id { get; set; }
        public EntityTypeEnum EntityType { get; set; }

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
