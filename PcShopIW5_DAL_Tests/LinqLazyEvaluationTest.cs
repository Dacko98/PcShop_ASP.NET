using System;
using System.Collections.Generic;
using System.Linq;
using PcShopIW5_DAL;
using PcShopIW5_DAL.Entities;
using PcShopIW5_DAL.Factories;
using PcShopIW5_DAL.Seeds;
using Xunit;

namespace PcShopIW5_DAL_Tests
{
    public class LinqLazyEvaluationTest : IDisposable
    {
        private readonly PcShopDbContext _pcShopDbContext;
        private readonly DbContextInMemoryFactory _dbContextFactory;

        public LinqLazyEvaluationTest()
        {
            _dbContextFactory = new DbContextInMemoryFactory(nameof(EntityStatesTest));
            _pcShopDbContext = _dbContextFactory.Create();
            _pcShopDbContext.Database.EnsureCreated();
        }

        [Fact]
        public void LazyEvaluationTest()
        {
            IEnumerable<GoodsEntity> students;
            using (var pcShopDbContext = _dbContextFactory.Create())
            {
                students = pcShopDbContext.Goods.Where(s => s.Id == Seed.GoodsAsusOfficePc.Id);
            }

            //Materialized outside of using scope
            Assert.Throws<ObjectDisposedException>(() => students.ToList());
        }

        public void Dispose()
        {
            _pcShopDbContext?.Dispose();
        }
    }
}