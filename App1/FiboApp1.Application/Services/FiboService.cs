using FiboApp1.Application.Abstractions.Services;
using FiboApp1.Presentation.FiboApp2Client.Abstractions;
using System.Numerics;

namespace FiboApp1.Application.Services
{
    internal sealed class FiboService : IFiboService
    {
        private readonly IFiboApp2Client _fiboApp2Client;
        private readonly IFiboCalculator _fiboCalculator;

        public FiboService(IFiboCalculator fiboCalculator, IFiboApp2Client fiboApp2Client)
        {
            _fiboApp2Client = fiboApp2Client;
            _fiboCalculator = fiboCalculator;
        }

        public async Task Process(BigInteger bigInteger, CancellationToken cancellationToken)
        {
            var next = _fiboCalculator.Next(bigInteger);

            await _fiboApp2Client.SendFiboAsync(next, cancellationToken);
        }
    }
}
