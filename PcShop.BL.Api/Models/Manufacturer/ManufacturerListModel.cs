using AutoMapper;
using PcShop.DAL.Entities;
using System;
using PcShop.BL.Api.Models.Interfaces;

namespace PcShop.BL.Api.Models.Manufacturer
{
    public class ManufacturerListModel : IListModel
    {
        public Guid Id { get; set; }
        public EntityTypeEnum EntityType { get; set; }

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