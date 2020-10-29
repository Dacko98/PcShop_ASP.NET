using AutoMapper;
using PcShop.DAL.Entities;
using PcShop.DAL.Repositories;
using System;
using System.Collections.Generic;
using PcShop.BL.Api.Facades.Interfaces;
using PcShop.BL.Api.Models.Evaluation;
using PcShop.DAL.Entities;

namespace PcShop.BL.Api.Facades
{
    public class EvaluationFacade : IAppFacade
    {
        private readonly EvaluationRepository evaluationRepository;
        private readonly IMapper mapper;

        public EvaluationFacade(
            EvaluationRepository evaluationRepository,
            IMapper mapper)
        {
            this.evaluationRepository = evaluationRepository;
            this.mapper = mapper;
        }

        public List<EvaluationListModel> GetAll()
        {
            return mapper.Map<List<EvaluationListModel>>(evaluationRepository.GetAll());
        }

        public EvaluationDetailModel GetById(Guid id)
        {
            return mapper.Map<EvaluationDetailModel>(evaluationRepository.GetById(id));
        }

        public Guid Create(EvaluationNewModel evaluation)
        {
            var evaluationEntity = mapper.Map<EvaluationEntity>(evaluation);
            return evaluationRepository.Insert(evaluationEntity);
        }

        public Guid? Update(EvaluationDetailModel evaluation)
        {
            var evaluationEntity = mapper.Map<EvaluationEntity>(evaluation);
            return evaluationRepository.Update(evaluationEntity);
        }

        public void Delete(Guid id)
        {
            evaluationRepository.Remove(id);
        }
    }
}