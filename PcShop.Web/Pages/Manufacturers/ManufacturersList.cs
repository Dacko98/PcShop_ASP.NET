using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;
using PcShop.WEB.BL.Facades;
using PcShop.BL.Api.Models.Manufacturer;


namespace PcShop.Web.Pages.Manufacturers
{
    public partial class ManufacturersList
    {
        [Inject]
        private ManufacturersFacade ManufacturersFacade { get; set; }

        private ICollection<ManufacturerListModel> Manufacturers { get; set; } = new List<ManufacturerListModel>();

        protected override async Task OnInitializedAsync()
        {
            await LoadData();

            await base.OnInitializedAsync();
        }

        private async Task LoadData()
        {
            Manufacturers = await ManufacturersFacade.GetManufacturersAsync();
        }
    }
}