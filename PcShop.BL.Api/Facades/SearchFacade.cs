using System.Collections.Generic;
using System.Linq;
using PcShop.BL.Api.Facades.Interfaces;
using PcShop.BL.Api.Models.Evaluation;
using PcShop.BL.Api.Models.Manufacturer;
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
            searchedText = searchedText.ToLower().Trim();

            var searchModel = new SearchResultModel
            {
                EvaluationEntities = _evaluationFacade.GetAll(),
                ManufacturerEntities = _manufacturerFacade.GetAll(),
                ProductEntities = _productFacade.GetAll()
            };

            var searchResultModel = new SearchResultModel();

            var texts = searchedText.Split(' ').ToList();
            texts.RemoveAll(s => s.Trim() == "");

            bool firstLoop = true;

            foreach (var t in texts)
            {
                searchResultModel.EvaluationEntities.AddRange(
                    searchModel.EvaluationEntities.FindAll(e => (e.TextEvaluation ?? "").ToLower().Contains(t)));

                searchResultModel.ProductEntities.AddRange(searchModel.ProductEntities.FindAll(p =>
                    (p.Name ?? "").ToLower().Contains(t) ||
                    (p.Description ?? "").ToLower().Contains(t)));

                searchResultModel.ManufacturerEntities.AddRange(searchModel.ManufacturerEntities.FindAll(m =>
                    (m.Name ?? "").ToLower().Contains(t) ||
                    (m.Description ?? "").ToLower().Contains(t) ||
                    (m.CountryOfOrigin ?? "").ToLower().Contains(t)));

                if (firstLoop)
                    firstLoop = false;
                else
                {
                    // for searching phrases all required
                    searchResultModel.EvaluationEntities = searchResultModel.EvaluationEntities.GroupBy(e => e.Id)
                        .SelectMany(e => e.Skip(1)).ToList();

                    searchResultModel.ManufacturerEntities = searchResultModel.ManufacturerEntities.GroupBy(e => e.Id)
                        .SelectMany(e => e.Skip(1)).ToList();

                    searchResultModel.ProductEntities = searchResultModel.ProductEntities.GroupBy(e => e.Id)
                        .SelectMany(e => e.Skip(1)).ToList();

                    // for searching phrases 1 required
                    //searchResultModel.ProductEntities = searchResultModel.ProductEntities.Distinct().ToList();
                    //searchResultModel.ManufacturerEntities = searchResultModel.ManufacturerEntities.Distinct().ToList();
                    //searchResultModel.EvaluationEntities = searchResultModel.EvaluationEntities.Distinct().ToList();
                }
            }

            return searchResultModel;

            //return new SearchResultModel
            //{
            //    EvaluationEntities = _evaluationFacade.GetAll()
            //        .FindAll(e => (e.TextEvaluation ?? "").ToLower().Contains(searchedText)),

            //    ManufacturerEntities = _manufacturerFacade.GetAll()
            //        .FindAll(m => (m.Name ?? "").ToLower().Contains(searchedText)
            //                      || (m.Description ?? "").ToLower().Contains(searchedText)
            //                      || (m.CountryOfOrigin ?? "").ToLower().Contains(searchedText)),

            //    ProductEntities = _productFacade.GetAll()
            //        .FindAll(p => (p.Name ?? "").ToLower().Contains(searchedText)
            //                      || (p.Description ?? "").ToLower().Contains(searchedText))
            //};
        }
    }
}