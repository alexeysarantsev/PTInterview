using FiboApp2.Presentation.Requests;
using System.Numerics;

namespace FiboApp2.Presentation.Converters
{
    internal static class CalculateNextRequestConverter
    {
        public static BigInteger ConvertToBigInteger(CalculateNextRequest request)
        {
            return new BigInteger(Convert.FromBase64String(request.Number));
            //return BigInteger.Parse(request.Number);
        }
    }
}
