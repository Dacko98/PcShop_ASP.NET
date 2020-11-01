using System.Collections.Generic;
using PcShop.BL.Api.Facades.Interfaces;
using PcShop.BL.Api.Models.Evaluation;
using PcShop.BL.Api.Models.Interfaces;
using PcShop.BL.Api.Models.Manufacturer;
using PcShop.BL.Api.Models.Product;

namespace PcShop.BL.Api.Facades
{
    public class SearchFacade : IAppFacade
    {
        private readonly EvaluationFacade evaluationFacade;
        private readonly ManufacturerFacade manufacturerFacade;
        private readonly ProductFacade productFacade;

        public SearchFacade(
            EvaluationFacade evaluationFacade,
            ManufacturerFacade manufacturerFacade,
            ProductFacade productFacade)
        {
            this.evaluationFacade = evaluationFacade;
            this.manufacturerFacade = manufacturerFacade;
            this.productFacade = productFacade;
        }

        public IList<IListModel> GetAllContainingText(string searchedText)
        {
            searchedText = searchedText.ToLower();

            IList<IListModel> foundEntities = new List<IListModel>();

            List<EvaluationListModel> evaluationEntities = evaluationFacade.GetAll();
            List<ManufacturerListModel> manufacturerEntities = manufacturerFacade.GetAll();
            List<ProductListModel> productEntities = productFacade.GetAll();


            foreach (var evaluationEntity in evaluationEntities)
                if ((evaluationEntity.TextEvaluation ?? "").ToLower().Contains(searchedText))
                    foundEntities.Add(evaluationEntity);

            foreach (var manufacturerEntity in manufacturerEntities)
                if ((manufacturerEntity.Name ?? "").ToLower().Contains(searchedText)
                    || (manufacturerEntity.Description ?? "").ToLower().Contains(searchedText)
                    || (manufacturerEntity.CountryOfOrigin ?? "").ToLower().Contains(searchedText))
                    foundEntities.Add(manufacturerEntity);

            foreach (var productEntity in productEntities)
                if ((productEntity.Name ?? "").ToLower().Contains(searchedText)
                    || (productEntity.Description ?? "").ToLower().Contains(searchedText))
                    foundEntities.Add(productEntity);

            return foundEntities;
        }
    }
}