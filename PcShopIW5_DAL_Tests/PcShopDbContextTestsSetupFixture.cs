using System;
using Microsoft.EntityFrameworkCore;
using PcShopIW5_DAL;


namespace PcShopIW5_DAL_Tests
{
    public class PcShopDbContextTestsSetupFixture : IDisposable
    {
        public PcShopDbContextTestsSetupFixture()
        {
            PcShopDbContext = CreatePcShopDbContext();
        }

        public PcShopDbContext PcShopDbContext { get; set; }

        public void Dispose()
        {
            PcShopDbContext?.Dispose();
        }

        public PcShopDbContext CreatePcShopDbContext()
        {
            return new PcShopDbContext(CreateDbContextOptions());
        }

        private DbContextOptions<PcShopDbContext> CreateDbContextOptions()
        {
            var contextOptionsBuilder = new DbContextOptionsBuilder<PcShopDbContext>();
            contextOptionsBuilder.UseInMemoryDatabase("TestInMemoryDb");
            return contextOptionsBuilder.Options;
        }
    }
}