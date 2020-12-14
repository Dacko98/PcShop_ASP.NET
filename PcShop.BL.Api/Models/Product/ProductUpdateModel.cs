using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using PcShop.BL.Api.Models.Evaluation;
using PcShop.BL.Api.Models.Interfaces;
using PcShop.Common.Extensions;
using PcShop.DAL.Entities;

namespace PcShop.BL.Api.Models.Product
{
    public class ProductUpdateModel : IModel
    {

        public Guid Id { get; set; }

        [StringLength(50, MinimumLength = 4, ErrorMessage = "Name is too short or too long, insert name of length between 4 and 50")]
        public string Name { get; set; }
        public string Photo { get; set; }
        public string Description { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Price cant be a negative value")]
        public int Price { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Weight cant be a negative value")]
        public int Weight { get; set; }
        public int CountInStock { get; set; }

        public string RAM { get; set; }

        public string CPU { get; set; }

        public string GPU { get; set; }

        public string HDD { get; set; }
        public Guid ManufacturerId { get; set; }

        public Guid CategoryId { get; set; }

        public virtual IList<EvaluationUpdateModel> Evaluations { get; set; }

    }

    public class ProductUpdateModelMapperProfile : Profile
    {
        public ProductUpdateModelMapperProfile()
        {
            CreateMap<ProductUpdateModel, ProductEntity>()
                .Ignore(dst => dst.Manufacturer)
                .Ignore(dst => dst.Category)
                .Ignore(dst => dst.AverageScore);
        }
    }
}
