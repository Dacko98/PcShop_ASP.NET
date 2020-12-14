using AutoMapper;
using PcShop.DAL.Entities;
using PcShop.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using PcShop.BL.Api.Facades.Interfaces;
using PcShop.BL.Api.Models.Evaluation;
using PcShop.BL.Api.Models.Product;

namespace PcShop.BL.Api.Facades
{
    public class ProductFacade : IAppFacade
    {
        private readonly ProductRepository _productRepository;
        private readonly EvaluationRepository _evaluationRepository;
        private readonly ManufacturerRepository _manufacturerRepository;
        private readonly CategoryRepository _categoryRepository;
        private readonly EvaluationFacade _evaluationFacade;
        private readonly IMapper _mapper;

        public ProductFacade(
            ProductRepository productRepository,
            EvaluationRepository evaluationRepository,
            ManufacturerRepository manufacturerRepository,
            CategoryRepository categoryRepository,
            EvaluationFacade evaluationFacade,
            IMapper mapper)
        {
            this._evaluationRepository = evaluationRepository;
            this._productRepository = productRepository;
            this._categoryRepository = categoryRepository;
            this._manufacturerRepository = manufacturerRepository;
            this._mapper = mapper;
            this._evaluationFacade = evaluationFacade;
        }

        public List<ProductListModel> GetAll()
        {
            var productEntityList = _productRepository.GetAll();
            foreach (var productEntity in productEntityList)
            {
                int avg = 0;
                int i = 0;
                IList<EvaluationEntity> evaluations = _evaluationRepository.GetByProductId(productEntity.Id);
                foreach (var eval in evaluations)
                {
                    i++;
                    avg += eval.PercentEvaluation;
                }

                if (i==0)
                {
                    productEntity.AverageScore = -1;
                }
                else
                {
                    productEntity.AverageScore = avg / i;
                }
                productEntity.Manufacturer = _manufacturerRepository.GetById(productEntity.ManufacturerId);
                productEntity.Category = _categoryRepository.GetById(productEntity.CategoryId);
            }

            return _mapper.Map<List<ProductListModel>>(productEntityList);
        }

        public ProductDetailModel GetById(Guid id)
        {
            var productEntity = _productRepository.GetById(id);
            if (productEntity == null)
            {
                return null;
            }
            productEntity.Evaluations = _evaluationRepository.GetByProductId(id);
            int avg = 0;
            int i = 0;
            foreach (var eval in productEntity.Evaluations)
            {
                i++;
                avg += eval.PercentEvaluation;
            }

            if (i == 0)
            {
                productEntity.AverageScore = -1;
            }
            else
            {
                productEntity.AverageScore = avg / i;
            }
            productEntity.Manufacturer = _manufacturerRepository.GetById(productEntity.ManufacturerId);
            productEntity.Category = _categoryRepository.GetById(productEntity.CategoryId);
            return _mapper.Map<ProductDetailModel>(productEntity);
        }

        public Guid Create(ProductNewModel product)
        {
            var productEntity = _mapper.Map<ProductEntity>(product);
            return _productRepository.Insert(productEntity);
        }

        public Guid? Update(ProductUpdateModel productUpdateModel)
        {
            var productEntityExisting = _productRepository.GetById(productUpdateModel.Id);
            if (productEntityExisting == null)
            {
                return null;
            }
            productEntityExisting.Evaluations = _evaluationRepository.GetByProductId(productUpdateModel.Id);
            UpdateEvaluation(productUpdateModel, productEntityExisting);

            var productEntityUpdated = _mapper.Map<ProductEntity>(productUpdateModel);
            return _productRepository.Update(productEntityUpdated);
        }

        private void UpdateEvaluation(ProductUpdateModel productUpdateModel, ProductEntity productEntity)
        {
            var evaluationToDelete = productEntity.Evaluations.Where(evaluation =>  
                !productUpdateModel.Evaluations.Any(evaluations => evaluations.Id == evaluation.Id));
            
            foreach (var evaluation in evaluationToDelete)
            {
                _evaluationFacade.Delete(evaluation.Id);
            }
            
            var evaluationToInsert = productUpdateModel.Evaluations.Where(
                evaluation => !productEntity.Evaluations.Any(evaluations => evaluations.Id == evaluation.Id));

            foreach (var evaluation in evaluationToInsert)
            {
                evaluation.ProductId = productUpdateModel.Id;
                _evaluationFacade.Create(_mapper.Map<EvaluationNewModel>(evaluation));
            }

            var evaluationToUpdate = productUpdateModel.Evaluations.Where(
                evaluation => productEntity.Evaluations.Any(evaluations => evaluations.Id == evaluation.Id));

            foreach (var evaluation in evaluationToUpdate)
            {
                evaluation.ProductId = productUpdateModel.Id;
                _evaluationFacade.Update(evaluation);
            }
        }

        public void Delete(Guid id)
        {
            var productEntity = _productRepository.GetById(id);
            productEntity.Evaluations = _evaluationRepository.GetByProductId(id);
            foreach (var evaluation in productEntity.Evaluations)
            {
                _evaluationRepository.Remove(evaluation.Id);
            }

            _productRepository.Remove(id);
        }
    }
}