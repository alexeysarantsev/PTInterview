using FiboApp1.Presentation.FiboApp2Client.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Numerics;
using System.Text;

namespace FiboApp1.Presentation.FiboApp2Client
{
    internal sealed class FiboApp2HttpClient : IFiboApp2Client
    {
        private readonly ILogger<FiboApp2HttpClient> _logger;
        private readonly HttpClient _httpClient;

        public FiboApp2HttpClient(HttpClient httpClient, IConfiguration configuration, ILogger<FiboApp2HttpClient> logger)
        {
            _httpClient = httpClient;
            _logger = logger;

            var handler = new SocketsHttpHandler
            {
                // Sets how long a connection can be in the pool to be considered reusable (by default - infinite)
                PooledConnectionLifetime = TimeSpan.FromMinutes(1),
            };

            _httpClient = new HttpClient(handler, disposeHandler: false);
            var fibo2Url = configuration["FiboApp2Url"];
            _httpClient.BaseAddress = new Uri(fibo2Url!);
        }

        public async Task SendFiboAsync(BigInteger number, CancellationToken cancellationToken)
        {
            var json = GetModel(number);
            using var httpRequestMessage = new HttpRequestMessage
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };
            httpRequestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            _logger.LogInformation(number.ToString());

            var response = await _httpClient.PostAsync("/api/Fibo/Fibo", httpRequestMessage.Content, cancellationToken);
        }

        private string GetModel(BigInteger number)
        {
            var numberBase64 = Convert.ToBase64String(number.ToByteArray());
            var model = JsonConvert.SerializeObject(new { number = numberBase64 }, Formatting.None);
            return model;
        }
    }
}
