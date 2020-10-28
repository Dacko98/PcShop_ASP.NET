using System;
using System.Collections.Generic;

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
}
