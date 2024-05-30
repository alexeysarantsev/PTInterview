using FiboApp1.Presentation.FiboApp2Client.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace FiboApp1.Presentation.FiboApp2Client
{

    public static class FiboApp2ClientRegistration
    {
        public static IServiceCollection RegisterFiboApp2Client(this IServiceCollection services)
        {
            services.AddHttpClient<FiboApp2HttpClient>((serviceProvider, client) =>
            {
                client.BaseAddress = new Uri("http://localhost:5006");
            })
                .ConfigurePrimaryHttpMessageHandler(() =>
            {
                return new SocketsHttpHandler()
                {
                    PooledConnectionLifetime = TimeSpan.FromMinutes(15)
                };
            })
                .SetHandlerLifetime(Timeout.InfiniteTimeSpan);

            services.AddSingleton<IFiboApp2Client, FiboApp2HttpClient>();
            return services;
        }
    }

}
