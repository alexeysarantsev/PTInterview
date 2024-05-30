using EasyNetQ;
using FiboApp2.Presentation.Publisher.Abstractions;
using Microsoft.Extensions.Logging;
using System.Numerics;

namespace FiboApp2.Presentation.Publisher
{
    public class Publisher : IPublisher
    {
        private readonly IBus _bus;
        private readonly ILogger<Publisher> _logger;

        public Publisher(IBus bus, ILogger<Publisher> logger)
        {
            _bus = bus;
            _logger = logger;
        }

        public async Task PublishAsync(BigInteger number, CancellationToken cancellationToken)
        {
            _logger.LogInformation(number.ToString());
            var message = Convert.ToBase64String(number.ToByteArray());
            await _bus.PubSub.PublishAsync(message, "fibo", cancellationToken);
        }

        public void Publish(BigInteger number)
        {
            _logger.LogInformation(number.ToString());
            var message = Convert.ToBase64String(number.ToByteArray());
            _bus.PubSub.Publish(message, "fibo");
        }

    }
}