using FiboApp1.Application.Abstractions.Services;
using FiboApp1.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FiboApp1.Application
{
    public static class ApplicationRegistration
    {
        public static IServiceCollection RegisterApplicationServices(this IServiceCollection services)
        {
            services.AddSingleton<IFiboCalculator, FiboCalculator>();
            services.AddSingleton<IFiboService, FiboService>();
            return services;
        }
    }
}
