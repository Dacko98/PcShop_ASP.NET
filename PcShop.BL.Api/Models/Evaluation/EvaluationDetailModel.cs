using AutoMapper;
using System;
using PcShop.DAL.Entities;
using PcShop.Common.Extensions;

namespace PcShop.BL.Api.Models.Evaluation
{
    public class EvaluationDetailModel
    {
        public Guid Id { get; set; }
        public string TextEvaluation { get; set; }
        public int PercentEvaluation { get; set; }
        public string ProductName { get; set; }

        public Guid GoodsId { get; set; }
    }

    public class EvaluationDetailModelMapperProfile : Profile
    {
        public EvaluationDetailModelMapperProfile()
        {
            CreateMap<EvaluationEntity, EvaluationDetailModel>()
                .MapMember(dst => dst.ProductName, src => src.Goods.Name);
        }
    }
}