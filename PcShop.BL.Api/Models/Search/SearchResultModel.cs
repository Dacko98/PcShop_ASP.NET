using System.Collections.Generic;
using PcShop.BL.Api.Models.Evaluation;
using PcShop.BL.Api.Models.Manufacturer;
using PcShop.BL.Api.Models.Product;

namespace PcShop.BL.Api.Models.Search
{
    public class SearchResultModel
    {
        public List<EvaluationListModel> EvaluationEntities { get; set; }
        public List<ManufacturerListModel> ManufacturerEntities { get; set; }
        public List<ProductListModel> ProductEntities { get; set; }
    }
}
