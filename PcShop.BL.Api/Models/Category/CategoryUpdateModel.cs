using System;
using System.Collections.Generic;
using AutoMapper;
using PcShop.BL.Api.Models.Goods;
using PcShop.Common.Extensions;
using PcShop.DAL.Entities;
using PcShop.DAL.Entities;

namespace PcShop.BL.Api.Models.Category
{
    public class CategoryUpdateModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public virtual IList<GoodsOnlyIdUpdateModel> Goods { get; set; }

    }

    public class CategoryUpdateModelModelMapperProfile : Profile
    {
        public CategoryUpdateModelModelMapperProfile()
        {
            CreateMap<CategoryUpdateModel, CategoryEntity>();
        }
    }
}
