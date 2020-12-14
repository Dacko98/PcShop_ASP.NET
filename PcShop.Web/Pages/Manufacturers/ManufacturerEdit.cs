using PcShop.BL.Api.Models.Manufacturer;
using Microsoft.AspNetCore.Components;
using PcShop.BL.Api.Models.Product;
using System.Threading.Tasks;
using PcShop.WEB.BL.Facades;
using System.Linq;
using System;

namespace PcShop.Web.Pages.Manufacturers
{
    public partial class ManufacturerEdit
    {
        [Inject] private ManufacturersFacade ManufacturersFacade { get; set; }
        [Inject] private ProductsFacade ProductsFacade { get; set; }
        [Inject] NavigationManager UriHelper { get; set; }

        [Parameter] public Guid Id { get; set; }

        private string ManufacturerOldName { get; set; }
        public ManufacturerDetailModel Manufacturer { get; set; }

        private bool NewManufacturer { get; set; }

        protected override async Task OnInitializedAsync()
        {
            NewManufacturer = Id == Guid.Empty;
            Manufacturer = NewManufacturer ? new ManufacturerDetailModel() { Logo = "default_manufacturer_logo.jpg" } : await ManufacturersFacade.GetManufacturerAsync(Id);
            ManufacturerOldName = Manufacturer.Name;

            await base.OnInitializedAsync();
        }

        protected async Task DeleteEntity()
        {
            if (!NewManufacturer)
                await ManufacturersFacade.DeleteAsync(Id);
            UriHelper.NavigateTo("/deleted/manufacturers");
        }

        protected async Task SaveData()
        {
            Guid response;
            if (NewManufacturer)
                response = await ManufacturersFacade.CreateAsync(new ManufacturerNewModel
                {
                    Name = Manufacturer.Name,
                    CountryOfOrigin = Manufacturer.CountryOfOrigin,
                    Description = Manufacturer.Description,
                    Logo = Manufacturer.Logo
                });
            else
            {
                var products = (await ProductsFacade.GetProductsAsync())
                    .ToList().FindAll(p => p.ManufacturerName == ManufacturerOldName);

                response = await ManufacturersFacade.UpdateAsync(new ManufacturerUpdateModel
                {
                    Id = Id,
                    Name = Manufacturer.Name,
                    Description = Manufacturer.Description,
                    Logo = Manufacturer.Logo,
                    CountryOfOrigin = Manufacturer.CountryOfOrigin,
                    Product = products.Select(product => new ProductOnlyIdUpdateModel { Id = product.Id }).ToList()
                });
            }

            UriHelper.NavigateTo("/manufacturer/" + response);
        }
    }
}
