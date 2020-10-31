using AutoMapper;
using System;
using PcShop.Common.Extensions;
using PcShop.DAL.Entities;

namespace PcShop.BL.Api.Models.Evaluation
{
    public class EvaluationUpdateModel
    {
        public Guid Id { get; set; }
        public string TextEvaluation { get; set; }
        public int PercentEvaluation { get; set; }

        public Guid ProductId { get; set; }
  
    }

    public class EvaluationUpdateModelMapperProfile : Profile
    {
        public EvaluationUpdateModelMapperProfile()
        {
            CreateMap<EvaluationUpdateModel, EvaluationEntity>()
                .Ignore(dst => dst.Product);
            CreateMap<EvaluationUpdateModel, EvaluationNewModel>();
        }
    }
}