using System;
using System.Collections.Generic;

namespace PcShopIW5_DAL.Entities
{
    public class GoodsEntity : IEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string Photo { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
        public string Weight { get; set; }
        public string CountInStock { get; set; }

        public Guid ManufacturerId { get; set; }
        public virtual ManufacturerEntity Manufacturer { get; set; }

        public virtual ICollection<GoodsCategoryEntity> GoodsCategories { get; set; } = new List<GoodsCategoryEntity>();

        public virtual ICollection<EvaluationEntity> Evaluations { get; set; } = new List<EvaluationEntity>();

        private sealed class GoodsEntityEqualityComparer : IEqualityComparer<GoodsEntity>
        {
            public bool Equals(GoodsEntity x, GoodsEntity y)
            {
                if (x is null && y is null) return true;
                if (x is null || y is null) return false;
                if (x.GetType() != y.GetType()) return false;
                return x.Id.Equals(y.Id) && x.Name == y.Name 
                                         && x.Photo == y.Photo 
                                         && x.Description == y.Description 
                                         && x.Price == y.Price 
                                         && x.Weight == y.Weight 
                                         && x.CountInStock == y.CountInStock
                                         && x.ManufacturerId.Equals(y.ManufacturerId) 
                                         && Equals(x.Manufacturer, y.Manufacturer) 
                                         && Equals(x.GoodsCategories, y.GoodsCategories)
                                         && Equals(x.Evaluations, y.Evaluations);
            }

            public int GetHashCode(GoodsEntity obj)
            {
                var hashCode = new HashCode();
                hashCode.Add(obj.Id);
                hashCode.Add(obj.Name);
                hashCode.Add(obj.Photo);
                hashCode.Add(obj.Description);
                hashCode.Add(obj.Price);
                hashCode.Add(obj.Weight);
                hashCode.Add(obj.CountInStock);
                hashCode.Add(obj.ManufacturerId);
                hashCode.Add(obj.Manufacturer);
                hashCode.Add(obj.GoodsCategories);
                hashCode.Add(obj.Evaluations);
                return hashCode.ToHashCode();
            }
        }

        public static IEqualityComparer<GoodsEntity> GoodsEntityComparer { get; } = new GoodsEntityEqualityComparer();
    }
}
