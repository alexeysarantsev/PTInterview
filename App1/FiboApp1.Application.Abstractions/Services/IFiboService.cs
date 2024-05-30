using System.Numerics;

namespace FiboApp1.Application.Abstractions.Services
{
    public interface IFiboService
    {
        Task Process(BigInteger bigInteger, CancellationToken cancellationToken);
    }
}
