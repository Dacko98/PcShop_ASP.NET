using System;
using System.Collections.Generic;

namespace PcShopIW5_DAL.Entities
{
    public class EvaluationEntity : IEntity
    {
        public Guid Id { get; set; }

        public string TextEvaluation { get; set; }
        public string PercentEvaluation { get; set; }

        public Guid GoodsId { get; set; }
        public virtual GoodsEntity Goods { get; set; }

        private sealed class EvaluationEntityEqualityComparer : IEqualityComparer<EvaluationEntity>
        {
            public bool Equals(EvaluationEntity x, EvaluationEntity y)
            {
                if (x is null && y is null) return true;
                if (x is null || y is null) return false;
                if (x.GetType() != y.GetType()) return false;
                return x.Id.Equals(y.Id) && x.TextEvaluation == y.TextEvaluation 
                                         && x.PercentEvaluation == y.PercentEvaluation 
                                         && x.GoodsId.Equals(y.GoodsId) 
                                         && Equals(x.Goods, y.Goods);
            }

            public int GetHashCode(EvaluationEntity obj)
            {
                return HashCode.Combine(obj.Id, obj.TextEvaluation, obj.PercentEvaluation, obj.GoodsId, obj.Goods);
            }
        }

        public static IEqualityComparer<EvaluationEntity> EvaluationEntityComparer { get; } = new EvaluationEntityEqualityComparer();
    }
}
