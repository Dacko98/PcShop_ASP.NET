using PcShop.Common.Installers;
using Microsoft.Extensions.DependencyInjection;
using PcShop.BL.Api.Facades.Interfaces;

namespace PcShop.BL.Api.Installers
{
    public class BlApiInstaller : IInstaller
    {
        public void Install(IServiceCollection serviceCollection)
        {
            serviceCollection.Scan(selector =>
                selector.FromAssemblyOf<BlApiInstaller>()
                    .AddClasses(filter => filter.AssignableTo<IAppFacade>())
                    .AsSelf()
                    .WithTransientLifetime());
        }
    }
}