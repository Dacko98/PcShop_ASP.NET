using AutoMapper;
using PcShop.DAL.Entities;
using System;

namespace PcShop.BL.Api.Models.Evaluation
{
    public class EvaluationUpdateModel
    {
        public Guid Id { get; set; }
        public string TextEvaluation { get; set; }
        public int PercentEvaluation { get; set; }

        public Guid GoodsId { get; set; }
        public virtual GoodsEntity Goods { get; set; }
    }


    public class EvaluationUpdateModelMapperProfile : Profile
    {
        public EvaluationUpdateModelMapperProfile()
        {
            CreateMap<EvaluationUpdateModel, EvaluationEntity>();
        }
    }
}