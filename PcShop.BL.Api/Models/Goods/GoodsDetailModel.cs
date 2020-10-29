using AutoMapper;
using System;
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

        public Guid ManufacturerId { get; set; }
        public virtual ManufacturerEntity Manufacturer { get; set; }
    }

    public class GoodsDetailModelMapperProfile : Profile
    {
        public GoodsDetailModelMapperProfile()
        {
            CreateMap<GoodsEntity, GoodsDetailModel>();
        }
    }
}