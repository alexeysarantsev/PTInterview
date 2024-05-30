namespace FiboApp1.Presentation.Abstractions.Consumers
{
    public interface IFiboConsumer
    {
        public void Consume(string message);
    }
}
