using AutoMapper;
using PcShop.DAL.Entities;
using System;
using PcShop.BL.Api.Models.Interfaces;
using PcShop.Common.Extensions;

namespace PcShop.BL.Api.Models.Evaluation
{
    public class EvaluationListModel : IListModel
    {
        public Guid Id { get; set; }
        public EntityTypeEnum EntityType { get; set; }

        public string TextEvaluation { get; set; }
        public int PercentEvaluation { get; set; }
        public string ProductName { get; set; }
    }

    public class EvaluationListModelMapperProfile : Profile
    {
        public EvaluationListModelMapperProfile()
        {
            CreateMap<EvaluationEntity, EvaluationListModel>()
                .MapMember(dst => dst.ProductName, src => src.Product.Name);
        }
    }
}