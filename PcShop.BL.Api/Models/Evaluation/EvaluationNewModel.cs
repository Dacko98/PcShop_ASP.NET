using System;
using AutoMapper;
using PcShop.Common.Extensions;
using PcShop.DAL.Entities;
using PcShop.DAL.Entities;

namespace PcShop.BL.Api.Models.Evaluation
{
    public class EvaluationNewModel
    {
        public string TextEvaluation { get; set; }
        public int PercentEvaluation { get; set; }

        public Guid ProductId { get; set; }
    }

    public class EvaluationNewModelMapperProfile : Profile
    {
        public EvaluationNewModelMapperProfile()
        {
            CreateMap<EvaluationNewModel, EvaluationEntity>()
                .Ignore(dst => dst.Id)
                .Ignore(dst => dst.Product);
        }
    }
}