using System;
using System.Collections.Generic;
using AutoMapper;

namespace PcShop.DAL.Entities
{
    public class EvaluationEntity : IEntity
    {
        public Guid Id { get; set; }

        public string TextEvaluation { get; set; }
        public int PercentEvaluation { get; set; }

        public Guid GoodsId { get; set; }
        public virtual GoodsEntity Goods { get; set; }

 }

    public class EvaluationEntityMapperProfile : Profile
    {
        public EvaluationEntityMapperProfile()
        {
            CreateMap<EvaluationEntity, EvaluationEntity>();
        }
    }

}
