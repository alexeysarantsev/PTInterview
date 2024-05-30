using System.Numerics;

namespace FiboApp2.Presentation.Publisher.Abstractions
{
    public interface IPublisher
    {
        Task PublishAsync(BigInteger number, CancellationToken cancellationToken);

        void Publish(BigInteger number);
    }
}