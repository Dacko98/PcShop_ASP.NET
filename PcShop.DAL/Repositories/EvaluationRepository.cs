using AutoMapper;
using PcShop.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using PcShop.DAL.Entities;

namespace PcShop.DAL.Repositories
{
    public class EvaluationRepository : IAppRepository<EvaluationEntity>
    {
        private readonly IList<EvaluationEntity> evaluations;
        private readonly IMapper mapper;

        public EvaluationRepository(
            Storage storage,
            IMapper mapper)
        {
            evaluations = storage.Evaluations;
            this.mapper = mapper;
        }

        public IList<EvaluationEntity> GetAll()
        {
            return evaluations;
        }
        public IList<EvaluationEntity> GetByGoodsId(Guid goodsId)
        {
            return evaluations.Where(evaluation => evaluation.GoodsId== goodsId).ToList();
        }

        public EvaluationEntity GetById(Guid id)
        {
            return evaluations.SingleOrDefault(entity => entity.Id == id);
        }

        public Guid Insert(EvaluationEntity evaluation)
        {
            evaluation.Id = Guid.NewGuid();
            evaluations.Add(evaluation);
            return evaluation.Id;
        }

        public Guid? Update(EvaluationEntity evaluationUpdated)
        {
            var evaluationExisting = evaluations.SingleOrDefault(evaluationInStorage => evaluationInStorage.Id == evaluationUpdated.Id);
            if (evaluationExisting != null)
            {
                mapper.Map(evaluationUpdated, evaluationExisting);
            }

            return evaluationExisting?.Id;
        }

        public void Remove(Guid id)
        {
            var evaluationToRemove = evaluations.Single(evaluation => evaluation.Id.Equals(id));
            evaluations.Remove(evaluationToRemove);
        }
    }
}