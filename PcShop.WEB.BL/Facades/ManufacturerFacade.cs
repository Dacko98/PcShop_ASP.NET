using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PcShop.BL.Api.Facades.Interfaces;
using PcShop.BL.Api.Models.Manufacturer;
using PcShop.Console.Api;

namespace PcShop.WEB.BL.Facades
{
    public class ManufacturersFacade : IAppFacade
    {
        private readonly IManufacturerClient _manufacturerClient;

        public ManufacturersFacade(IManufacturerClient manufacturerClient)
        {
            this._manufacturerClient = manufacturerClient;
        }

        public async Task<ICollection<ManufacturerListModel>> GetManufacturersAsync()
        {
            return await _manufacturerClient.ManufacturerGetAsync();
        }

        public async Task<ManufacturerDetailModel> GetManufacturerAsync(Guid id)
        {
            return await _manufacturerClient.ManufacturerGetAsync(id);
        }

        public async Task<Guid> CreateAsync(ManufacturerNewModel data)
        {
            return await _manufacturerClient.ManufacturerPostAsync(data);
        }

        public async Task<Guid> UpdateAsync(ManufacturerUpdateModel data)
        {
            return await _manufacturerClient.ManufacturerPutAsync(data);
        }   

        public async Task DeleteAsync(Guid id)
        {
            await _manufacturerClient.ManufacturerDeleteAsync(id);
        }
    }
}