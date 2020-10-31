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
        private readonly ProductRepository productRepository;

        public EvaluationFacade(
            EvaluationRepository evaluationRepository,
            ProductRepository productRepository,
            IMapper mapper)
        {
            this.evaluationRepository = evaluationRepository;
            this.mapper = mapper;
            this.productRepository = productRepository;
        }

        public List<EvaluationListModel> GetAll()
        {
            var evaluationEntityList = evaluationRepository.GetAll();
            foreach (var evaluationEntity in evaluationEntityList)
            {
                evaluationEntity.Product = productRepository.GetById(evaluationEntity.ProductId);
            }
            return mapper.Map<List<EvaluationListModel>>(evaluationEntityList);
        }

        public EvaluationDetailModel GetById(Guid id)
        {
            var evaluationEntity = evaluationRepository.GetById(id);
            evaluationEntity.Product = productRepository.GetById(evaluationEntity.ProductId);
            return mapper.Map<EvaluationDetailModel>(evaluationEntity);
        }

        public Guid Create(EvaluationNewModel evaluation)
        {
            var evaluationEntity = mapper.Map<EvaluationEntity>(evaluation);
            return evaluationRepository.Insert(evaluationEntity);
        }

        public Guid? Update(EvaluationUpdateModel evaluation)
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