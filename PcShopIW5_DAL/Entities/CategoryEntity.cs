using System;
using System.Collections.Generic;

namespace PcShopIW5_DAL.Entities
{
    public class CategoryEntity : IEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<GoodsCategoryEntity> GoodsCategories { get; set; } = new List<GoodsCategoryEntity>();

        private sealed class IdNameGoodsCategoriesEqualityComparer : IEqualityComparer<CategoryEntity>
        {
            public bool Equals(CategoryEntity x, CategoryEntity y)
            {
                if (x is null && y is null) return true;
                if (x is null || y is null) return false;
                if (x.GetType() != y.GetType()) return false;
                return x.Id.Equals(y.Id) && x.Name == y.Name 
                                         && Equals(x.GoodsCategories, y.GoodsCategories);
            }

            public int GetHashCode(CategoryEntity obj)
            {
                return HashCode.Combine(obj.Id, obj.Name, obj.GoodsCategories);
            }
        }

        public static IEqualityComparer<CategoryEntity> IdNameGoodsCategoriesComparer { get; } = new IdNameGoodsCategoriesEqualityComparer();
    }
}
