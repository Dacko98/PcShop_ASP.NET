using AutoMapper;
using PcShop.DAL.Entities;
using System;
using PcShop.BL.Api.Models.Manufacturer;
using PcShop.DAL.Entities;
using PcShop.Common.Extensions;

namespace PcShop.BL.Api.Models.Goods
{
    public class GoodsListModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string Photo { get; set; }

        public Guid ManufacturerId { get; set; }
        public virtual ManufacturerEntity Manufacturer { get; set; }
    }

    public class GoodsListModelMapperProfile : Profile
    {
        public GoodsListModelMapperProfile()
        {
            CreateMap<GoodsEntity, GoodsListModel>();
        }
    }
}
