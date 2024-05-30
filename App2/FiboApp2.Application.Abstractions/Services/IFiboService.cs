using System.Numerics;

namespace FiboApp2.Application.Abstractions.Services
{
    public interface IFiboService
    {
        Task ProcessAsync(BigInteger number, CancellationToken cancellationToken);
    }
}
