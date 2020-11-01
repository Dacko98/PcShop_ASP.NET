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
        private readonly ProductRepository productRepository;
        private readonly EvaluationRepository evaluationRepository;
        private readonly ManufacturerRepository manufacturerRepository;
        private readonly CategoryRepository categoryRepository;
        private readonly EvaluationFacade evaluationFacade;
        private readonly IMapper mapper;

        public ProductFacade(
            ProductRepository productRepository,
            EvaluationRepository evaluationRepository,
            ManufacturerRepository manufacturerRepository,
            CategoryRepository categoryRepository,
            EvaluationFacade evaluationFacade,
            IMapper mapper)
        {
            this.evaluationRepository = evaluationRepository;
            this.productRepository = productRepository;
            this.categoryRepository = categoryRepository;
            this.manufacturerRepository = manufacturerRepository;
            this.mapper = mapper;
            this.evaluationFacade = evaluationFacade;
        }

        public List<ProductListModel> GetAll()
        {
            var productEntityList = productRepository.GetAll();
            foreach (var productEntity in productEntityList)
            {
                productEntity.Manufacturer = manufacturerRepository.GetById(productEntity.ManufacturerId);
                productEntity.Category = categoryRepository.GetById(productEntity.CategoryId);
            }
            return mapper.Map<List<ProductListModel>>(productEntityList);
        }

        public ProductDetailModel GetById(Guid id)
        {
            var productEntity = productRepository.GetById(id);
            if (productEntity == null)
            {
                return null;
            }
            productEntity.Evaluations = evaluationRepository.GetByProductId(id);
            productEntity.Manufacturer = manufacturerRepository.GetById(productEntity.ManufacturerId);
            productEntity.Category = categoryRepository.GetById(productEntity.CategoryId);
            return mapper.Map<ProductDetailModel>(productEntity);
        }

        public Guid Create(ProductNewModel product)
        {
            var productEntity = mapper.Map<ProductEntity>(product);
            return productRepository.Insert(productEntity);
        }

        public Guid? Update(ProductUpdateModel productUpdateModel)
        {
            var productEntityExisting = productRepository.GetById(productUpdateModel.Id);
            if (productEntityExisting == null)
            {
                return null;
            }
            productEntityExisting.Evaluations = evaluationRepository.GetByProductId(productUpdateModel.Id);
            UpdateEvaluation(productUpdateModel, productEntityExisting);

            var productEntityUpdated = mapper.Map<ProductEntity>(productUpdateModel);
            return productRepository.Update(productEntityUpdated);
        }

        private void UpdateEvaluation(ProductUpdateModel productUpdateModel, ProductEntity productEntity)
        {
            var evaluationToDelete = productEntity.Evaluations.Where(evaluation =>  
                !productUpdateModel.Evaluations.Any(evaluations => evaluations.Id == evaluation.Id));
            
            foreach (var evaluation in evaluationToDelete)
            {
                evaluationFacade.Delete(evaluation.Id);
            }
            
            var evaluationToInsert = productUpdateModel.Evaluations.Where(
                evaluation => !productEntity.Evaluations.Any(evaluations => evaluations.Id == evaluation.Id));

            foreach (var evaluation in evaluationToInsert)
            {
                evaluation.ProductId = productUpdateModel.Id;
                evaluationFacade.Create(mapper.Map<EvaluationNewModel>(evaluation));
            }

            var evaluationToUpdate = productUpdateModel.Evaluations.Where(
                evaluation => productEntity.Evaluations.Any(evaluations => evaluations.Id == evaluation.Id));

            foreach (var evaluation in evaluationToUpdate)
            {
                evaluation.ProductId = productUpdateModel.Id;
                evaluationFacade.Update(evaluation);
            }
        }

        public void Delete(Guid id)
        {
            var productEntity = productRepository.GetById(id);
            productEntity.Evaluations = evaluationRepository.GetByProductId(id);
            foreach (var evaluation in productEntity.Evaluations)
            {
                evaluationRepository.Remove(evaluation.Id);
            }

            productRepository.Remove(id);
        }
    }
}