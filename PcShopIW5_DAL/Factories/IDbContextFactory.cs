using Microsoft.EntityFrameworkCore;

namespace PcShopIW5_DAL.Factories
{
    public interface IDbContextFactory<out TDbContext> where TDbContext : DbContext
    {
        TDbContext Create();
    }
}