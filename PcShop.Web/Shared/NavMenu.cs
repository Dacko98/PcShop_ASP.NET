using Microsoft.AspNetCore.Components;
using PcShop.BL.Api.Models.Search;

namespace PcShop.Web.Shared
{
    public partial class NavMenu : ComponentBase
    {
        public SearchingModel SearchingModel { get; set; } = new SearchingModel();
    }
}