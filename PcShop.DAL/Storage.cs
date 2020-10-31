using System;
using System.Collections;
using System.Collections.Generic;
using PcShop.DAL.Entities;

namespace PcShop.DAL
{
    public class Storage
    {
        private readonly IList<Guid> EvaluationGuids = new List<Guid>
        {
            new Guid("df935095-8709-4040-a2bb-b6f97cb416dc"),
            new Guid("23b3902d-7d4f-4213-9cf0-112348f56239"),
            new Guid("23b3902d-7d4f-4213-9cf0-112348f56240"),
            new Guid("23b3902d-7d4f-4213-9cf0-112348f56230"),
        };

        private readonly IList<Guid> ManufacturerGuids = new List<Guid>
        {
            new Guid("0d4fa150-ad80-4d46-a511-4c666166ec5e"),
            new Guid("87833e66-05ba-4d6b-900b-fe5ace88dbd8")
        };

        private readonly IList<Guid> CategoryGuids = new List<Guid>
        {
            new Guid("fabde0cd-eefe-443f-baf6-3d96cc2cbf2e"),
            new Guid("23b3902d-7d4f-4213-9cf0-112348f56238"),
        };

        private readonly IList<Guid> ProductGuids = new List<Guid>
        {
            new Guid("0d4fa150-ad80-4d46-a511-4c666166ec5a"),
            new Guid("23b3902d-7d4f-4213-9cf0-112348f56288"),
            new Guid("23b3902d-7d4f-4213-9cf0-112348f56244"),
            new Guid("df935095-8709-4040-a2bb-b6f97cb416dc"),
            new Guid("23b3902d-7d4f-4213-9cf0-112348f56233"),
            new Guid("df935095-8709-4040-a2bb-b6f97cb416bb")
        };

        public IList<EvaluationEntity> Evaluations { get; } = new List<EvaluationEntity>();
        public IList<ManufacturerEntity> Manufacturers { get; } = new List<ManufacturerEntity>();
        public IList<CategoryEntity> Categories { get; } = new List<CategoryEntity>();
        public IList<ProductEntity> Product { get; } = new List<ProductEntity>();


        public Storage()
        {
            SeedEvaluations();
            SeedManufacturers();
            SeedCategory();
            SeedProduct();
        }

        private void SeedEvaluations()
        {
            Evaluations.Add(new EvaluationEntity
            {
                Id = EvaluationGuids[0],
                TextEvaluation = "Good",
                PercentEvaluation = 80,
                ProductId = ProductGuids[0]

            });
            Evaluations.Add(new EvaluationEntity
            {
                Id = EvaluationGuids[1],
                TextEvaluation = "Not bad",
                PercentEvaluation = 60,
                ProductId = ProductGuids[1]
            });
            Evaluations.Add(new EvaluationEntity
            {
                Id = EvaluationGuids[2],
                TextEvaluation = "Little bit expensive",
                PercentEvaluation = 50,
                ProductId = ProductGuids[1]
            });
            Evaluations.Add(new EvaluationEntity
            {
                Id = EvaluationGuids[3],
                TextEvaluation = "Broke after one month",
                PercentEvaluation = 0,
                ProductId = ProductGuids[2]
            });
        }

        private void SeedManufacturers()
        {
            Manufacturers.Add(new ManufacturerEntity
            {
                Id = ManufacturerGuids[0],
                Name = "Dell",
                Description = "...",
                Logo = "Path",
                CountryOfOrigin = "USA",
            });
            Manufacturers.Add(new ManufacturerEntity
            {
                Id = ManufacturerGuids[1],
                Name = "Lenovo",
                Description = "...",
                Logo = "Path",
                CountryOfOrigin = "China",
            });
        }

        private void SeedCategory()
        {
            Categories.Add(new CategoryEntity
            {
                Id = CategoryGuids[0],
                Name = "Professional"
            });
            Categories.Add(new CategoryEntity
            {
                Id = CategoryGuids[1],
                Name = "Graphic design"
            });
        }

        private void SeedProduct()
        {
            Product.Add(new ProductEntity
            {
                Id = ProductGuids[0],
                Name = "Lattitude E6440",
                Description = "...",
                Price = 600,
                Weight = 200,
                CountInStock = 10,
                Photo = "path",
                ManufacturerId = ManufacturerGuids[0],
                CategoryId = CategoryGuids[1],


            });
            Product.Add(new ProductEntity
            {
                Id = ProductGuids[1],
                Name = "Lattitude 9100",
                Description = "...",
                Price = 1500,
                Weight = 200,
                CountInStock = 10,
                Photo = "path",
                ManufacturerId = ManufacturerGuids[0],
                CategoryId = CategoryGuids[0],
            });
            Product.Add(new ProductEntity
            {
                Id = ProductGuids[2],
                Name = "Thinkpad T560",
                Description = "...",
                Price = 800,
                Weight = 200,
                CountInStock = 10,
                Photo = "path",
                ManufacturerId = ManufacturerGuids[1],
                CategoryId = CategoryGuids[0],
            });
            Product.Add(new ProductEntity
            {
                Id = ProductGuids[3],
                Name = "Ideapad 4587",
                Description = "...",
                Price = 600,
                Weight = 200,
                CountInStock = 10,
                Photo = "path",
                ManufacturerId = ManufacturerGuids[1],
                CategoryId = CategoryGuids[1],
            });
            Product.Add(new ProductEntity
            {
                Id = ProductGuids[5],
                Name = "XPS 6300",
                Description = "...",
                Price = 2000,
                Weight = 200,
                CountInStock = 10,
                Photo = "path",
                ManufacturerId = ManufacturerGuids[0],
                CategoryId = CategoryGuids[1],
            });
            Product.Add(new ProductEntity
            {
                Id = ProductGuids[4],
                Name = "Thinkpad L580",
                Description = "...",
                Price = 1000,
                Weight = 200,
                CountInStock = 10,
                Photo = "path",
                ManufacturerId = ManufacturerGuids[1],
                CategoryId = CategoryGuids[0],
            });
        }
    }
}