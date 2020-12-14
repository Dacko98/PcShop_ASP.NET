using System;
using System.Collections.Generic;
using PcShop.DAL.Entities;

namespace PcShop.DAL
{
    public class Storage
    {
        private readonly IList<Guid> _evaluationGuids = new List<Guid> // 50
        {
            new Guid("df935095-8709-4040-a2bb-b6f97cb416dc"),
            new Guid("23b3902d-7d4f-4213-9cf0-112348f56239"),
            new Guid("23b3902d-7d4f-4213-9cf0-112348f56240"),
            new Guid("23b3902d-7d4f-4213-9cf0-112348f56230"),
            new Guid("23b3902d-7d4f-4213-9cf0-112348f56231"),
            new Guid("23b3902d-7d4f-4213-9cf0-112348f56232"),
            new Guid("23b3902d-7d4f-4213-9cf0-112348f56233"),
            new Guid("23b3902d-7d4f-4213-9cf0-112348f56234"),
            new Guid("23b3902d-7d4f-4213-9cf0-112348f56235"),
            new Guid("23b3902d-7d4f-4213-9cf0-112348f56236"),

            new Guid("df935095-8709-4040-a2bb-b6f97cbf0000"),
            new Guid("df935095-8709-4040-a2bb-b6f97cbf1001"),
            new Guid("df935095-8709-4040-a2bb-b6f97cbf2002"),
            new Guid("df935095-8709-4040-a2bb-b6f97cbf3003"),
            new Guid("df935095-8709-4040-a2bb-b6f97cbf4004"),
            new Guid("df935095-8709-4040-a2bb-b6f97cbf5005"),
            new Guid("df935095-8709-4040-a2bb-b6f97cbf6006"),
            new Guid("df935095-8709-4040-a2bb-b6f97cbf7007"),
            new Guid("df935095-8709-4040-a2bb-b6f97cbf8008"),
            new Guid("df935095-8709-4040-a2bb-b6f97cbf9009"),

            new Guid("df935095-8709-4040-a2bb-b6f97cbf0010"),
            new Guid("df935095-8709-4040-a2bb-b6f97cbf1011"),
            new Guid("df935095-8709-4040-a2bb-b6f97cbf2012"),
            new Guid("df935095-8709-4040-a2bb-b6f97cbf3013"),
            new Guid("df935095-8709-4040-a2bb-b6f97cbf4014"),
            new Guid("df935095-8709-4040-a2bb-b6f97cbf5015"),
            new Guid("df935095-8709-4040-a2bb-b6f97cbf6016"),
            new Guid("df935095-8709-4040-a2bb-b6f97cbf7017"),
            new Guid("df935095-8709-4040-a2bb-b6f97cbf8018"),
            new Guid("df935095-8709-4040-a2bb-b6f97cbf9019"),

            new Guid("df935095-8709-4040-a2bb-b6f97cbf0020"),
            new Guid("df935095-8709-4040-a2bb-b6f97cbf1021"),
            new Guid("df935095-8709-4040-a2bb-b6f97cbf2022"),
            new Guid("df935095-8709-4040-a2bb-b6f97cbf3023"),
            new Guid("df935095-8709-4040-a2bb-b6f97cbf4024"),
            new Guid("df935095-8709-4040-a2bb-b6f97cbf5025"),
            new Guid("df935095-8709-4040-a2bb-b6f97cbf6026"),
            new Guid("df935095-8709-4040-a2bb-b6f97cbf7027"),
            new Guid("df935095-8709-4040-a2bb-b6f97cbf8028"),
            new Guid("df935095-8709-4040-a2bb-b6f97cbf9029"),

            new Guid("df935095-8709-4040-a2bb-b6f97cbf0030"),
            new Guid("df935095-8709-4040-a2bb-b6f97cbf1031"),
            new Guid("df935095-8709-4040-a2bb-b6f97cbf2032"),
            new Guid("df935095-8709-4040-a2bb-b6f97cbf3033"),
            new Guid("df935095-8709-4040-a2bb-b6f97cbf4034"),
            new Guid("df935095-8709-4040-a2bb-b6f97cbf5035"),
            new Guid("df935095-8709-4040-a2bb-b6f97cbf6036"),
            new Guid("df935095-8709-4040-a2bb-b6f97cbf7037"),
            new Guid("df935095-8709-4040-a2bb-b6f97cbf8038"),
            new Guid("df935095-8709-4040-a2bb-b6f97cbf9039"),

        };

        private readonly IList<Guid> _manufacturerGuids = new List<Guid>
        {
            new Guid("0d4fa150-ad80-4d46-a511-4c666166ec5e"),
            new Guid("87833e66-05ba-4d6b-900b-fe5ace88dbd8"),
            new Guid("87833e66-05ba-4d6b-900b-fe5ace88dba1"),
            new Guid("87833e66-05ba-4d6b-900b-fe5ace88dba2"),
            new Guid("87833e66-05ba-4d6b-900b-fe5ace88dba3"),
            new Guid("87833e66-05ba-4d6b-900b-fe5ace88dba4"),
            new Guid("87833e66-05ba-4d6b-900b-fe5ace88dba5"),
            new Guid("87833e66-05ba-4d6b-900b-fe5ace88dba6"),
            new Guid("87833e66-05ba-4d6b-900b-fe5ace88dba7"),
            new Guid("87833e66-05ba-4d6b-900b-fe5ace88dba8")
        };

        private readonly IList<Guid> _categoryGuids = new List<Guid>
        {
            new Guid("fabde0cd-eefe-443f-baf6-3d96cc2cbf2e"),
            new Guid("23b3902d-7d4f-4213-9cf0-115748f56238"),
            new Guid("23b3902d-7d4f-4213-9cf0-112348f56238"),
        };

        private readonly IList<Guid> _productGuids = new List<Guid>
        {
            new Guid("23b3902d-7d4f-4213-9cf0-112348f56137"),
            new Guid("23b3902d-7d4f-4213-9cf0-112348f56248"),
            new Guid("23b3902d-7d4f-4213-9cf0-112348f56339"),
            new Guid("23b3902d-7d4f-4213-9cf0-112348f56437"),
            new Guid("23b3902d-7d4f-4213-9cf0-112348f56538"),
            new Guid("23b3902d-7d4f-4213-9cf0-112348f56639"),
            new Guid("23b3902d-7d4f-4213-9cf0-112348f56737"),
            new Guid("23b3902d-7d4f-4213-9cf0-112348f56838"),
            new Guid("23b3902d-7d4f-4213-9cf0-112348f56939"),
            new Guid("23b3902d-7d4f-4213-9cf0-112348f56a37"),

            new Guid("23b3902d-7d4f-4213-9cf0-a12348f56137"),
            new Guid("23b3902d-7d4f-4213-9cf0-212348f56248"),
            new Guid("23b3902d-7d4f-4213-9cf0-312348f56339"),
            new Guid("23b3902d-7d4f-4213-9cf0-412348f56437"),
            new Guid("23b3902d-7d4f-4213-9cf0-512348f56538"),
            new Guid("23b3902d-7d4f-4213-9cf0-612348f56639"),
            new Guid("23b3902d-7d4f-4213-9cf0-712348f56737"),
            new Guid("23b3902d-7d4f-4213-9cf0-812348f56838"),
            new Guid("23b3902d-7d4f-4213-9cf0-912348f56939"),
            new Guid("23b3902d-7d4f-4213-9cf0-b12348f56a37"),

            new Guid("23b3902d-7d4f-4213-9cf0-1b2348f56137"),
            new Guid("23b3902d-7d4f-4213-9cf0-122348f56248"),
            new Guid("23b3902d-7d4f-4213-9cf0-132348f56339"),
            new Guid("23b3902d-7d4f-4213-9cf0-142348f56437"),
            new Guid("23b3902d-7d4f-4213-9cf0-152348f56538"),
            new Guid("23b3902d-7d4f-4213-9cf0-162348f56639"),
            new Guid("23b3902d-7d4f-4213-9cf0-172348f56737"),
            new Guid("23b3902d-7d4f-4213-9cf0-182348f56838"),
            new Guid("23b3902d-7d4f-4213-9cf0-192348f56939"),
            new Guid("23b3902d-7d4f-4213-9cf0-1a2348f56a37"),

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
                Id = _evaluationGuids[0],
                TextEvaluation = "Good",
                PercentEvaluation = 80,
                ProductId = _productGuids[0]

            });
            Evaluations.Add(new EvaluationEntity
            {
                Id = _evaluationGuids[1],
                TextEvaluation = "Not bad",
                PercentEvaluation = 60,
                ProductId = _productGuids[1]
            });
            Evaluations.Add(new EvaluationEntity
            {
                Id = _evaluationGuids[2],
                TextEvaluation = "Little bit expensive Little bit expensive Little bit expensive Little bit expensive Little bit expensive Little bit expensive Little bit expensive Little bit expensive Little bit expensive Little bit expensive Little bit expensive Little bit expensive Little bit expensive Little bit expensive Little bit expensive Little bit expensive Little bit expensive Little bit expensive Little bit expensive Little bit expensive Little bit expensive Little bit expensive ",
                PercentEvaluation = 50,
                ProductId = _productGuids[1]
            });
            Evaluations.Add(new EvaluationEntity
            {
                Id = _evaluationGuids[3],
                TextEvaluation = "Broke after one month",
                PercentEvaluation = 0,
                ProductId = _productGuids[2]
            });
            Evaluations.Add(new EvaluationEntity
            {
                Id = _evaluationGuids[4],
                TextEvaluation = "Don't recommend",
                PercentEvaluation = 20,
                ProductId = _productGuids[2]
            });
            Evaluations.Add(new EvaluationEntity
            {
                Id = _evaluationGuids[5],
                TextEvaluation = "Don't recommend",
                PercentEvaluation = 30,
                ProductId = _productGuids[3]
            });
            Evaluations.Add(new EvaluationEntity
            {
                Id = _evaluationGuids[6],
                TextEvaluation = "Don't recommend",
                PercentEvaluation = 30,
                ProductId = _productGuids[5]
            });
            Evaluations.Add(new EvaluationEntity
            {
                Id = _evaluationGuids[7],
                TextEvaluation = "Don't recommend",
                PercentEvaluation = 0,
                ProductId = _productGuids[2]
            });
            Evaluations.Add(new EvaluationEntity
            {
                Id = _evaluationGuids[8],
                TextEvaluation = "Don't recommend",
                PercentEvaluation = 0,
                ProductId = _productGuids[7]
            });
            Evaluations.Add(new EvaluationEntity
            {
                Id = _evaluationGuids[9],
                TextEvaluation = "Don't recommend",
                PercentEvaluation = 0,
                ProductId = _productGuids[7]
            });

            Evaluations.Add(new EvaluationEntity
            {
                Id = _evaluationGuids[10],
                TextEvaluation = "The best PC",
                PercentEvaluation = 100,
                ProductId = _productGuids[8]
            });
            Evaluations.Add(new EvaluationEntity
            {
                Id = _evaluationGuids[11],
                TextEvaluation = "Good",
                PercentEvaluation = 80,
                ProductId = _productGuids[8]
            });
            Evaluations.Add(new EvaluationEntity
            {
                Id = _evaluationGuids[12],
                TextEvaluation = "Recommend",
                PercentEvaluation = 80,
                ProductId = _productGuids[9]
            });
            Evaluations.Add(new EvaluationEntity
            {
                Id = _evaluationGuids[13],
                TextEvaluation = "Good",
                PercentEvaluation = 80,
                ProductId = _productGuids[9]
            });
            Evaluations.Add(new EvaluationEntity
            {
                Id = _evaluationGuids[14],
                TextEvaluation = "Quality for reasonable money, I am satisfied",
                PercentEvaluation = 80,
                ProductId = _productGuids[10]
            });
            Evaluations.Add(new EvaluationEntity
            {
                Id = _evaluationGuids[15],
                TextEvaluation = "Meets my requirements",
                PercentEvaluation = 80,
                ProductId = _productGuids[10]
            });
            Evaluations.Add(new EvaluationEntity
            {
                Id = _evaluationGuids[16],
                TextEvaluation = "Suitable product configuration to suit my needs. I have it for a few days, it is still early for the reliability evaluation, but so far I am satisfied.",
                PercentEvaluation = 90,
                ProductId = _productGuids[11]
            });
            Evaluations.Add(new EvaluationEntity
            {
                Id = _evaluationGuids[17],
                TextEvaluation = "Good",
                PercentEvaluation = 90,
                ProductId = _productGuids[11]
            });
            Evaluations.Add(new EvaluationEntity
            {
                Id = _evaluationGuids[18],
                TextEvaluation = "The best PC",
                PercentEvaluation = 100,
                ProductId = _productGuids[12]
            });
            Evaluations.Add(new EvaluationEntity
            {
                Id = _evaluationGuids[19],
                TextEvaluation = "Quality for reasonable money, I am satisfied",
                PercentEvaluation = 90,
                ProductId = _productGuids[12]
            });

            Evaluations.Add(new EvaluationEntity
            {
                Id = _evaluationGuids[20],
                TextEvaluation = "Good",
                PercentEvaluation = 70,
                ProductId = _productGuids[12]
            });
            Evaluations.Add(new EvaluationEntity
            {
                Id = _evaluationGuids[11],
                TextEvaluation = "Quality gaming laptop for a great price.",
                PercentEvaluation = 70,
                ProductId = _productGuids[8]
            });
            Evaluations.Add(new EvaluationEntity
            {
                Id = _evaluationGuids[12],
                TextEvaluation = "Quality gaming laptop for a great price.",
                PercentEvaluation = 80,
                ProductId = _productGuids[9]
            });
            Evaluations.Add(new EvaluationEntity
            {
                Id = _evaluationGuids[23],
                TextEvaluation = "Recommend",
                PercentEvaluation = 80,
                ProductId = _productGuids[18]
            });
            Evaluations.Add(new EvaluationEntity
            {
                Id = _evaluationGuids[24],
                TextEvaluation = "Good",
                PercentEvaluation = 60,
                ProductId = _productGuids[18]
            });
            Evaluations.Add(new EvaluationEntity
            {
                Id = _evaluationGuids[25],
                TextEvaluation = "Quality for reasonable money, I am satisfied",
                PercentEvaluation = 100,
                ProductId = _productGuids[19]
            });
            Evaluations.Add(new EvaluationEntity
            {
                Id = _evaluationGuids[26],
                TextEvaluation = "It's a good laptop",
                PercentEvaluation = 90,
                ProductId = _productGuids[20]
            });
            Evaluations.Add(new EvaluationEntity
            {
                Id = _evaluationGuids[27],
                TextEvaluation = "I am a pensioner, the product suits me due to my hobbies.",
                PercentEvaluation = 100,
                ProductId = _productGuids[20]
            });
            Evaluations.Add(new EvaluationEntity
            {
                Id = _evaluationGuids[28],
                TextEvaluation = "It's a good laptop",
                PercentEvaluation = 100,
                ProductId = _productGuids[20]
            });
            Evaluations.Add(new EvaluationEntity
            {
                Id = _evaluationGuids[29],
                TextEvaluation = "the product was ready for immediate use.",
                PercentEvaluation = 70,
                ProductId = _productGuids[20]
            });

            Evaluations.Add(new EvaluationEntity
            {
                Id = _evaluationGuids[30],
                TextEvaluation = "Good",
                PercentEvaluation = 70,
                ProductId = _productGuids[20]
            });
            Evaluations.Add(new EvaluationEntity
            {
                Id = _evaluationGuids[31],
                TextEvaluation = "I have been using the purchased computer for 14 days and I am satisfied.",
                PercentEvaluation = 80,
                ProductId = _productGuids[20]
            });
            Evaluations.Add(new EvaluationEntity
            {
                Id = _evaluationGuids[32],
                TextEvaluation = "The best PC",
                PercentEvaluation = 100,
                ProductId = _productGuids[22]
            });
            Evaluations.Add(new EvaluationEntity
            {
                Id = _evaluationGuids[33],
                TextEvaluation = "It's a good laptop",
                PercentEvaluation = 100,
                ProductId = _productGuids[23]
            });
            Evaluations.Add(new EvaluationEntity
            {
                Id = _evaluationGuids[34],
                TextEvaluation = "Overall satisfaction, but time will show quality ...",
                PercentEvaluation = 80,
                ProductId = _productGuids[23]
            });
            Evaluations.Add(new EvaluationEntity
            {
                Id = _evaluationGuids[35],
                TextEvaluation = "Suitable product configuration to suit my needs. I have it for a few days, it is still early for the reliability evaluation, but so far I am satisfied.",
                PercentEvaluation = 80,
                ProductId = _productGuids[24]
            });
            Evaluations.Add(new EvaluationEntity
            {
                Id = _evaluationGuids[36],
                TextEvaluation = "Good",
                PercentEvaluation = 60,
                ProductId = _productGuids[25]
            });
            Evaluations.Add(new EvaluationEntity
            {
                Id = _evaluationGuids[37],
                TextEvaluation = "Recommend",
                PercentEvaluation = 80,
                ProductId = _productGuids[25]
            });
            Evaluations.Add(new EvaluationEntity
            {
                Id = _evaluationGuids[38],
                TextEvaluation = "Quality for reasonable money, I am satisfied",
                PercentEvaluation = 80,
                ProductId = _productGuids[25]
            });
            Evaluations.Add(new EvaluationEntity
            {
                Id = _evaluationGuids[39],
                TextEvaluation = "very good",
                PercentEvaluation = 100,
                ProductId = _productGuids[27]
            });

            Evaluations.Add(new EvaluationEntity
            {
                Id = _evaluationGuids[40],
                TextEvaluation = "Suitable product configuration to suit my needs. I have it for a few days, it is still early for the reliability evaluation, but so far I am satisfied.",
                PercentEvaluation = 70,
                ProductId = _productGuids[27]
            });
            Evaluations.Add(new EvaluationEntity
            {
                Id = _evaluationGuids[41],
                TextEvaluation = "Good",
                PercentEvaluation = 80,
                ProductId = _productGuids[27]
            });
            Evaluations.Add(new EvaluationEntity
            {
                Id = _evaluationGuids[42],
                TextEvaluation = "Meets my requirements",
                PercentEvaluation = 100,
                ProductId = _productGuids[29]
            });
            Evaluations.Add(new EvaluationEntity
            {
                Id = _evaluationGuids[43],
                TextEvaluation = "The best PC",
                PercentEvaluation = 100,
                ProductId = _productGuids[29]
            });
            Evaluations.Add(new EvaluationEntity
            {
                Id = _evaluationGuids[44],
                TextEvaluation = "I have been using the purchased computer for 14 days and I am satisfied.",
                PercentEvaluation = 80,
                ProductId = _productGuids[30]
            });
            Evaluations.Add(new EvaluationEntity
            {
                Id = _evaluationGuids[45],
                TextEvaluation = "The best PC",
                PercentEvaluation = 100,
                ProductId = _productGuids[30]
            });
            Evaluations.Add(new EvaluationEntity
            {
                Id = _evaluationGuids[46],
                TextEvaluation = "Good",
                PercentEvaluation = 60,
                ProductId = _productGuids[30]
            });
            Evaluations.Add(new EvaluationEntity
            {
                Id = _evaluationGuids[47],
                TextEvaluation = "Overall satisfaction, but time will show quality ...",
                PercentEvaluation = 80,
                ProductId = _productGuids[31]
            });
            Evaluations.Add(new EvaluationEntity
            {
                Id = _evaluationGuids[48],
                TextEvaluation = "Absolute garbage",
                PercentEvaluation = 0,
                ProductId = _productGuids[32]
            });
            Evaluations.Add(new EvaluationEntity
            {
                Id = _evaluationGuids[49],
                TextEvaluation = "Recommend",
                PercentEvaluation = 80,
                ProductId = _productGuids[32]
            });
        }

        private void SeedManufacturers()
        {
            Manufacturers.Add(new ManufacturerEntity
            {
                Id = _manufacturerGuids[0],
                Name = "Dell",
                Description = "Dell is computer manufacturer that was first started by Michael Dell at the University of Texas-Austin on May 3, 1984. Today, it is one of the largest and fastest growing major computer companies in the world. Dell allows customers to order their products directly from the factory, and from retail electronics stores.",
                Logo = "default_manufacturer_logo.jpg",
                CountryOfOrigin = "USA",
            });
            Manufacturers.Add(new ManufacturerEntity
            {
                Id = _manufacturerGuids[1],
                Name = "Lenovo",
                Description = "Founded in 1984, Lenovo is one of the world's largest manufacturers of computers and portables.",
                Logo = "default_manufacturer_logo.jpg",
                CountryOfOrigin = "U.S.",
            });
            Manufacturers.Add(new ManufacturerEntity
            {
                Id = _manufacturerGuids[2],
                Name = "Apple",
                Description = "Founded by Steve Wozniak and Steve Jobs, Apple was incorporated on January 4, 1977. Today, Apple is a leading manufacturer of a line of personal computers, peripherals, and computer software under the Apple Macintosh (Mac) brand name. Apple's line of smartphones, the iPhone, are the third most popular smartphone brand in the world. Apple also develops wearable devices, like the Apple Watch.",
                Logo = "default_manufacturer_logo.jpg",
                CountryOfOrigin = "U.S.",
            });
            Manufacturers.Add(new ManufacturerEntity
            {
                Id = _manufacturerGuids[3],
                Name = "Acer",
                Description = "Established in 1977 as a subsidiary of the Acer Group, which is a family of four branks -- Acer, Gateway, Packard Bell, and eMachines. Today, the Acer group sales a wide range of computer-related products.",
                Logo = "default_manufacturer_logo.jpg",
                CountryOfOrigin = "America",
            });
            Manufacturers.Add(new ManufacturerEntity
            {
                Id = _manufacturerGuids[4],
                Name = "Asus",
                Description = "Founded April 1, 1989, ASUS is one of the largest manufacturers of computer CPU, motherboards, laptops, tablets, and other computer peripherals.",
                Logo = "default_manufacturer_logo.jpg",
                CountryOfOrigin = "Taiwan",
            });
            Manufacturers.Add(new ManufacturerEntity
            {
                Id = _manufacturerGuids[5],
                Name = "LG",
                Description = "Founded in 1958 as GoldStar, LG (Life's Good) Electronics is a company who manufacturers a wide range of products. Their computer-related products today include monitor, data storage, USB modems, and various computer accessories.",
                Logo = "default_manufacturer_logo.jpg",
                CountryOfOrigin = "U.S.",
            });
            Manufacturers.Add(new ManufacturerEntity
            {
                Id = _manufacturerGuids[6],
                Name = "MSI",
                Description = "Founded in 1986, MSI (Micro-Star International) is a manufacturer of computer notebooks, tablets, motherboards, video cards and other accessories.",
                Logo = "default_manufacturer_logo.jpg",
                CountryOfOrigin = "USA",
            });
            Manufacturers.Add(new ManufacturerEntity
            {
                Id = _manufacturerGuids[7],
                Name = "Hewlett-Packard",
                Description = "Founded on January 1, 1939 by William Hewlett and David Packard, Hewlett-Packard is one of the world's largest computer and peripheral manufacturers. They are also a foremost producer of test and measurement instruments.",
                Logo = "default_manufacturer_logo.jpg",
                CountryOfOrigin = "USA",
            });
            Manufacturers.Add(new ManufacturerEntity
            {
                Id = _manufacturerGuids[8],
                Name = "Microsoft",
                Description = "Founded on April 4, 1975, by Bill Gates and Paul Allen, Microsoft is one of the largest and most successful companies in the world. Microsoft is the developer and distributor of Microsoft Windows, Microsoft Office, DirectX, Xbox, and numerous other programs, games, and services.",
                Logo = "default_manufacturer_logo.jpg",
                CountryOfOrigin = "USA",
            });
        }

        private void SeedCategory()
        {
            Categories.Add(new CategoryEntity
            {
                Id = _categoryGuids[0],
                Name = "Professional"
            });
            Categories.Add(new CategoryEntity
            {
                Id = _categoryGuids[1],
                Name = "Home & office"
            });
            Categories.Add(new CategoryEntity
            {
                Id = _categoryGuids[2],
                Name = "Gaming"
            });
        }

        private void SeedProduct()
        {
            Product.Add(new ProductEntity
            {
                Id = _productGuids[0],
                Name = "Acer Nitro 5 AN517-51-76CG",
                Description = "Features: Crisp, matte full HD 120 Hz IPS display; system real time control with NitroSense. Up to 7h Battery Life\r\nDesign: Epic gaming look - slim display frame in a compact laptop case in black with red applications (hinge, keyboard lighting and touchpad edge)\r\nMultiple ports and interfaces: Bluetooth 5. 0, 1xHDMI, 1xUSB 3. 1 (Type-C Gen. 1), 2xUSB 3. 0 (1x Power-Off USB Charging), 1x USB 2. 0, 1xEthernet, 1xAudio Connection: Speaker/Headphones/Line-out (supports headsets with integrated microphone)\r\nAreas of use: Ideal for all types of gaming thanks to variable memory and graphics combinations - whether casual, pro or harcore. Also ideal for on the go thanks to its flat and lightweight design\r\nManufacturer: 2 years The conditions can be found under \"Further technical information\". Your statutory warranty rights remain unaffected.",
                Price = 755,
                Weight = 2700,
                CountInStock = 2,
                Photo = "default.jpg",
                ManufacturerId = _manufacturerGuids[3],
                CategoryId = _categoryGuids[2],


            });
            Product.Add(new ProductEntity
            {
                Id = _productGuids[1],
                Name = "Acer Nitro 5 AN515-54-74F2",
                Description = "Processor: Powerful gaming and working with the Intel Core i7-9750H\r\nIdeal for gaming – razor-sharp, matte full HD display, high-performance hardware, red QWERTZ keyboard with practical backlight\r\nDesign: aggressive yet sophisticated gaming look characterizes the design of the laptop, with a brushed pattern surface, black with red appliances\r\nVersatile ports and interfaces: Bluetooth, HDMI, USB 3.1, USB 3.0, USB 2.0, Ethernet, speaker/headphones/line-out\r\nWhether casual or hardcore gaming, the Acer Nitro 5 is ready for every battle. The Nitro 5 is also ideal for video editing or just multimedia applications",
                Price = 1266,
                Weight = 3400,
                CountInStock = 1,
                Photo = "default.jpg",
                ManufacturerId = _manufacturerGuids[3],
                CategoryId = _categoryGuids[2],
            });
            Product.Add(new ProductEntity
            {
                Id = _productGuids[2],
                Name = "Acer PH315-53-72XD",
                Description = "10th Generation Intel Core i7-10750H 6-Core Processor (Up to 5.0 GHz) with Windows 10 Home 64 Bit\r\nOverclockable NVIDIA GeForce RTX 2060 with 6 GB of dedicated GDDR6 VRAM\r\n15.6\" Full HD (1920 x 1080) Widescreen LED-backlit IPS display (144Hz Refresh Rate, 3ms Overdrive Response Time, 300nit Brightness & 72% NTSC)\r\n16 GB DDR4 2933MHz Dual-Channel Memory, 512GB NVMe SSD (2 x M.2 slots | 1 Slot open for easy upgrades) & 1 - Available Hard Drive Bay\r\n4-Zone RGB Backlit Keyboard | Wireless: Killer Double Shot Pro Wireless-AX 1650i 802. 11ax Wi-Fi 6 | LAN: Killer Ethernet E2600 10/100/1000 Gigabit Ethernet LAN | DTS X: Ultra Audio | 4th Gen All-Metal AeroBlade 3D Fan\r\nConnectivity technology: Bluetooth",
                Price = 1178,
                Weight = 2300,
                CountInStock = 7,
                Photo = "default.jpg",
                ManufacturerId = _manufacturerGuids[3],
                CategoryId = _categoryGuids[2],
            });
            Product.Add(new ProductEntity
            {
                Id = _productGuids[3],
                Name = "MSI GL65 Leopard 10SFK-062",
                Description = "15.6\" FHD IPS-Level 144Hz 72%NTSC Thin Bezel close to 100%Srgb NVIDIA GeForce RTX 2070 8G GDDR6\r\nIntel Core i7-10750H 2.6-5.0GHz Intel Wi-Fi 6 AX201(2*2 ax)\r\n512GB NVMe SSD 16GB (8G*2) DDR4 2666MHz 2 Sockets Max Memory 64GB\r\nUSB 3.1 Gen2 Type C *1 USB 3.2 Gen1 3 Steel Series per-Key RGB with Anti-Ghost key+ silver lining 720p HD Webcam\r\nWin10 Multi-language Giant Speakers 3W*2 6 cell (51Wh) Li-Ion 230W",
                Price = 1320,
                Weight = 4230,
                CountInStock = 5,
                Photo = "default.jpg",
                ManufacturerId = _manufacturerGuids[6],
                CategoryId = _categoryGuids[2],
            });
            Product.Add(new ProductEntity
            {
                Id = _productGuids[5],
                Name = "ASUS TUF Gaming Laptop FX505GT-AB73",
                Description = "NVIDIA GeForce GTX 1650 4GB GDDR5 (Base: 1395MHz, Boost: 1560MHz, TDP: 50W)\r\nIntel Core i7-9750H Processor (8M Cache, up to 4.5GHz)\r\n15.6” 144Hz FHD (1920x1080) IPS-Type display\r\n512GB PCIe NVMe M.2 SSD | 8GB DDR4 2666MHz RAM | Windows 10 Home\r\nDurable MIL-STD-810G military standard construction\r\nDual fans with anti-dust technology\r\nRGB backlit keyboard rated for 20-million keystroke durability\r\nGigabit Wave 2 Wi-Fi 5 (802.11ac) with Bluetooth 5.0",
                Price = 860,
                Weight = 2100,
                CountInStock = 14,
                Photo = "default.jpg",
                ManufacturerId = _manufacturerGuids[4],
                CategoryId = _categoryGuids[2],
            });
            Product.Add(new ProductEntity
            {
                Id = _productGuids[4],
                Name = "Lenovo Legion 5 Gaming Laptop 82B1000AUS",
                Description = "Welcome to the next generation of gaming performance with the AMD Ryzen 7 4800H mobile processor, 16GB 3200MHz DDR4 memory, and 512GB M.2 NVMe PCIe SSD storage\r\nEnjoy fast refresh and deep colors with a 144 Hz refresh rate and outstanding clarity on a 15.6\" FHD (1920 x 1080) IPS display\r\nThe NVIDIA GeForce GTX 1660Ti GPU is a blazing-fast supercharger for your favorite games and the newest titles\r\nGet maximum performance via Dual Burn Support, which pushes the CPU and GPU together for improved framerates, and Legion Coldfront 2.0 for thermal tuning\r\nThe Legion TrueStrike keyboard with soft-landing switches delivers hair-trigger inputs",
                Price = 1200,
                Weight = 2400,
                CountInStock = 10,
                Photo = "default.jpg",
                ManufacturerId = _manufacturerGuids[1],
                CategoryId = _categoryGuids[2],
            });
            Product.Add(new ProductEntity
            {
                Id = _productGuids[5],
                Name = "ASUS ROG Strix G15 	G512LW-ES76",
                Description = "NVIDIA GeForce RTX 2070 8GB GDDR6 with ROG Boost (Base: 1260MHz, Boost: 1455MHz, 115W)\r\nLatest 10th Gen Intel Core i7-10750H Processor\r\n240Hz 3ms 15.6” Full HD 1920x1080 IPS-Type Display\r\n16GB DDR4 2933MHz RAM | 1TB PCIe SSD | Windows 10 Home\r\nROG Intelligent Cooling thermal system with Thermal Grizzly Liquid Metal Thermal Compound\r\nROG Aura Sync System with RGB Keyboard, Logo, and Light Bar\r\nGig+ Wi-Fi 6 & Bluetooth 5.0 | ROG Easy Upgrade Design\r\nBundle: Get 30 days of Xbox Game Pass for PC with purchase*",
                Price = 1440,
                Weight = 2800,
                CountInStock = 7,
                Photo = "default.jpg",
                ManufacturerId = _manufacturerGuids[4],
                CategoryId = _categoryGuids[2],
            });
            Product.Add(new ProductEntity
            {
                Id = _productGuids[6],
                Name = "ASUS TUF TUF506IU-ES74 ",
                Description = "NVIDIA GeForce GTX 1660 Ti 6GB GDDR6 with ROG Boost\r\n8-core AMD Ryzen 7 4800H Processor 45W (8M Cache, up to 4.2GHz)\r\n15.6\" 144Hz Full HD IPS-Type Display\r\n16GB DDR4 3200MHz RAM | 512GB PCIe NVMe M.2 SSD | Windows 10 Home\r\n90WHr battery | Up to 8.7 hours web browsing and up to 12.3 hours video playback\r\nDurable MIL-STD-810H military standard construction\r\nSelf cleaning dual fans with anti-dust technology to extend system longevity",
                Price = 980,
                Weight = 2200,
                CountInStock = 5,
                Photo = "default.jpg",
                ManufacturerId = _manufacturerGuids[4],
                CategoryId = _categoryGuids[2],
            });
            Product.Add(new ProductEntity
            {
                Id = _productGuids[7],
                Name = "Dell Inspiron 17 3793 2020 Premium",
                Description = "Intel Core i5-1035G1 1. 0 GHz, Turbo to 3. 60 GHz, 6MB Cache, 4 Cores, 8 Threads, Intel UHD Graphics with shared graphics memory\r\nRAM is upgraded to 16GB memory for better multitasking. Hard Drive is upgrade to 512GB SSD + 1TB HDD. 512GB SSD(Solid State Drive) has faster data access speed, better performance and greater reliability, 1TB HDD for larger storage so you could store more data and files on it\r\n17. 3-inch FHD (1920 x 1080) Anti-Glare LED-Backlit Non-touch WVA Display. Built-in HD webcam with dual array microphone\r\nDVD, Wi-Fi, Bluetooth, Windows 10 Home\r\n1x USB 2. 0, 2x USB 3. 1 Gen 1, 1x USB 3. 1 Type-C, 1x RJ-45, 1x HDMI, 1x Headphone output/Microphone input combo",
                Price = 899,
                Weight = 3100,
                CountInStock = 7,
                Photo = "default.jpg",
                ManufacturerId = _manufacturerGuids[0],
                CategoryId = _categoryGuids[2],
            });
            Product.Add(new ProductEntity
            {
                Id = _productGuids[8],
                Name = "ASUS ZenBook Pro Duo UX581",
                Description = "ScreenPad Plus: 14 inches 4K matte touchscreen, giving you endless way to optimize your multitasking experience by extending the screen or split windows and apps on both displays\r\n15.6 inches 4K UHD NanoEdge touchscreen glossy main display\r\nLatest 9th generation Intel Core i7 9750H Quad Core Processor (12M Cache, up to 4.5 GHz) with NVIDIA GeForce RTX 2060\r\nDetachable palm rest and ASUS active stylus pen included\r\nFast storage and memory featuring 1TB PCIe NVMe SSD with 16GB DDR4 RAM\r\nBuilt-in IR camera for facial recognition sign in with Windows Hello\r\nExclusive ErgoLift design for improved typing position, optimized cooling system and enhanced audio performance\r\nExtensive connectivity with HDMI, USB Type C with Thunderbolt, Gig+ Wi-Fi 6 (802.11ax) (USB Transfer speed may vary. Learn more on ASUS website)",
                Price = 1999,
                Weight = 2800,
                CountInStock = 3,
                Photo = "default.jpg",
                ManufacturerId = _manufacturerGuids[4],
                CategoryId = _categoryGuids[2],
            });
            Product.Add(new ProductEntity
            {
                Id = _productGuids[9],
                Name = "Dell XPS9500-7002SLV-PUS",
                Description = "62% larger touchpad, 5% larger screen, and 5.6% smaller footprint\r\n16:10 FHD+ edge to edge display equipped with DisplayHDR 400 and Dolby Vision\r\nIntegrated Eyesafe display technology\r\nQuad speaker design with Waves Nx audio\r\nHigh-polished diamond-cut sidewalls",
                Price = 2100,
                Weight = 3200,
                CountInStock = 7,
                Photo = "default.jpg",
                ManufacturerId = _manufacturerGuids[0],
                CategoryId = _categoryGuids[2],
            });

            Product.Add(new ProductEntity
            {
                Id = _productGuids[10],
                Name = "Acer Aspire 5 Slim Laptop",
                Description = "AMD Ryzen 3 3200U Dual Core Processor (Up to 3.5GHz); 4GB DDR4 Memory; 128GB PCIe NVMe SSD\r\n15.6 inches full HD (1920 x 1080) widescreen LED backlit IPS display; AMD Radeon Vega 3 Mobile Graphics\r\n1 USB 3.1 Gen 1 port, 2 USB 2.0 ports & 1 HDMI port with HDCP support\r\n802.11ac Wi-Fi; Backlit Keyboard; Up to 7.5 hours battery life\r\nWindows 10 in S mode. Maximum power supply wattage: 65 Watts",
                Price = 450,
                Weight = 1920,
                CountInStock = 9,
                Photo = "default.jpg",
                ManufacturerId = _manufacturerGuids[3],
                CategoryId = _categoryGuids[0],
            });
            Product.Add(new ProductEntity
            {
                Id = _productGuids[11],
                Name = "Acer Chromebook Spin 311",
                Description = "Chromebook runs on Chrome OS - An operating system by Google that is built for the way we live today. It comes with built-in virus protection, updates automatically, boots up in seconds and continues to stay fast over time. (Internet connection is required).\r\nAll the Google apps you know and love come standard on every Chromebook, which means you can edit, download, and convert Microsoft Office files in Google Docs, Sheets and Slides.\r\nGet access to more than 2 million Android apps from Google Play to learn and do more.\r\nChromebooks come with built-in storage for offline access to your most important files and an additional 100GB of Google Drive space to ensure that all of your files are backed up automatically.\r\nAcer CP311-2H-C679 convertible Chromebook comes with 11.6” HD Touch IPS Display, Intel Celeron N4020, 4GB LPDDR4 Memory, 32GB eMMC, Google Chrome and up to 10-hours battery life.",
                Price = 334,
                Weight = 1900,
                CountInStock = 5,
                Photo = "default.jpg",
                ManufacturerId = _manufacturerGuids[3],
                CategoryId = _categoryGuids[0],
            });
            Product.Add(new ProductEntity
            {
                Id = _productGuids[12],
                Name = "ASUS F512DA-EB51 VivoBook",
                Description = "15.6 inch FHD 4 way NanoEdge bezel display with a stunning 88% screen-to-body ratio\r\nPowerful AMD Quad Core Ryzen 5 3500U Processor (2M Cache, up to 3.6 GHz)\r\nAMD Radeon Vega 8 discrete graphics with Windows 10 Home\r\n8GB DDR4 RAM and 256GB PCIe NVMe M.2 SSD\r\nErgonomic backlit keyboard with fingerprint sensor activated via Windows Hello\r\nExclusive ErgoLift design for improved typing position\r\nComprehensive connections including USB 3.2 Type-C, USB 3.2, USB 2.0, HDMI, and Wi-Fi 5 (802.11ac)",
                Price = 579,
                Weight = 1700,
                CountInStock = 11,
                Photo = "default.jpg",
                ManufacturerId = _manufacturerGuids[4],
                CategoryId = _categoryGuids[0],
            });
            Product.Add(new ProductEntity
            {
                Id = _productGuids[13],
                Name = "Lenovo Flex 5 14\" 2-in-1 Laptop",
                Description = "Thin, light, and stylish – This 2-in-1 laptop weighs just 3.64 pounds and is only 0.82\" thick. It's soft and comfortable to the touch, with a durable paint that creates a better user experience. Digital pen included\r\nThe 10-point, 14-inch FHD (1920 x 1080) IPS touchscreen allows the Lenovo Flex 5 14\" 2-in-1 laptop to be comfortable, fun, and easy to use. It's also great to look at, with 4-side narrow bezels\r\nThe 360⁰ hinge lets you use your 2-in-1 touchscreen laptop in whatever mode works best for you; Use it in 'Laptop' mode for everyday computing, 'Tent' mode for sharing things, 'Stand' mode for binge-watching, or 'Tablet' mode for more intuitive interaction\r\nEnjoy up to 10 hours of battery life, plus quick charge to 80% in just 1 hour\r\nPowered by the AMD Ryzen 5 4500U mobile processor with Radeon graphics, you have the performance to do more, from anywhere. With more cores, you'll experience responsiveness that leaps into action for productivity, gaming, and content creation",
                Price = 769,
                Weight = 1700,
                CountInStock = 4,
                Photo = "default.jpg",
                ManufacturerId = _manufacturerGuids[1],
                CategoryId = _categoryGuids[0],
            });
            Product.Add(new ProductEntity
            {
                Id = _productGuids[14],
                Name = "HP 15-dy1036nr ",
                Description = "Your career essential: With Wi-Fi 6 (2x) and Bluetooth(R) 5 connections, HP True Vision HD webcam and an integrated numeric keypad, this 15-inch laptop has all the essential features you need for a productive work day whether you're in the office or at home\r\nDesign and style with performance to match: Stay connected and productive with long-lasting battery life and a thin and portable, micro-edge bezel design\r\nBIOS recovery and protection: Automatically checks the health of your PC, protects against unauthorized access, secures local storage and recovers itself from boot up issues\r\nDisplay: 15.6-inch diagonal full HD, anti-glare, micro-edge, WLED-backlit display (1920 x 1080); 82% screen to body ratio\r\nFast processor: 10th Generation Intel(R) Core(TM) i5-1035G1, Quad-Core, 1.0 GHz base frequency, up to 3.6 GHz with Intel Turbo Boost Technology\r\nFast bootup with solid-state drive & higher bandwidth memory: Boot up in seconds, transfer files without waiting hours, and enjoy a speedier experience with the internal 256 GB PCIe(R) NVMe(TM) M.2 SSD, and higher bandwidth, speed and efficiency with 8 GB DDR4-2666 SDRAM (1 x 8 GB, not upgradable)\r\nBattery life: Up to 10 hours and 15 minutes (mixed usage), up to 6 hours and 30 minutes (video playback), up to 8 hours and 30 minutes (wireless streaming); 0 to 50% charge in 45 minutes with HP Fast Charge",
                Price = 622,
                Weight = 1800,
                CountInStock = 3,
                Photo = "default.jpg",
                ManufacturerId = _manufacturerGuids[7],
                CategoryId = _categoryGuids[0],
            });
            Product.Add(new ProductEntity
            {
                Id = _productGuids[15],
                Name = "Acer Aspire 5 A515-55-56VK",
                Description = "10th Generation Intel Core i5-1035G1 Processor (Up to 3.6GHz)| 8GB DDR4 Memory | 256GB NVMe SSD\r\n15.6\" Full HD (1920 x 1080) widescreen LED backlit IPS Display | Intel UHD Graphics\r\nIntel Wireless Wi-Fi 6 AX201 802.11ax | Backlit Keyboard | Fingerprint Reader | HD Webcam | Up to 8 Hours Battery Life\r\n1 - USB 3.1 (Type-C) Gen 1 port (up to 5 Gbps), 2 - USB 3.1 Gen 1 Port (one with Power-off Charging), 1 - USB 2.0 Port & 1 - HDMI Port with HDCP Support\r\nWindows 10 Home",
                Price = 549,
                Weight = 1650,
                CountInStock = 13,
                Photo = "default.jpg",
                ManufacturerId = _manufacturerGuids[3],
                CategoryId = _categoryGuids[0],
            });
            Product.Add(new ProductEntity
            {
                Id = _productGuids[16],
                Name = "ASUS UX534FTC-AS77 ZenBook ",
                Description = "15.6 inch wide-view 4K UHD 4-way NanoEdge bezel display with 92% screen to body ratio\r\nInnovative ScreenPad: 5.65-inch interactive touchscreen trackpad that adapts to your needs for smarter control and multitasking\r\nLatest 10th generation Intel Core i7-10510U Quad Core Processor (8M Cache, up to 4.9 GHz) with NVIDIA GeForce GTX 1650 Max-Q discrete graphics\r\nFast storage and memory featuring 512GB PCIe NVMe SSD and 16GB RAM\r\nWorks with Amazon Alexa Voice Service that helps you with tasks, entertainment, general information, and more.\r\nBuilt-in IR camera for facial recognition sign in with Windows Hello\r\nExtensive connectivity with HDMI, USB Type C, Wi-Fi 6 (802.11ax), Bluetooth 5.0 and SD card reader (USB Transfer speed may vary. Learn more at ASUS website)",
                Price = 1152,
                Weight = 1700,
                CountInStock = 8,
                Photo = "default.jpg",
                ManufacturerId = _manufacturerGuids[4],
                CategoryId = _categoryGuids[0],
            });
            Product.Add(new ProductEntity
            {
                Id = _productGuids[17],
                Name = "2020 Lenovo IdeaPad Laptop",
                Description = "▶ AMD A6-9220e accelerated processor; Dual-core processing. AMD A6 APU handles the AMD Radeon graphics alongside the central processor to balance the load, enabling great performance, rapid multitasking and immersive entertainment.\r\n▶ Beautiful 14\" display Typical 1366 x 768 HD resolution. Energy-efficient LED backlight.\r\n▶ 4GB DDR4 Memory for full-power multitasking; 64GB eMMC flash memory: This ultracompact memory system is ideal for mobile devices and applications, providing enhanced storage capabilities, streamlined data management, quick boot-up times and support for high-definition video playback.\r\n▶ 802. 11ac wireless LAN; 2 x USB 3. 0, 1 HDMI 1. 4; 1 headphone/microphone combo, AMD Radeon R4 integrated graphics\r\n▶ Windows 10 Home 64-bit English brings back the Start Menu from Windows 7 and introduces new features, like the Edge Web browser that lets you markup Web pages on your screen. Microsoft Office 365 Personal 1-year subscription for free",
                Price = 259,
                Weight = 1800,
                CountInStock = 3,
                Photo = "default.jpg",
                ManufacturerId = _manufacturerGuids[1],
                CategoryId = _categoryGuids[0],
            });
            Product.Add(new ProductEntity
            {
                Id = _productGuids[18],
                Name = "Acer Chromebook 314",
                Description = "Chromebook runs on Chrome OS - an operating system by Google that is built for the way we live today. It comes with built-in virus protection, updates automatically*, boots up in seconds and continues to stay fast over time. (*Internet connection is required).\r\nAll the Google apps you know and love come standard on every Chromebook, which means you can edit, download, and convert Microsoft Office files in Google Docs, Sheets and Slides.\r\nGet access to more than 2 million Android apps from Google Play to learn and do more.\r\nChromebooks come with built-in storage for offline access to your most important files and an additional 100GB of Google Drive space to ensure that all of your files are backed up automatically.\r\nCB314-1H-C884 comes with 14” Full HD IPS Display, Intel Celeron N4000, 4GB LPDDR4 Memory, 64GB eMMC, Google Chrome and Up to 12. 5-hours Battery Life.",
                Price = 287,
                Weight = 2000,
                CountInStock = 7,
                Photo = "default.jpg",
                ManufacturerId = _manufacturerGuids[3],
                CategoryId = _categoryGuids[0],
            });
            Product.Add(new ProductEntity
            {
                Id = _productGuids[19],
                Name = "ASUS Laptop L210 Ultra Thin",
                Description = "Efficient Intel Celeron N4020 Processor (4M Cache, up to 2.8 GHz)\r\n11.6” HD (1366 x 768) Slim Display\r\n64GB eMMC Flash Storage and 4GB DDR4 RAM\r\nWindows 10 in S Mode with One Year of Microsoft 365 Personal\r\nSlim and Portable: 0.7” thin and weighs only 2.2 lbs (battery included)",
                Price = 229,
                Weight = 1200,
                CountInStock = 4,
                Photo = "default.jpg",
                ManufacturerId = _manufacturerGuids[4],
                CategoryId = _categoryGuids[0],
            });

            Product.Add(new ProductEntity
            {
                Id = _productGuids[20],
                Name = "HP Stream 11.6-inch HD Laptop",
                Description = "STUDY, STREAM, SHARE: Between home, school and work, you need a PC that won't quit. Post, play and stay productive all day with the affordable and portable HP Stream 11\r\nKEEP YOUR PROJECTS SAFE: Experience peace of mind that comes with the most secure Windows ever built with Office, Microsoft Edge, Bing Search, Windows Defender and more\r\nOFFICE 365 FOR ONE YEAR: Get full access to Microsoft Excel, Word, PowerPoint, OneNote, Access, and 1 TB of OneDrive Storage for one year\r\nPROCESSOR: Intel(R) Celeron(R) N4000 Processor, Dual-Core, 1.1 GHz base frequency, up to 2.6 GHz burst frequency\r\nDISPLAY: 11.6-inch diagonal HD AntiGlare WLED-backlit display (1366 x 768); 73% screen to body ratio\r\nMEMORY: 4 GB DDR3L-1600 SDRAM (not upgradable)\r\nSTORAGE: 32 GB eMMC",
                Price = 259,
                Weight = 1300,
                CountInStock = 10,
                Photo = "default.jpg",
                ManufacturerId = _manufacturerGuids[7],
                CategoryId = _categoryGuids[1],
            });
            Product.Add(new ProductEntity
            {
                Id = _productGuids[21],
                Name = "ASUS Chromebook Flip C434 2-In-1",
                Description = "14\" Touchscreen Full HD 1920x1080 4-way NanoEdge display featuring ultra-narrow bezels (5mm thin) around each side of the display that allows for a 14\" screen to fit in the body of a 13\" laptop footprint\r\nThe Full HD display has a durable 360 degree hinge that can be used to flip the touchscreen display to tent, stand, and tablet mode\r\nPowered by the Intel Core m3-8100Y Processor (up to 3. 4 GHz) for super-fast and snappy performance. If you use a ton of tabs or run lots of apps, this has the power to get it all done with ease. Bluetooth 4.0\r\n8GB LPDDR3 RAM; 64GB eMMC storage and 2x USB Type-C (Gen 1) and 1x Type-A (Gen 1) ports plus a backlit keyboard (USB Transfer speed may vary. Learn more at ASUS website)\r\nThe lightweight (3 lbs) all-aluminum metal body makes the C434 both durable and beautiful for a timeless look that will never go out of style\r\nChromebook runs on Chrome OS - an operating system by Google that is built for the way we live today. It comes with built-in virus protection, updates automatically, boots up in seconds and continues to stay fast over time\r\nAll the Google apps you know and love come standard on every Chromebook, which means you can edit, download, and convert Microsoft Office files in Google Docs, Sheets and Slides",
                Price = 599,
                Weight = 1500,
                CountInStock = 7,
                Photo = "default.jpg",
                ManufacturerId = _manufacturerGuids[4],
                CategoryId = _categoryGuids[1],
            });
            Product.Add(new ProductEntity
            {
                Id = _productGuids[22],
                Name = "New HP 15.6\" HD Touchscreen Laptop",
                Description = "15.6 in HD WLED touchscreen (1366 x 768), 10-finger multi-touch support.\r\n10th Generation Intel Core i3-1005G1 1.2GHz up to 3.4GHz.\r\n8GB DDR4 SDRAM 2666MHz, 128GB SSD, No Optical Drive.\r\nIntel UHD Graphics, HD Audio with stereo speakers. HP TrueVision HD camera.\r\nRealtek RTL8821CE 802.11b/g/n/ac, Bluetooth 4.2, 1 HDMI 1.4, 1 USB 3.1 Gen 1 Type-C, 2 USB 3.1 Gen 1 Type-A.",
                Price = 519,
                Weight = 1700,
                CountInStock = 0,
                Photo = "default.jpg",
                ManufacturerId = _manufacturerGuids[7],
                CategoryId = _categoryGuids[1],
            });
            Product.Add(new ProductEntity
            {
                Id = _productGuids[23],
                Name = "ROG Zephyrus G15 (2020) Ultra Slim",
                Description = "NVIDIA GeForce GTX 1660 Ti 6GB GDDR6 with ROG Boost (Base 1140MHz Boost 1335MHz TDP 60W)\r\nAMD Ryzen 7 4800HS processor (up to 4.2GHz)\r\n15.6” 144Hz IPS-Type Full HD (1920x1080) display\r\n16GB 3200MHz DDR4 RAM | 1TB PCIe NVMe M 2 SSD | Backlit Precision Gaming Keyboard | Windows 10 Home\r\n0.8” thin, 4.85 pound ultra-portable form-factor\r\nGig+ Wi-Fi 6 (802.11ax) | USB Type-C charging capable\r\nROG Intelligent Cooling system with self-cleaning anti-dust tunnels preserves cooling performance and system stability",
                Price = 1049,
                Weight = 2000,
                CountInStock = 3,
                Photo = "default.jpg",
                ManufacturerId = _manufacturerGuids[4],
                CategoryId = _categoryGuids[1],
            });
            Product.Add(new ProductEntity
            {
                Id = _productGuids[24],
                Name = "ASUS Vivobook S15 S532",
                Description = "ScreenPad 2.0 adds an interactive, secondary 5.65” touchscreen to enhance productivity\r\nScreenPad 2.0 fits a series of handy ASUS utility apps: Quick Key, Number Key, Handwriting, Slide Xpert, etc.\r\n15.6 inch Full HD 4 way NanoEdge bezel display with stunning 88% screen-to-body ratio and 5.65” Full HD ScreenPad 2.0\r\n10th Gen Intel Core i5-10210U Processor (6M Cache, up to 4.2 GHz)\r\n8GB DDR4 RAM and 512GB PCIe NVMe SSD; Windows 10 Home\r\nErgonomic backlit chiclet keyboard and facial login via IR camera + Windows Hello\r\nExclusive Ergolift design for improved typing position\r\nComprehensive connections, including USB 3.1 Type-C, USB 3.1 Type A, USB 2.0, HDMI, and Gig+ Wi-Fi 6 (802.11ax) (USB Transfer speed may vary. Learn more on ASUS website)",
                Price = 799,
                Weight = 1700,
                CountInStock = 1,
                Photo = "default.jpg",
                ManufacturerId = _manufacturerGuids[4],
                CategoryId = _categoryGuids[1],
            });
            Product.Add(new ProductEntity
            {
                Id = _productGuids[25],
                Name = "HP Stream 14-inch Laptop",
                Description = "Laptop for work, school and play: With Office 365 and 1 TB of cloud storage, this device combines functionality, connectivity, style, and value\r\nEssential productivity: This nimble laptop delivers the essential productivity and entertainment you want for school or home, without slowing you down\r\nOffice 365 for one year: Get full access to Microsoft Excel, Word, PowerPoint, OneNote, Access, and 1 TB of OneDrive Storage for one year\r\nProcessor: Intel(R) Celeron(R) N4000 Processor, Dual-Core, 1.1 GHz base frequency, up to 2.6 GHz burst frequency\r\nDisplay: 14-inch diagonal HD SVA BrightView WLED-backlit display (1366 x 768)\r\nMemory: 4 GB DDR4-2400 SDRAM (not upgradable)\r\nStorage: 64 GB eMMC",
                Price = 295,
                Weight = 1500,
                CountInStock = 0,
                Photo = "default.jpg",
                ManufacturerId = _manufacturerGuids[7],
                CategoryId = _categoryGuids[1],
            });
            Product.Add(new ProductEntity
            {
                Id = _productGuids[26],
                Name = "2019 HP Stream Laptop",
                Description = "14\" diagonal HD SVA BrightView micro-edge WLED-backlit (1366 x 768), Intel Celeron N4000 (1.1 GHz base frequency, up to 2.6 GHz burst frequency, 4 MB cache, 2 cores)\r\nIntel Integrated UHD Graphics 600, 32 GB eMMC Hard Drive\r\n4 GB DDR4-2400 SDRAM, 802.11 ac 2X2 Wi-Fi and Bluetooth\r\n2 USB 3.1 Gen 1; 1 USB 2.0; 1 HDMI 1.4; 1 headphone/microphone combo, Micro SD media card reader, DTS Studio Sound with dual speakers\r\nFull-size island-style keyboard, Front-facing Webcam with integrated digital microphone, Windows 10 in S Mode, Only 3.39 Lbs",
                Price = 265,
                Weight = 1600,
                CountInStock = 2,
                Photo = "default.jpg",
                ManufacturerId = _manufacturerGuids[7],
                CategoryId = _categoryGuids[1],
            });
            Product.Add(new ProductEntity
            {
                Id = _productGuids[27],
                Name = "ASUS ZenBook 13 Ultra-Slim",
                Description = "13.3 inch Full HD (1920 x 1080) Wide View 4-way NanoEdge bezel display\r\nLatest 11th generation Latest 11th generation Intel Core i7-1165G7 Quad Core Processor (12M Cache, up to 4.70 GHz, with IPU)\r\nWindows 10 Home\r\nFast storage and memory featuring 1TB PCIe NVMe M.2 SSD with 16GB LPDDR4X RAM\r\nBuilt-in IR camera for facial recognition sign in with Windows Hello\r\nErgoLift hinge and backlit keyboard and NumberPad\r\nExtensive connectivity with HDMI 2.0b, Thunderbolt 4 via USB Type C, Wi-Fi 6 (802.11ax), Bluetooth 5.0, USB 3.2 Type-A, and Micro SD card reader (USB Transfer speed may vary. Learn more on ASUS website)",
                Price = 973,
                Weight = 1780,
                CountInStock = 7,
                Photo = "default.jpg",
                ManufacturerId = _manufacturerGuids[4],
                CategoryId = _categoryGuids[1],
            });
            Product.Add(new ProductEntity
            {
                Id = _productGuids[28],
                Name = "Lenovo IdeaPad 3 ",
                Description = "AMD Ryzen 5 3500U Mobile Processors with Radeon Graphics deliver powerful performance for everyday tasks\r\nDopoundsy Audio delivers crystal-clear sound, while the 14-inch FHD screen and narrow side bezels give you more viewing area and less clutter\r\nQuick and quiet with Q-control – Effortlessly swap between fast & powerful performance and quiet battery saving mode\r\nConnect with ease using Bluetooth 4.1, up to 2x2 Wi-Fi 5, three USB ports, and HDMI\r\nKeep your privacy intact with a physical shutter for your webcam. You'll enjoy privacy right at your fingertips\r\nSystem RAM Type: DDR4 SDRAM",
                Price = 421,
                Weight = 1900,
                CountInStock = 6,
                Photo = "default.jpg",
                ManufacturerId = _manufacturerGuids[1],
                CategoryId = _categoryGuids[1],
            });
            Product.Add(new ProductEntity
            {
                Id = _productGuids[29],
                Name = "Microsoft Surface Pro 7",
                Description = "Next-gen, best-in-class laptop with the versatility of a studio and tablet, so you can type, touch, draw, write, work, and play more naturally\r\nFaster than Surface Pro 6, with a 10th Gen Intel Core Processor – redefining what’s possible in a thin and light computer. Wireless : Wi-Fi 6: 802.11ax compatible. Bluetooth Wireless 5.0 technology\r\nMore ways to connect, with both USB-C and USB-A ports for connecting to displays, docking stations and more, as well as accessory charging\r\nStandout design that won’t weigh you down — ultra-slim and light Surface Pro 7 starts at just 1.70 pounds.Aspect ratio: 3:2\r\nAll-day battery life upto 10.5 hours, plus the ability to go from empty to full faster — about 80% in just over an hour",
                Price = 700,
                Weight = 900,
                CountInStock = 5,
                Photo = "default.jpg",
                ManufacturerId = _manufacturerGuids[8],
                CategoryId = _categoryGuids[1],
            });

            Product.Add(new ProductEntity
            {
                Id = _productGuids[30],
                Name = "MacBook Air",
                Description = "13.3” Retina display\r\nApple M1 chip\r\nUp to 16GB memory\r\nUp to 2TB storage2\r\nUp to 18 hours battery life",
                Price = 999,
                Weight = 1290,
                CountInStock = 5,
                Photo = "default.jpg",
                ManufacturerId = _manufacturerGuids[2],
                CategoryId = _categoryGuids[0],
            });
            Product.Add(new ProductEntity
            {
                Id = _productGuids[31],
                Name = "MacBook Pro 13”",
                Description = "13.3” Retina display\r\nApple M1 chip\r\nAlso available with Intel Core i5 or\r\ni7 processor\r\nUp to 16GB memory\r\nUp to 2TB storage4\r\nUp to 20 hours battery life",
                Price = 1299,
                Weight = 1400,
                CountInStock = 5,
                Photo = "default.jpg",
                ManufacturerId = _manufacturerGuids[2],
                CategoryId = _categoryGuids[0],
            });
            Product.Add(new ProductEntity
            {
                Id = _productGuids[32],
                Name = "MacBook Pro 16”",
                Description = "16” Retina display\r\nIntel Core i7 or i9 processor\r\nUp to 64GB memory\r\nUp to 8TB storage\r\nUp to 11 hours battery life",
                Price = 2399,
                Weight = 2000,
                CountInStock = 5,
                Photo = "default.jpg",
                ManufacturerId = _manufacturerGuids[2],
                CategoryId = _categoryGuids[0],
            });
        }
    }
}