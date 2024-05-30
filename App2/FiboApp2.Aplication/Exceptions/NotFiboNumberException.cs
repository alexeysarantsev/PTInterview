using System.Numerics;

namespace FiboApp2.Application.Exceptions
{
    internal class NotFiboNumberException : Exception
    {
        public BigInteger Number { get; }

        public NotFiboNumberException(string message, Exception exception, BigInteger number) : base(message, exception)
        {
            Number = number;
        }

        public NotFiboNumberException(string message, BigInteger number) : base(message)
        {
            Number = number;
        }

    }
}
