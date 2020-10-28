using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace PcShopIW5_DAL.Factories
{
    /// <summary>
    /// Allows to use `dotnet ef migrations add xxx`
    /// https://docs.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.design.idesigntimedbcontextfactory-1
    /// https://codingblast.com/entityframework-core-idesigntimedbcontextfactory/
    /// </summary>
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<PcShopDbContext>
    {
        public PcShopDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<PcShopDbContext>();
            // https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-strings
            // todo move to configuration
            // todo do it correctly
            builder.UseSqlServer(
                @"Data Source=(LocalDB)\MSSQLLocalDB;
                Database = PcShop;
                Initial Catalog = PcShop;
                MultipleActiveResultSets = True;
                Integrated Security = True; ");

            return new PcShopDbContext(builder.Options);
        }
    }
}