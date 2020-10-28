using Microsoft.Extensions.DependencyInjection;

namespace PcShop.Common.Installers
{
    public interface IInstaller
    {
        void Install(IServiceCollection serviceCollection);
    }
}