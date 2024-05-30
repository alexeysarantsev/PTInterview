using EasyNetQ;
using FiboApp2.Application;
using FiboApp2.Presentation.Publisher;

namespace FiboApp2.Presentation
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
            services.RegisterServices();
            services.RegisterPublisher();
            services.AddMvc(options => options.EnableEndpointRouting = false);

            services.AddSwaggerGen();
            services.AddLogging();

            var connectionString = _configuration.GetValue<string>("RabbitMQConnectionString");
            services.AddSingleton<IBus>(RabbitHutch.CreateBus(connectionString));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger()
               .UseSwaggerUI(c =>
               {
                   c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
               });

            app.UseMvc();
        }
    }
}
