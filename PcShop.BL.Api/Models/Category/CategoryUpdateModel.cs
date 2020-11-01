using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using PcShop.BL.Api.Models.Interfaces;
using PcShop.BL.Api.Models.Product;
using PcShop.DAL.Entities;

namespace PcShop.BL.Api.Models.Category
{
    public class CategoryUpdateModel : IModel
    {
        public Guid Id { get; set; }
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name is too short or too long, insert name of length between 4 and 50")]
        public string Name { get; set; }

        public virtual IList<ProductOnlyIdUpdateModel> Product { get; set; }

    }

    public class CategoryUpdateModelModelMapperProfile : Profile
    {
        public CategoryUpdateModelModelMapperProfile()
        {
            CreateMap<CategoryUpdateModel, CategoryEntity>();
        }
    }
}
