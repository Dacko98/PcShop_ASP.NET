using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PcShop.BL.Api.Facades.Interfaces;
using PcShop.BL.Api.Models.Evaluation;
using PcShop.Console.Api;

namespace PcShop.WEB.BL.Facades
{
    public class EvaluationsFacade : IAppFacade
    {
        private readonly IEvaluationClient _evaluationClient;

        public EvaluationsFacade(IEvaluationClient evaluationClient)
        {
            this._evaluationClient = evaluationClient;
        }

        public async Task<ICollection<EvaluationListModel>> GetEvaluationsAsync()
        {
            return await _evaluationClient.EvaluationGetAsync();
        }

        public async Task<EvaluationDetailModel> GetEvaluationAsync(Guid id)
        {
            return await _evaluationClient.EvaluationGetAsync(id);
        }

        public async Task<Guid> CreateAsync(EvaluationNewModel data)
        {
            return await _evaluationClient.EvaluationPostAsync(data);
        }

        public async Task<Guid> UpdateAsync(EvaluationUpdateModel data)
        {
            return await _evaluationClient.EvaluationPutAsync(data);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _evaluationClient.EvaluationDeleteAsync(id);
        }
    }
}