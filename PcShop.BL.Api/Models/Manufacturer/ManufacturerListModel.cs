using AutoMapper;
using PcShop.DAL.Entities;
using System;
using PcShop.DAL.Entities;

namespace PcShop.BL.Api.Models.Manufacturer
{
    public class ManufacturerListModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string Logo { get; set; }
        public string CountryOfOrigin { get; set; }
    }

    public class ManufacturerListModelMapperProfile : Profile
    {
        public ManufacturerListModelMapperProfile()
        {
            CreateMap<ManufacturerEntity, ManufacturerListModel>();
        }
    }
}