using AutoMapper;
using System;
using System.ComponentModel.DataAnnotations;
using PcShop.BL.Api.Models.Interfaces;
using PcShop.Common.Extensions;
using PcShop.DAL.Entities;

namespace PcShop.BL.Api.Models.Evaluation
{
    public class EvaluationUpdateModel : IModel
    {
        public Guid Id { get; set; }
        public string TextEvaluation { get; set; }
        [Range(0, 100, ErrorMessage = "Evaluation out of range, insert number between 0 and 100")]
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