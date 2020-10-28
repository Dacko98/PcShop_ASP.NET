using Microsoft.EntityFrameworkCore;
using PcShopIW5_DAL.Entities;
using PcShopIW5_DAL.Seeds;

namespace PcShopIW5_DAL
{
    public class PcShopDbContext : DbContext
    { 
        public PcShopDbContext(DbContextOptions contextOptions)
            : base(contextOptions)
        {
        }

        public DbSet<CategoryEntity> Category { get; set; }
        public DbSet<EvaluationEntity> Evaluation { get; set; }
        public DbSet<GoodsCategoryEntity> GoodsCategory { get; set; }
        public DbSet<GoodsEntity> Goods { get; set; }
        public DbSet<ManufacturerEntity> Manufacturer { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<GoodsCategoryEntity>()
                .HasIndex(sc => new { sc.GoodsId, sc.CategoryId }).IsUnique();

            modelBuilder.SeedCategories();
            modelBuilder.SeedEvaluations();
            modelBuilder.SeedGoods();
            modelBuilder.SeedGoodsCategories();
            modelBuilder.SeedManufacturers();
        }
    }
}
