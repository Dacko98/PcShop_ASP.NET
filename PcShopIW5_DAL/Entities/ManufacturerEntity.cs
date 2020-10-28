using System;
using System.Collections.Generic;

namespace PcShopIW5_DAL.Entities
{
    public class ManufacturerEntity : IEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string Logo { get; set; }
        public string CountryOfOrigin { get; set; }

        public virtual ICollection<GoodsEntity> Goods { get; set; } = new List<GoodsEntity>();

        private sealed class ManufacturerEntityEqualityComparer : IEqualityComparer<ManufacturerEntity>
        {
            public bool Equals(ManufacturerEntity x, ManufacturerEntity y)
            {
                if (x is null && y is null) return true;
                if (x is null || y is null) return false;
                if (x.GetType() != y.GetType()) return false;
                return x.Id.Equals(y.Id) && x.Name == y.Name 
                                         && x.Description == y.Description 
                                         && x.Logo == y.Logo 
                                         && x.CountryOfOrigin == y.CountryOfOrigin 
                                         && Equals(x.Goods, y.Goods);
            }

            public int GetHashCode(ManufacturerEntity obj)
            {
                return HashCode.Combine(obj.Id, obj.Name, obj.Description, obj.Logo, obj.CountryOfOrigin, obj.Goods);
            }
        }

        public static IEqualityComparer<ManufacturerEntity> ManufacturerEntityComparer { get; } = new ManufacturerEntityEqualityComparer();
    }
}
