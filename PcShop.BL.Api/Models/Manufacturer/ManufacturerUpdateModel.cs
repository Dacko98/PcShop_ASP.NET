using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PcShop.BL.Api.Models.Interfaces;
using PcShop.BL.Api.Models.Product;
using PcShop.DAL.Entities;

namespace PcShop.BL.Api.Models.Manufacturer
{
    public class ManufacturerUpdateModel : IModel
    {
        public Guid Id { get; set; }

        [StringLength(50, MinimumLength = 4, ErrorMessage = "Name is too short or too long, insert name of length between 4 and 50")]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Logo { get; set; }
        public string CountryOfOrigin { get; set; }

        public IList<ProductOnlyIdUpdateModel> Product { get; set; }
    }

    public class ManufacturerUpdateModelMapperProfile : Profile
    {
        public ManufacturerUpdateModelMapperProfile()
        {
            CreateMap<ManufacturerUpdateModel, ManufacturerEntity>();
        }
    }
}