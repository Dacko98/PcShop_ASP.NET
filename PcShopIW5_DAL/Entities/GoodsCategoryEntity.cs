using System;
using System.Collections.Generic;

namespace PcShopIW5_DAL.Entities
{
    public class GoodsCategoryEntity : IEntity
    {
        public Guid Id { get; set; }

        public Guid GoodsId { get; set; }
        public virtual GoodsEntity Goods { get; set; }

        public Guid CategoryId { get; set; }
        public virtual CategoryEntity Category { get; set; }

        private sealed class GoodsCategoryEntityEqualityComparer : IEqualityComparer<GoodsCategoryEntity>
        {
            public bool Equals(GoodsCategoryEntity x, GoodsCategoryEntity y)
            {
                if (x is null && y is null) return true;
                if (x is null || y is null) return false;
                if (x.GetType() != y.GetType()) return false;
                return x.Id.Equals(y.Id) && x.GoodsId.Equals(y.GoodsId) 
                                         && Equals(x.Goods, y.Goods)
                                         && x.CategoryId.Equals(y.CategoryId) 
                                         && Equals(x.Category, y.Category);
            }

            public int GetHashCode(GoodsCategoryEntity obj)
            {
                return HashCode.Combine(obj.Id, obj.GoodsId, obj.Goods, obj.CategoryId, obj.Category);
            }
        }

        public static IEqualityComparer<GoodsCategoryEntity> GoodsCategoryEntityComparer { get; } = new GoodsCategoryEntityEqualityComparer();
    }
}
