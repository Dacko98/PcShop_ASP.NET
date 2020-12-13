using System;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using PcShop.BL.Api.Models.Interfaces;
using PcShop.Common.Extensions;
using PcShop.DAL.Entities;

namespace PcShop.BL.Api.Models.Evaluation
{
    public class EvaluationNewModel : IModel
    {
        public string TextEvaluation { get; set; }

        [Required]
        [Range(0, 100, ErrorMessage = "Evaluation out of range, insert number between 0 and 100")]
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