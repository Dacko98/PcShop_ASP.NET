using System;
using System.Collections.Generic;
using System.Text;
using PcShopIW5_DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace PcShopIW5_DAL.Seeds
{
    public static class Seed
    {
        public static readonly CategoryEntity CategoryGaming = new CategoryEntity
        {
            Id = Guid.Parse("f49f3131-b9b9-4d0f-b00c-21eba22b0b91"),
            Name = "Gaming",
            GoodsCategories = new List<GoodsCategoryEntity>()
        };

        public static readonly ManufacturerEntity ManufacturerAsus = new ManufacturerEntity
        {
            Id = Guid.Parse("6c215111-dd97-4fae-95b8-80fe9147e3c8"),
            Name = "Asus",
            CountryOfOrigin = "China",
            Description = "Well known manufacturer",
            Logo = "url/to/logo.png",
            Goods = new List<GoodsEntity>()
        };

        public static readonly GoodsEntity GoodsAsusOfficePc = new GoodsEntity
        {
            Id = Guid.Parse("b3bd0e04-473b-4a08-ac1f-2bbca4783463"),
            Name = "AsusOfficePc",
            CountInStock = "18",
            Description = "Intel i5 8000U, RAM 8GB, SSD 250GB, HDD 1TB, Intel HD Graphics 520, 14\", 1920 x 1080 px, Windows 10",
            Photo = "url/to/photo.png",
            Price = "$500",
            Weight = "2kg",
            Manufacturer = ManufacturerAsus,
            ManufacturerId = ManufacturerAsus.Id,
            GoodsCategories = new List<GoodsCategoryEntity>(),
            Evaluations = new List<EvaluationEntity>()
        };

        public static readonly EvaluationEntity EvaluationBad = new EvaluationEntity
        {
            Id = Guid.Parse("99b80200-ff4b-4f07-b1ef-4e07b2682770"),
            PercentEvaluation = "30",
            TextEvaluation = "Really bad PC",
            Goods = GoodsAsusOfficePc,
            GoodsId = GoodsAsusOfficePc.Id
        };

        public static readonly GoodsCategoryEntity GoodsAsusOfficePcCategoryGaming = new GoodsCategoryEntity
        {
            Id = Guid.Parse("70b63184-38ee-4482-b655-c74560c278cd"),
            GoodsId = GoodsAsusOfficePc.Id,
            Goods = GoodsAsusOfficePc,
            CategoryId = CategoryGaming.Id,
            Category = CategoryGaming
        };

        static Seed()
        {
            ManufacturerAsus.Goods.Add(GoodsAsusOfficePc);
            GoodsAsusOfficePc.Evaluations.Add(EvaluationBad);

            // todo Foreign Keys
            //GoodsAsusOfficePc.GoodsCategories.Add(GoodsAsusOfficePcCategoryGaming);
            //CategoryGaming.GoodsCategories.Add(GoodsAsusOfficePcCategoryGaming);
        }

        public static void SeedCategories(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CategoryEntity>()
                .HasData(CategoryGaming);
        }

        public static void SeedEvaluations(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EvaluationEntity>()
                .HasData(new EvaluationEntity
                {
                    Id = EvaluationBad.Id,
                    PercentEvaluation = EvaluationBad.PercentEvaluation,
                    TextEvaluation = EvaluationBad.TextEvaluation
                });
        }

        public static void SeedGoods(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GoodsEntity>()
                .HasData(new GoodsEntity
                {
                    Id = GoodsAsusOfficePc.Id,
                    Name = GoodsAsusOfficePc.Name,
                    CountInStock = GoodsAsusOfficePc.CountInStock,
                    Description = GoodsAsusOfficePc.Description,
                    Photo = GoodsAsusOfficePc.Photo,
                    Price = GoodsAsusOfficePc.Price,
                    Weight = GoodsAsusOfficePc.Weight,
                    ManufacturerId = GoodsAsusOfficePc.ManufacturerId
                });
        }

        public static void SeedManufacturers(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ManufacturerEntity>()
                .HasData(new ManufacturerEntity
                {
                    Id = ManufacturerAsus.Id,
                    CountryOfOrigin = ManufacturerAsus.CountryOfOrigin,
                    Logo = ManufacturerAsus.Logo,
                    Description = ManufacturerAsus.Description,
                    Name = ManufacturerAsus.Name
                });
        }

        public static void SeedGoodsCategories(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GoodsCategoryEntity>()
                .HasData(new GoodsCategoryEntity
                {
                    Id = GoodsAsusOfficePcCategoryGaming.Id,
                    CategoryId = GoodsAsusOfficePcCategoryGaming.CategoryId,
                    GoodsId = GoodsAsusOfficePcCategoryGaming.GoodsId
                });
        }
    }
}
