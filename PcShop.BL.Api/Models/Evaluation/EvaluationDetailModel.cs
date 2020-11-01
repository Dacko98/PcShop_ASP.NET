using AutoMapper;
using System;
using PcShop.BL.Api.Models.Interfaces;
using PcShop.DAL.Entities;
using PcShop.Common.Extensions;

namespace PcShop.BL.Api.Models.Evaluation
{
    public class EvaluationDetailModel : IModel
    {
        public Guid Id { get; set; }
        public string TextEvaluation { get; set; }
        public int PercentEvaluation { get; set; }
        public string ProductName { get; set; }

        public Guid ProductId { get; set; }
    }

    public class EvaluationDetailModelMapperProfile : Profile
    {
        public EvaluationDetailModelMapperProfile()
        {
            CreateMap<EvaluationEntity, EvaluationDetailModel>()
                .MapMember(dst => dst.ProductName, src => src.Product.Name);
        }
    }
}