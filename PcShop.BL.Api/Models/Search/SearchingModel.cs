using PcShop.BL.Api.Models.Interfaces;

namespace PcShop.BL.Api.Models.Search
{
    public class SearchingModel : IModel
    {
        private string _searchedPhrase;
        public string SearchedPhraseSet
        {
            get => _searchedPhrase;
            set => _searchedPhrase = value;
        }
        public string SearchedPhraseSearch => "/search/" + _searchedPhrase;
    }
}