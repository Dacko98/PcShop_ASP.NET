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
            new Guid("23b3902d-7d4f-4213-9cf0-112348f56238")
        };

        private readonly IList<Guid> ManufacturerGuids = new List<Guid>
        {
            new Guid("0d4fa150-ad80-4d46-a511-4c666166ec5e"),
        };


        public IList<EvaluationEntity> Evaluations { get; } = new List<EvaluationEntity>();
        public IList<ManufacturerEntity> Manufacturers { get; } = new List<ManufacturerEntity>();

        public Storage()
        {
            SeedEvaluations();
            SeedManufacturers();
        }

        private void SeedEvaluations()
        {
            Evaluations.Add(new EvaluationEntity
            {
                Id = EvaluationGuids[0],
                TextEvaluation = "Good",
                PercentEvaluation= 50
            });
            Evaluations.Add(new EvaluationEntity
            {
                Id = EvaluationGuids[1],
                TextEvaluation = "Not bad",
                PercentEvaluation = 100
            });
        }

        private void SeedManufacturers()
        {
            Manufacturers.Add(new ManufacturerEntity
            {
                Id = ManufacturerGuids[0],
                Name = "DELL",
                Description = "Best one",
                Logo = "Path",
                CountryOfOrigin = "USA",
            });
        }
    }
}