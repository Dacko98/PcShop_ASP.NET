using System;
using PcShopIW5_DAL.Entities;

namespace PcShopIW5_DAL.Factories
{
    public interface IEntityFactory
    {
        TEntity Create<TEntity>(Guid id) where TEntity : class, IEntity, new();
    }
}