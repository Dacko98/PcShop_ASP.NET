using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PcShopIW5_DAL;
using PcShopIW5_DAL.Entities;
using PcShopIW5_DAL.Factories;
using PcShopIW5_DAL.Seeds;
using PcShopIW5_DAL_Tests;
using Xunit;

namespace PcShopIW5_DAL_Tests
{
    public class EntityTypesTest : IDisposable
    {
        private readonly PcShopDbContext _pcShopDbContext;
        private const string DbName = nameof(EntityStatesTest);

        public EntityTypesTest()
        {
            var dbContextFactory = new DbContextInMemoryFactory(DbName);
            _pcShopDbContext = dbContextFactory.Create();
            _pcShopDbContext.Database.EnsureCreated();
        }
        
        [Fact]
        public void POCO_EntitiesTest()
        {
            var pc = _pcShopDbContext.Goods.Single(a => a.Id == Seed.GoodsAsusOfficePc.Id);

            Assert.Null(pc.Manufacturer);
        }

        [Fact]
        public void POCO_EntitiesIncludeTest()
        {
            // todo I am not sure... 
            var pc = _pcShopDbContext
                .Goods
                //.Include(i => i.CountInStock)
                //.Include(i => i.Description)
                //.Include(i => i.Name)
                //.Include(i => i.Photo)
                //.Include(i => i.Price)
                //.Include(i => i.Weight)
                //.Include(i => i.ManufacturerId)
                //.Include(i => i.Manufacturer) 
                //.Include(i => i.Evaluations)
                .Include(i => i.GoodsCategories)
                .ThenInclude(i => i.Category)
                .Single(i => i.Id == Seed.GoodsAsusOfficePc.Id);

            Assert.Equal(pc, Seed.GoodsAsusOfficePc, GoodsEntity.GoodsEntityComparer);
        }

        [Fact]
        public void POCO_ProxyTest()
        {
            var lazyLoadingDbContextInMemoryFactory = new LazyLoadingDbContextInMemoryFactory(DbName);

            using var pcShopDbContext = lazyLoadingDbContextInMemoryFactory.Create();
            pcShopDbContext.Database.EnsureCreated();

            var pc = pcShopDbContext.Goods.Single(a => a.Id == Seed.GoodsAsusOfficePc.Id);

            Assert.Equal(pc, Seed.GoodsAsusOfficePc, GoodsEntity.GoodsEntityComparer);
        }

        public void Dispose()
        {
            _pcShopDbContext?.Dispose();
        }
    }
}