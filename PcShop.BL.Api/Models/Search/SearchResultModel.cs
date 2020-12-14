using System.Collections.Generic;
using PcShop.BL.Api.Models.Evaluation;
using PcShop.BL.Api.Models.Interfaces;
using PcShop.BL.Api.Models.Manufacturer;
using PcShop.BL.Api.Models.Product;

namespace PcShop.BL.Api.Models.Search
{
    public class SearchResultModel : IModel
    {
        public List<EvaluationListModel> EvaluationEntities { get; set; } = new List<EvaluationListModel>();
        public List<ManufacturerListModel> ManufacturerEntities { get; set; } = new List<ManufacturerListModel>();
        public List<ProductListModel> ProductEntities { get; set; } = new List<ProductListModel>();
    }
}
