using AutoMapper;
using System;
using System.Collections.Generic;
using PcShop.BL.Api.Models.Evaluation;
using PcShop.BL.Api.Models.Product;
using PcShop.DAL.Entities;

namespace PcShop.BL.Api.Models.Category
{
    public class CategoryDetailModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public virtual IList<ProductListModel> Product { get; set; }
    }

    public class CategoryDetailModelMapperProfile : Profile
    {
        public CategoryDetailModelMapperProfile()
        {
            CreateMap<CategoryEntity, CategoryDetailModel>();
        }
    }
}