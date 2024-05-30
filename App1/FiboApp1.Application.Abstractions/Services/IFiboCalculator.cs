using System.Numerics;

namespace FiboApp1.Application.Abstractions.Services
{
    public interface IFiboCalculator
    {
        BigInteger Next(BigInteger current);
    }
}
