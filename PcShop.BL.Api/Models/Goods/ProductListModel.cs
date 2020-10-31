using AutoMapper;
using PcShop.DAL.Entities;
using System;
using PcShop.Common.Extensions;

namespace PcShop.BL.Api.Models.Product
{
    public class ProductListModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string Photo { get; set; }

        public string ManufacturerName { get; set; }
        public string CategoryName { get; set; }
    }

    public class ProductListModelMapperProfile : Profile
    {
        public ProductListModelMapperProfile()
        {
            CreateMap<ProductEntity, ProductListModel>()
                .MapMember(dst => dst.ManufacturerName, src => src.Manufacturer.Name)
                .MapMember(dst => dst.CategoryName, src => src.Category.Name);
        }
    }
}
