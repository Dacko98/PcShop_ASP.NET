using System;
using System.Linq;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using PcShopIW5_DAL.Entities;

namespace PcShopIW5_DAL.Factories
{
    public class EntityFactory
    {
        private readonly ChangeTracker _changeTracker;

        public EntityFactory(ChangeTracker changeTracker) => _changeTracker = changeTracker;

        public TEntity Create<TEntity>(Guid id) where TEntity : class, IEntity, new()
        {
            TEntity entity;
            if (id != Guid.Empty)
            {
                entity = _changeTracker?.Entries<TEntity>()
                    .SingleOrDefault(i => i.Entity.Id == id)
                    ?.Entity;
                if (entity == null)
                {
                    entity = new TEntity {Id = id};
                    _changeTracker?.Context.Attach(entity);
                }
            }
            else
            {
                entity = new TEntity();
            }

            return entity;
        }
    }
}