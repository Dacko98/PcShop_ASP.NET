using AutoMapper;
using System;
using System.Collections.Generic;
using PcShop.BL.Api.Models.Evaluation;
using PcShop.BL.Api.Models.Interfaces;
using PcShop.Common.Extensions;
using PcShop.DAL.Entities;
using System.ComponentModel.DataAnnotations;

namespace PcShop.BL.Api.Models.Product
{
    public class ProductDetailModel : IModel
    {
        public Guid Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "The name is required.")]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "Name is too short or too long, insert name of length between 4 and 50")]
        public string Name { get; set; }
        public string Photo { get; set; }
        public string Description { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Price cant be a negative value")]
        public int Price { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Weight cant be a negative value")]
        public int Weight { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "There cannot be a negative product count in stock")]
        public int CountInStock { get; set; }

        [StringLength(50, MinimumLength = 4, ErrorMessage = "Name is too short or too long, insert name of length between 4 and 50")]
        public string ManufacturerName { get; set; }
        [StringLength(50, MinimumLength = 4, ErrorMessage = "Name is too short or too long, insert name of length between 4 and 50")]
        public string CategoryName { get; set; }

        public string Ram { get; set; }

        public string Cpu { get; set; }

        public string Gpu { get; set; }

        public string Hdd { get; set; }
        public virtual IList<EvaluationListModel> Evaluations { get; set; }

        public int AverageScore { get; set; }

    }

    public class ProductDetailModelMapperProfile : Profile
    {
        public ProductDetailModelMapperProfile()
        {
            CreateMap<ProductEntity, ProductDetailModel>()
                .MapMember(dst => dst.ManufacturerName, src => src.Manufacturer.Name)
                .MapMember(dst => dst.CategoryName, src => src.Category.Name);
        }
    }
}