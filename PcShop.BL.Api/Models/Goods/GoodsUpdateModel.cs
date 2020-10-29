using System;
using AutoMapper;
using PcShop.Common.Extensions;
using PcShop.DAL.Entities;
using PcShop.DAL.Entities;

namespace PcShop.BL.Api.Models.Goods
{
    public class GoodsUpdateModel
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

    public class GoodsUpdateModelMapperProfile : Profile
    {
        public GoodsUpdateModelMapperProfile()
        {
            CreateMap<GoodsUpdateModel, GoodsEntity>();
        }
    }
}
