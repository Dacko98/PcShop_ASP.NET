using AutoMapper;
using PcShop.DAL.Entities;
using System;
using PcShop.Common.Extensions;

namespace PcShop.BL.Api.Models.Goods
{
    public class GoodsListModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string Photo { get; set; }

        public string ManufacturerName { get; set; }
        public string CategoryName { get; set; }
    }

    public class GoodsListModelMapperProfile : Profile
    {
        public GoodsListModelMapperProfile()
        {
            CreateMap<GoodsEntity, GoodsListModel>()
                .MapMember(dst => dst.ManufacturerName, src => src.Manufacturer.Name)
                .MapMember(dst => dst.CategoryName, src => src.Category.Name);
        }
    }
}
