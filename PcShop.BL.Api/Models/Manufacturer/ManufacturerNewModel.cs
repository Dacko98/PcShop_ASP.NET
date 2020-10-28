using AutoMapper;
using PcShop.Common.Extensions;
using PcShop.DAL.Entities;
using PcShop.DAL.Entities;

namespace PcShop.BL.Api.Models.Manufacturer
{
    public class ManufacturerNewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Logo { get; set; }
        public string CountryOfOrigin { get; set; }
    }

    public class ManufacturerNewModelMapperProfile : Profile
    {
        public ManufacturerNewModelMapperProfile()
        {
            CreateMap<ManufacturerNewModel, ManufacturerEntity>()
                .Ignore(dst => dst.Id);
        }
    }
}