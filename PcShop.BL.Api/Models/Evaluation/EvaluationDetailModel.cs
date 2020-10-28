using AutoMapper;
using System;
using PcShop.DAL.Entities;

namespace PcShop.BL.Api.Models.Evaluation
{
    public class EvaluationDetailModel
    {
        public Guid Id { get; set; }
        public string TextEvaluation { get; set; }
        public int PercentEvaluation { get; set; }

        public Guid GoodsId { get; set; }
        public virtual GoodsEntity Goods { get; set; }
    }

    public class EvaluationDetailModelMapperProfile : Profile
    {
        public EvaluationDetailModelMapperProfile()
        {
            CreateMap<EvaluationEntity, EvaluationDetailModel>();
        }
    }
}