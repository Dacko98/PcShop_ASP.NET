using System.ComponentModel.DataAnnotations;
using AutoMapper;
using PcShop.BL.Api.Models.Interfaces;
using PcShop.Common.Extensions;
using PcShop.DAL.Entities;

namespace PcShop.BL.Api.Models.Manufacturer
{
    public class ManufacturerNewModel : IModel
    {
        [StringLength(50, MinimumLength = 4, ErrorMessage = "Name is too short or too long, insert name of length between 4 and 50")]
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
                .Ignore(dst => dst.Id)
                .Ignore(dst => dst.Product);
        }
    }
}