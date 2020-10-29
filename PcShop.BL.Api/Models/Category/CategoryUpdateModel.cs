﻿using System;
using AutoMapper;
using PcShop.Common.Extensions;
using PcShop.DAL.Entities;
using PcShop.DAL.Entities;

namespace PcShop.BL.Api.Models.Category
{
    public class CategoryUpdateModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }

    public class CategoryUpdateModelModelMapperProfile : Profile
    {
        public CategoryUpdateModelModelMapperProfile()
        {
            CreateMap<CategoryUpdateModel, CategoryEntity>();
        }
    }
}
