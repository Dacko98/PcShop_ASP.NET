using System;
using AutoMapper;
using PcShop.Common.Extensions;
using PcShop.DAL.Entities;

namespace PcShop.BL.Api.Models.Product
{
    public class ProductNewModel
    {


        public string Name { get; set; }
        public string Photo { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int Weight { get; set; }
        public int CountInStock { get; set; }

        public string RAM { get; set; }

        public string CPU { get; set; }

        public string GPU { get; set; }

        public string HDD { get; set; }

        public Guid ManufacturerId { get; set; }

        public Guid CategoryId { get; set; }
  
    }

    public class ProductNewModelMapperProfile : Profile
    {
        public ProductNewModelMapperProfile()
        {
            CreateMap<ProductNewModel, ProductEntity>()
                .Ignore(dst => dst.Id)
                .Ignore(dst => dst.Evaluations)
                .Ignore(dst => dst.Manufacturer)
                .Ignore(dst => dst.Category);
        }
    }
}
