using AutoMapper;
using System;
using System.Collections.Generic;
using PcShop.BL.Api.Models.Evaluation;
using PcShop.BL.Api.Models.Interfaces;
using PcShop.Common.Extensions;
using PcShop.DAL.Entities;

namespace PcShop.BL.Api.Models.Product
{
    public class ProductDetailModel : IModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string Photo { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int Weight { get; set; }
        public int CountInStock { get; set; }

        public string ManufacturerName { get; set; }
        public string CategoryName { get; set; }

        public string RAM { get; set; }

        public string CPU { get; set; }

        public string GPU { get; set; }

        public string HDD { get; set; }
        public virtual IList<EvaluationListModel> Evaluations { get; set; }
       
    }

    public class ProductDetailModelMapperProfile : Profile
    {
        public ProductDetailModelMapperProfile()
        {
            CreateMap<ProductEntity, ProductDetailModel>()
                .MapMember(dst => dst.ManufacturerName, src => src.Manufacturer.Name)
                .MapMember(dst => dst.CategoryName, src => src.Category.Name); ;
        }
    }
}