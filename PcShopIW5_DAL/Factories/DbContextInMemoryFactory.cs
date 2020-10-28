using Microsoft.EntityFrameworkCore;

namespace PcShopIW5_DAL.Factories
{
    public class DbContextInMemoryFactory : IDbContextFactory<PcShopDbContext>
    {
        private readonly string _databaseName;

        public DbContextInMemoryFactory(string databaseName)
        {
            _databaseName = databaseName;
        }
        public PcShopDbContext Create()
        {
            var contextOptionsBuilder = new DbContextOptionsBuilder<PcShopDbContext>();
            contextOptionsBuilder.UseInMemoryDatabase(_databaseName);
            return new PcShopDbContext(contextOptionsBuilder.Options);
        }
    }
}
