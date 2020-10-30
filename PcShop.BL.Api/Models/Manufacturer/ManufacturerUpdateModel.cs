using AutoMapper;
using System;
using System.Collections.Generic;
using PcShop.BL.Api.Models.Goods;
using PcShop.Common.Extensions;
using PcShop.DAL.Entities;

namespace PcShop.BL.Api.Models.Manufacturer
{
    public class ManufacturerUpdateModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string Logo { get; set; }
        public string CountryOfOrigin { get; set; }

        public IList<GoodsUpdateModel> Goods { get; set; }
    }

    public class ManufacturerUpdateModelMapperProfile : Profile
    {
        public ManufacturerUpdateModelMapperProfile()
        {
            CreateMap<ManufacturerUpdateModel, ManufacturerEntity>();
        }
    }
}