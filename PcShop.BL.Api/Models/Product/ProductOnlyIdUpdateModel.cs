using System;
using AutoMapper;
using PcShop.BL.Api.Models.Interfaces;
using PcShop.Common.Extensions;
using PcShop.DAL.Entities;

namespace PcShop.BL.Api.Models.Product
{
    public class ProductOnlyIdUpdateModel : IModel
    {

        public Guid Id { get; set; }

    }

    public class ProductCategoryUpdateModelMapperProfile : Profile
    {
        public ProductCategoryUpdateModelMapperProfile()
        {
            CreateMap<ProductOnlyIdUpdateModel, ProductEntity>()
                .Ignore(dst => dst.Name)
                .Ignore(dst => dst.Category)
                .Ignore(dst => dst.Evaluations)
                .Ignore(dst => dst.Manufacturer)
                .Ignore(dst => dst.CategoryId)
                .Ignore(dst => dst.CountInStock)
                .Ignore(dst => dst.Manufacturer)
                .Ignore(dst => dst.Photo)
                .Ignore(dst => dst.Price)
                .Ignore(dst => dst.Description)
                .Ignore(dst => dst.ManufacturerId)
                .Ignore(dst => dst.Weight)
                .Ignore(dst => dst.CPU)
                .Ignore(dst => dst.GPU)
                .Ignore(dst => dst.RAM)
                .Ignore(dst => dst.HDD)
                .Ignore(dst => dst.AverageScore);
        }
    }
}
