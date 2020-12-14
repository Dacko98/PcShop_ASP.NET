using PcShop.Common.Installers;
using Microsoft.Extensions.DependencyInjection;
using PcShop.DAL.Repositories;

namespace PcShop.DAL.Installers
{
    public class DalInstaller : IInstaller
    {
        public void Install(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<Storage>();

            serviceCollection.Scan(selector =>
                selector.FromAssemblyOf<DalInstaller>()
                    .AddClasses(filter => filter.AssignableTo(typeof(IAppRepository<>)))
                    .AsSelf()
                    .WithTransientLifetime());
        }
    }
}