using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using PcShop.BL.Api.Models.Category;
using PcShop.BL.Api.Models.Manufacturer;
using PcShop.BL.Api.Models.Product;
using PcShop.WEB.BL.Facades;

namespace PcShop.Web.Pages.Manufacturers
{
    public partial class ManufacturerEdit
    {
        [Inject] private ManufacturersFacade ManufacturersFacade { get; set; }
        [Inject] private ProductsFacade ProductsFacadde { get; set; }
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
                var products = (await ProductsFacadde.GetProductsAsync())
                    .ToList().FindAll(p => p.CategoryName == ManufacturerOldName);

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
