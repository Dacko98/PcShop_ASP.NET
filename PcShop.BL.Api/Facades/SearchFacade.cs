using PcShop.BL.Api.Facades.Interfaces;
using PcShop.BL.Api.Models.Search;

namespace PcShop.BL.Api.Facades
{
    public class SearchFacade : IAppFacade
    {
        private readonly EvaluationFacade _evaluationFacade;
        private readonly ManufacturerFacade _manufacturerFacade;
        private readonly ProductFacade _productFacade;

        public SearchFacade(
            EvaluationFacade evaluationFacade,
            ManufacturerFacade manufacturerFacade,
            ProductFacade productFacade)
        {
            this._evaluationFacade = evaluationFacade;
            this._manufacturerFacade = manufacturerFacade;
            this._productFacade = productFacade;
        }

        public SearchResultModel GetAllContainingText(string searchedText)
        {
            searchedText = searchedText.ToLower();

            return new SearchResultModel
            {
                EvaluationEntities = _evaluationFacade.GetAll()
                    .FindAll(e => (e.TextEvaluation ?? "").ToLower().Contains(searchedText)),

                ManufacturerEntities = _manufacturerFacade.GetAll()
                    .FindAll(m => (m.Name ?? "").ToLower().Contains(searchedText)
                                  || (m.Description ?? "").ToLower().Contains(searchedText)
                                  || (m.CountryOfOrigin ?? "").ToLower().Contains(searchedText)),

                ProductEntities = _productFacade.GetAll()
                    .FindAll(p => (p.Name ?? "").ToLower().Contains(searchedText)
                                  || (p.Description ?? "").ToLower().Contains(searchedText))
            };
        }
    }
}