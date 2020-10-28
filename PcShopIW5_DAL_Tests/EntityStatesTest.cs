using System;
using Microsoft.EntityFrameworkCore;
using PcShopIW5_DAL;
using PcShopIW5_DAL.Entities;
using PcShopIW5_DAL.Factories;
using Xunit;

namespace PcShopIW5_DAL_Tests
{
    public class EntityStatesTest : IDisposable
    {
        private readonly PcShopDbContext _pcShopDbContext;

        public EntityStatesTest()
        {
            var dbContextFactory = new DbContextInMemoryFactory(nameof(EntityStatesTest));
            _pcShopDbContext = dbContextFactory.Create();
            _pcShopDbContext.Database.EnsureCreated();
        }

        readonly GoodsEntity _goodsEntity = new GoodsEntity()
        {
            Name = "Random PC 1"
        };

        [Fact]
        public void AddedStateTest()
        {
            _pcShopDbContext.Goods.Add(_goodsEntity);
            Assert.Equal(EntityState.Added, _pcShopDbContext.Entry(_goodsEntity).State);
        }

        [Fact]
        public void UnchangedStateTest()
        {
            _pcShopDbContext.Goods.Add(_goodsEntity);
            _pcShopDbContext.SaveChanges();
            Assert.Equal(EntityState.Unchanged, _pcShopDbContext.Entry(_goodsEntity).State);
        }

        [Fact]
        public void ModifiedStateTest()
        {
            var entityEntry = _pcShopDbContext.Goods.Add(_goodsEntity);
            _pcShopDbContext.SaveChanges();
            entityEntry.Entity.Name = "Random PC 2";
            Assert.Equal(EntityState.Modified, _pcShopDbContext.Entry(_goodsEntity).State);
        }

        [Fact]
        public void DeletedStateTest()
        {
            _pcShopDbContext.Goods.Add(_goodsEntity);
            _pcShopDbContext.SaveChanges();
            _pcShopDbContext.Remove(_goodsEntity);
            Assert.Equal(EntityState.Deleted, _pcShopDbContext.Entry(_goodsEntity).State);
        }

        [Fact]
        public void DetachedStateTest()
        {
            Assert.Equal(EntityState.Detached, _pcShopDbContext.Entry(_goodsEntity).State);
        }

        public void Dispose()
        {
            _pcShopDbContext?.Dispose();
        }
    }
}
