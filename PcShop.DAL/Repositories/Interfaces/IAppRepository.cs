using System;
using System.Collections.Generic;
using PcShop.DAL.Entities;

namespace PcShop.DAL.Repositories
{
    public interface IAppRepository<TEntity>
        where TEntity : IEntity
    {
        IList<TEntity> GetAll();
        TEntity GetById(Guid id);
        Guid Insert(TEntity entity);
        Guid? Update(TEntity entity);
        void Remove(Guid id);
    }
}