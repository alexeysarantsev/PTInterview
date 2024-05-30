using FiboApp2.Presentation.Publisher.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace FiboApp2.Presentation.Publisher
{
    public static class PublisherRegistration
    {
        public static IServiceCollection RegisterPublisher(this IServiceCollection services)
        {
            services.AddSingleton<IPublisher, Publisher>();
            return services;
        }
    }
}
