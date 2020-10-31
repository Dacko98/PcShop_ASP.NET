using AutoMapper;
using PcShop.DAL.Entities;
using PcShop.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using PcShop.BL.Api.Facades.Interfaces;
using PcShop.BL.Api.Models.Evaluation;
using PcShop.BL.Api.Models.Goods;
using PcShop.DAL.Entities;

namespace PcShop.BL.Api.Facades
{
    public class GoodsFacade : IAppFacade
    {
        private readonly GoodsRepository goodsRepository;
        private readonly EvaluationRepository evaluationRepository;
        private readonly ManufacturerRepository manufacturerRepository;
        private readonly CategoryRepository categoryRepository;
        private readonly EvaluationFacade evaluationFacade;
        private readonly IMapper mapper;

        public GoodsFacade(
            GoodsRepository goodsRepository,
            EvaluationRepository evaluationRepository,
            ManufacturerRepository manufacturerRepository,
            CategoryRepository categoryRepository,
            EvaluationFacade evaluationFacade,
            IMapper mapper)
        {
            this.evaluationRepository = evaluationRepository;
            this.goodsRepository = goodsRepository;
            this.categoryRepository = categoryRepository;
            this.manufacturerRepository = manufacturerRepository;
            this.mapper = mapper;
            this.evaluationFacade = evaluationFacade;
        }

        public List<GoodsListModel> GetAll()
        {
            var goodsEntityList = goodsRepository.GetAll();
            foreach (var goodsEntity in goodsEntityList)
            {
                goodsEntity.Manufacturer = manufacturerRepository.GetById(goodsEntity.ManufacturerId);
                goodsEntity.Category = categoryRepository.GetById(goodsEntity.CategoryId);
            }
            return mapper.Map<List<GoodsListModel>>(goodsEntityList);
        }

        public GoodsDetailModel GetById(Guid id)
        {
            var goodsEntity = goodsRepository.GetById(id);
            goodsEntity.Evaluations = evaluationRepository.GetByGoodsId(id);
            goodsEntity.Manufacturer = manufacturerRepository.GetById(goodsEntity.ManufacturerId);
            goodsEntity.Category = categoryRepository.GetById(goodsEntity.CategoryId);
            return mapper.Map<GoodsDetailModel>(goodsEntity);
        }

        public Guid Create(GoodsNewModel goods)
        {
            var goodsEntity = mapper.Map<GoodsEntity>(goods);
            return goodsRepository.Insert(goodsEntity);
        }

        public Guid? Update(GoodsUpdateModel goodsUpdateModel)
        {
            var goodsEntityExisting = goodsRepository.GetById(goodsUpdateModel.Id);
            goodsEntityExisting.Evaluations = evaluationRepository.GetByGoodsId(goodsUpdateModel.Id);
            UpdateEvaluation(goodsUpdateModel, goodsEntityExisting);

            var goodsEntityUpdated = mapper.Map<GoodsEntity>(goodsUpdateModel);
            return goodsRepository.Update(goodsEntityUpdated);
        }

        private void UpdateEvaluation(GoodsUpdateModel goodsUpdateModel, GoodsEntity goodsEntity)
        {
            var evaluationToDelete = goodsEntity.Evaluations.Where(evaluation =>  
                !goodsUpdateModel.Evaluations.Any(evaluations => evaluations.Id == evaluation.Id));
            
            foreach (var evaluation in evaluationToDelete)
            {
                evaluationFacade.Delete(evaluation.Id);
            }
            
            var evaluationToInsert = goodsUpdateModel.Evaluations.Where(
                evaluation => !goodsEntity.Evaluations.Any(evaluations => evaluations.Id == evaluation.Id));

            foreach (var evaluation in evaluationToInsert)
            {
                evaluation.GoodsId = goodsUpdateModel.Id;
                evaluationFacade.Create(mapper.Map<EvaluationNewModel>(evaluation));
            }

            var evaluationToUpdate = goodsUpdateModel.Evaluations.Where(
                evaluation => goodsEntity.Evaluations.Any(evaluations => evaluations.Id == evaluation.Id));

            foreach (var evaluation in evaluationToUpdate)
            {
                evaluation.GoodsId = goodsUpdateModel.Id;
                evaluationFacade.Update(evaluation);
            }
        }

        public void Delete(Guid id)
        {
            var goodsEntity = goodsRepository.GetById(id);
            goodsEntity.Evaluations = evaluationRepository.GetByGoodsId(id);
            foreach (var evaluation in goodsEntity.Evaluations)
            {
                evaluationRepository.Remove(evaluation.Id);
            }

            goodsRepository.Remove(id);
        }
    }
}