using System;
using AutoMapper;

namespace PcShop.DAL.Entities
{
    public class EvaluationEntity : IEntity
    {
        public Guid Id { get; set; }

        public string TextEvaluation { get; set; }
        public int PercentEvaluation { get; set; }

        public Guid ProductId { get; set; }
        public virtual ProductEntity Product { get; set; }

 }

    public class EvaluationEntityMapperProfile : Profile
    {
        public EvaluationEntityMapperProfile()
        {
            CreateMap<EvaluationEntity, EvaluationEntity>();
        }
    }

}
