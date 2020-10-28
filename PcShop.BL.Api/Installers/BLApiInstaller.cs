using PcShop.Common.Installers;
using Microsoft.Extensions.DependencyInjection;
using PcShop.BL.Api.Facades;
using PcShop.BL.Api.Facades.Interfaces;

namespace PcShop.BL.Api.Installers
{
    public class BLApiInstaller : IInstaller
    {
        public void Install(IServiceCollection serviceCollection)
        {
            serviceCollection.Scan(selector =>
                selector.FromAssemblyOf<BLApiInstaller>()
                    .AddClasses(filter => filter.AssignableTo<IAppFacade>())
                    .AsSelf()
                    .WithTransientLifetime());
        }
    }
}