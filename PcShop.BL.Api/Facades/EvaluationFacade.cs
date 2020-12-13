using AutoMapper;
using PcShop.DAL.Entities;
using PcShop.DAL.Repositories;
using System;
using System.Collections.Generic;
using PcShop.BL.Api.Facades.Interfaces;
using PcShop.BL.Api.Models.Evaluation;

namespace PcShop.BL.Api.Facades
{
    public class EvaluationFacade : IAppFacade
    {
        private readonly EvaluationRepository _evaluationRepository;
        private readonly IMapper _mapper;
        private readonly ProductRepository _productRepository;

        public EvaluationFacade(
            EvaluationRepository evaluationRepository,
            ProductRepository productRepository,
            IMapper mapper)
        {
            this._evaluationRepository = evaluationRepository;
            this._mapper = mapper;
            this._productRepository = productRepository;
        }

        public List<EvaluationListModel> GetAll()
        {
            var evaluationEntityList = _evaluationRepository.GetAll();
            foreach (var evaluationEntity in evaluationEntityList)
            {
                evaluationEntity.Product = _productRepository.GetById(evaluationEntity.ProductId);
            }
            return _mapper.Map<List<EvaluationListModel>>(evaluationEntityList);
        }

        public EvaluationDetailModel GetById(Guid id)
        {
            var evaluationEntity = _evaluationRepository.GetById(id);
            if (evaluationEntity == null)
            {
                return null;
            }
            evaluationEntity.Product = _productRepository.GetById(evaluationEntity.ProductId);
            return _mapper.Map<EvaluationDetailModel>(evaluationEntity);
        }

        public Guid Create(EvaluationNewModel evaluation)
        {
            var evaluationEntity = _mapper.Map<EvaluationEntity>(evaluation);
            return _evaluationRepository.Insert(evaluationEntity);
        }

        public Guid? Update(EvaluationUpdateModel evaluation)
        {
            var evaluationEntity = _mapper.Map<EvaluationEntity>(evaluation);
            if (evaluationEntity == null)
            {
                return null;
            }
            return _evaluationRepository.Update(evaluationEntity);
        }

        public void Delete(Guid id)
        {
            _evaluationRepository.Remove(id);
        }
    }
}