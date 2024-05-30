using System.Numerics;

namespace FiboApp1.Presentation.FiboApp2Client.Abstractions
{
    public interface IFiboApp2Client
    {
        Task SendFiboAsync(BigInteger number, CancellationToken cancellationToken);
    }
}
