﻿using AutoMapper;
using PcShop.DAL.Entities;
using System;
using PcShop.DAL.Entities;

namespace PcShop.BL.Api.Models.Evaluation
{
    public class EvaluationListModel
    {
        public Guid Id { get; set; }
        public string TextEvaluation { get; set; }
        public int PercentEvaluation { get; set; }

        public Guid GoodsId { get; set; }
        public virtual GoodsEntity Goods { get; set; }
    }

    public class EvaluationListModelMapperProfile : Profile
    {
        public EvaluationListModelMapperProfile()
        {
            CreateMap<EvaluationEntity, EvaluationListModel>();
        }
    }
}