using AutoMapper;
using PcShop.Common.Extensions;
using PcShop.DAL.Entities;

namespace PcShop.BL.Api.Models.Category
{
    public class CategoryNewModel
    {

        public string Name { get; set; }
    }

    public class CategoryNewModelMapperProfile : Profile
    {
        public CategoryNewModelMapperProfile()
        {
            CreateMap<CategoryNewModel, CategoryEntity>()
                .Ignore(dst => dst.Id)
                .Ignore(dst => dst.Goods);
        }
    }
}
