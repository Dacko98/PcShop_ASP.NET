using AutoMapper;
using System;
using System.Collections.Generic;
using PcShop.BL.Api.Models.Evaluation;
using PcShop.Common.Extensions;
using PcShop.DAL.Entities;

namespace PcShop.BL.Api.Models.Goods
{
    public class GoodsDetailModel
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

        public virtual IList<EvaluationListModel> Evaluations { get; set; }
       
    }

    public class GoodsDetailModelMapperProfile : Profile
    {
        public GoodsDetailModelMapperProfile()
        {
            CreateMap<GoodsEntity, GoodsDetailModel>()
                .MapMember(dst => dst.ManufacturerName, src => src.Manufacturer.Name)
                .MapMember(dst => dst.CategoryName, src => src.Category.Name); ;
        }
    }
}