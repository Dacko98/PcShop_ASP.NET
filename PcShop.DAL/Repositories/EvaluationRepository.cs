using AutoMapper;
using PcShop.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PcShop.DAL.Repositories
{
    public class EvaluationRepository : IAppRepository<EvaluationEntity>
    {
        private readonly IList<EvaluationEntity> _evaluations;
        private readonly IMapper _mapper;

        public EvaluationRepository(
            Storage storage,
            IMapper mapper)
        {
            _evaluations = storage.Evaluations;
            this._mapper = mapper;
        }

        public IList<EvaluationEntity> GetAll()
        {
            return _evaluations;
        }
        public IList<EvaluationEntity> GetByProductId(Guid productId)
        {
            return _evaluations.Where(evaluation => evaluation.ProductId== productId).ToList();
        }

        public EvaluationEntity GetById(Guid id)
        {
            return _evaluations.SingleOrDefault(entity => entity.Id == id);
        }

        public Guid Insert(EvaluationEntity evaluation)
        {
            evaluation.Id = Guid.NewGuid();
            _evaluations.Add(evaluation);
            return evaluation.Id;
        }

        public Guid? Update(EvaluationEntity evaluationUpdated)
        {
            var evaluationExisting = _evaluations.SingleOrDefault(evaluationInStorage => evaluationInStorage.Id == evaluationUpdated.Id);
            if (evaluationExisting != null)
            {
                _mapper.Map(evaluationUpdated, evaluationExisting);
            }

            return evaluationExisting?.Id;
        }

        public void Remove(Guid id)
        {
            var evaluationToRemove = _evaluations.Single(evaluation => evaluation.Id.Equals(id));
            _evaluations.Remove(evaluationToRemove);
        }
    }
}