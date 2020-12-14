using System;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using PcShop.BL.Api.Models.Interfaces;
using PcShop.Common.Extensions;
using PcShop.DAL.Entities;

namespace PcShop.BL.Api.Models.Product
{
    public class ProductNewModel : IModel
    {

        [StringLength(50, MinimumLength = 4, ErrorMessage = "Name is too short or too long, insert name of length between 4 and 50")]
        public string Name { get; set; }
        public string Photo { get; set; }
        public string Description { get; set; }
        [Range(0,  int.MaxValue, ErrorMessage = "Price cant be a negative value")]
        public int Price { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Weight cant be a negative value")]
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
                .Ignore(dst => dst.Category)
                .Ignore(dst => dst.AverageScore);
        }
    }
}
