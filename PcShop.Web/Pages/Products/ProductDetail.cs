using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using PcShop.BL.Api.Models.Evaluation;
using PcShop.BL.Api.Models.Product;
using PcShop.WEB.BL.Facades;

namespace PcShop.Web.Pages.Products
{
    public partial class ProductDetail : ComponentBase
    {
        [Inject] private ProductsFacade ProductFacade { get; set; }

        [Inject] private EvaluationsFacade EvaluationsFacade { get; set; }

        [Parameter] public Guid Id { get; set; }
        [Parameter] public Guid EvaluationId { get; set; }
        
        public ProductDetailModel Product { get; set; }
        private ICollection<EvaluationListModel> Evaluations { get; set; } = new List<EvaluationListModel>();

        protected override async Task OnInitializedAsync()
        {
            if (EvaluationId != Guid.Empty)
            {
                EvaluationDetailModel eval = await EvaluationsFacade.GetEvaluationAsync(EvaluationId);
                Id = eval.ProductId;
            }

            Product = await ProductFacade.GetProductAsync(Id);
            Evaluations = await EvaluationsFacade.GetEvaluationsAsync();
            await base.OnInitializedAsync();
        }
    }
}
