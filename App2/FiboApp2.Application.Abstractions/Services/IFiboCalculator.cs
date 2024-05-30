using System.Numerics;

namespace FiboApp2.Application.Abstractions.Services
{
    public interface IFiboCalculator
    {
        BigInteger Next(BigInteger current);
    }
}
