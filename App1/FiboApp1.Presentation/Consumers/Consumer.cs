using FiboApp1.Application.Abstractions.Services;
using FiboApp1.Presentation.Abstractions.Consumers;
using System.Numerics;

namespace FiboApp1.Presentation.Consumers
{
    internal sealed class Consumer : IFiboConsumer
    {
        private readonly IFiboService _service;

        public Consumer(IFiboService service)
        {
            _service = service;
        }

        public void Consume(string message)
        {
            var number = new BigInteger(Convert.FromBase64String(message));
            _ = _service.Process(number, default);
        }
    }
}
