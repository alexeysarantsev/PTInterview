using EasyNetQ;
using FiboApp1.Application;
using FiboApp1.Presentation.FiboApp2Client;
using FiboApp1.Presentation.Abstractions.Consumers;
using FiboApp1.Presentation.Consumers;
using FiboApp1.Presentation.FiboApp2Client.Abstractions;

namespace FiboApp1.Presentation
{
    public sealed class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration, IWebHostEnvironment hostEnvironment)
        {
            _configuration = configuration;
            _hostEnvironment = hostEnvironment;
        }

        private readonly IWebHostEnvironment _hostEnvironment;

        public void ConfigureServices(IServiceCollection services)
        {
            RegisterServices(services);

            services.AddMvc(options => options.EnableEndpointRouting = false);

            services.AddSwaggerGen();
            services.AddLogging();

            var connectionString = _configuration.GetValue<string>("RabbitMQConnectionString");
            services.AddSingleton<IBus>(RabbitHutch.CreateBus(connectionString));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var serviceProvider = app.ApplicationServices;
            ConfigureQueueConsumer(serviceProvider);
            StartComputing(serviceProvider);
        }

        private void RegisterServices(IServiceCollection services)
        {
            services.RegisterApplicationServices();
            services.RegisterFiboApp2Client();

            services.AddSingleton<IFiboConsumer, Consumer>();
        }

        private void ConfigureQueueConsumer(IServiceProvider serviceProvider)
        {
            var bus = serviceProvider.GetService<IBus>()!;
            bus.PubSub.Subscribe<string>("fibo", message =>
            {
                var consumer = serviceProvider.GetService<IFiboConsumer>()!;
                consumer.Consume(message);
            });
        }

        private void StartComputing(IServiceProvider serviceProvider)
        {
            var parallelComputations = _configuration.GetValue<int>("ParallelComputations");

            for (int i = 0; i < parallelComputations; i++)
            {
                Task.Run(() => { serviceProvider.GetRequiredService<IFiboApp2Client>().SendFiboAsync(0, default); });
            }
        }
    }
}
