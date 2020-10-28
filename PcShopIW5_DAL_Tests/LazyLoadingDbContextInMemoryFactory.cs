using Microsoft.EntityFrameworkCore;
using PcShopIW5_DAL.Factories;
using PcShopIW5_DAL;

namespace PcShopIW5_DAL_Tests
{
    public class LazyLoadingDbContextInMemoryFactory : IDbContextFactory<PcShopDbContext>
    {
        private readonly string _databaseName;

        public LazyLoadingDbContextInMemoryFactory(string databaseName)
        {
            _databaseName = databaseName;
        }
        public PcShopDbContext Create()
        {
            var contextOptionsBuilder = new DbContextOptionsBuilder<PcShopDbContext>();
            contextOptionsBuilder.UseInMemoryDatabase(_databaseName);
            contextOptionsBuilder.UseLazyLoadingProxies();
            return new PcShopDbContext(contextOptionsBuilder.Options);
        }
    }
}