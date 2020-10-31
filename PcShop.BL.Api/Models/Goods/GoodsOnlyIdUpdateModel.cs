using System;
using System.Collections.Generic;
using AutoMapper;
using PcShop.BL.Api.Models.Evaluation;
using PcShop.Common.Extensions;
using PcShop.DAL.Entities;
using PcShop.DAL.Entities;

namespace PcShop.BL.Api.Models.Goods
{
    public class GoodsOnlyIdUpdateModel
    {

        public Guid Id { get; set; }

    }

    public class GoodsCategoryUpdateModelMapperProfile : Profile
    {
        public GoodsCategoryUpdateModelMapperProfile()
        {
            CreateMap<GoodsOnlyIdUpdateModel, GoodsEntity>()
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
                .Ignore(dst => dst.Weight);
        }
    }
}
