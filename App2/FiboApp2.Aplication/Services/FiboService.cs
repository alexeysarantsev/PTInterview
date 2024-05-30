using FiboApp2.Application.Abstractions.Services;
using FiboApp2.Presentation.Publisher.Abstractions;
using System.Numerics;

namespace FiboApp2.Aplication.Services
{
    internal sealed class FiboService : IFiboService
    {
        private readonly IFiboCalculator _fiboCalculator;
        private readonly IPublisher _publisher;
        public FiboService(IFiboCalculator fiboCalculator, IPublisher publisher)
        {
            _fiboCalculator = fiboCalculator;
            _publisher = publisher;
        }

        public async Task ProcessAsync(BigInteger number, CancellationToken cancellationToken)
        {
            var next = _fiboCalculator.Next(number);

            await _publisher.PublishAsync(next, cancellationToken);
        }
    }
}
