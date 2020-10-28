using System;
using System.Collections.Generic;
using PcShopIW5_DAL.Entities;

namespace PcShopIW5_DAL.Repositories
{
    public static class PrimaryKeyComparers
    {
        public static IEqualityComparer<IEntity> IdComparer { get; } = new IdEqualityComparer<Guid>();

        private sealed class IdEqualityComparer<TKey> : IEqualityComparer<IEntity>
        {
            public bool Equals(IEntity x, IEntity y)
            {
                if (x is null && y is null) return true;
                if (x is null || y is null) return false;
                return x.Id.Equals(y.Id);
            }

            public int GetHashCode(IEntity obj) => obj.Id.GetHashCode();
        }
    }
}