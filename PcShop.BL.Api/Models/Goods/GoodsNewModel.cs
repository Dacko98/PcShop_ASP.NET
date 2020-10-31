using System;
using AutoMapper;
using PcShop.Common.Extensions;
using PcShop.DAL.Entities;

namespace PcShop.BL.Api.Models.Goods
{
    public class GoodsNewModel
    {


        public string Name { get; set; }
        public string Photo { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int Weight { get; set; }
        public int CountInStock { get; set; }

        public Guid ManufacturerId { get; set; }

        public Guid CategoryId { get; set; }
  
    }

    public class GoodsNewModelMapperProfile : Profile
    {
        public GoodsNewModelMapperProfile()
        {
            CreateMap<GoodsNewModel, GoodsEntity>()
                .Ignore(dst => dst.Id)
                .Ignore(dst => dst.Evaluations)
                .Ignore(dst => dst.Manufacturer)
                .Ignore(dst => dst.Category);
        }
    }
}
