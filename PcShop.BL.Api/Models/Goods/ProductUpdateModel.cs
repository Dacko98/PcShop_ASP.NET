﻿using System;
using System.Collections.Generic;
using AutoMapper;
using PcShop.BL.Api.Models.Evaluation;
using PcShop.Common.Extensions;
using PcShop.DAL.Entities;
using PcShop.DAL.Entities;

namespace PcShop.BL.Api.Models.Product
{
    public class ProductUpdateModel
    {

        public Guid Id { get; set; }

        public string Name { get; set; }
        public string Photo { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
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
                .Ignore(dst => dst.Category);
        }
    }
}