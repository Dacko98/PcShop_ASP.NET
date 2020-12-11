using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PcShop.BL.Api.Facades.Interfaces;
using PcShop.BL.Api.Models.Manufacturer;
using PcShop.Console.Api;

namespace PcShop.WEB.BL.Facades
{
    public class ManufacturersFacade : IAppFacade
    {
        private readonly IManufacturerClient manufacturerClient;

        public ManufacturersFacade(IManufacturerClient manufacturerClient)
        {
            this.manufacturerClient = manufacturerClient;
        }

        public async Task<ICollection<ManufacturerListModel>> GetManufacturersAsync()
        {
            return await manufacturerClient.ManufacturerGetAsync();
        }

        public async Task<ManufacturerDetailModel> GetManufacturerAsync(Guid id)
        {
            return await manufacturerClient.ManufacturerGetAsync(id);
        }

        public async Task<Guid> CreateAsync(ManufacturerNewModel data)
        {
            return await manufacturerClient.ManufacturerPostAsync(data);
        }

        public async Task<Guid> UpdateAsync(ManufacturerUpdateModel data)
        {
            return await manufacturerClient.ManufacturerPutAsync(data);
        }   

        public async Task DeleteAsync(Guid id)
        {
            await manufacturerClient.ManufacturerDeleteAsync(id);
        }
    }
}