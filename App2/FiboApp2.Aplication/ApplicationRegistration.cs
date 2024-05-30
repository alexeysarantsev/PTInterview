using FiboApp2.Aplication.Services;
using FiboApp2.Application.Abstractions.Services;
using FiboApp2.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FiboApp2.Application
{
    public static class ApplicationRegistration
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<IFiboCalculator, FiboCalculator>();
            services.AddSingleton<IFiboService, FiboService>();
            return services;
        }
    }
}
