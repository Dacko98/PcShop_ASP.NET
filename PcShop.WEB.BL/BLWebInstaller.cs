using PcShop.Common.Installers;
using Microsoft.Extensions.DependencyInjection;
using PcShop.BL.Api.Facades.Interfaces;
using PcShop.Console.Api;

namespace PcShop.WEB.BL
{
    public class BLWebInstaller : IInstaller
    {
        public void Install(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<ICategoryClient, CategoryClient>();
            serviceCollection.AddTransient<IProductClient, ProductClient>();
            serviceCollection.AddTransient<IEvaluationClient, EvaluationClient>();
            serviceCollection.AddTransient<IManufacturerClient, ManufacturerClient>();
            serviceCollection.AddTransient<ISearchClient, SearchClient>();
            serviceCollection.Scan(selector =>
            selector.FromCallingAssembly()
            .AddClasses(classes => classes.AssignableTo<IAppFacade>())
                .AsSelfWithInterfaces()
                .WithTransientLifetime());
        }
    }

}