using FiboApp2.Application.Abstractions.Services;
using FiboApp2.Presentation.Requests;
using Microsoft.AspNetCore.Mvc;
using System.Numerics;

namespace FiboApp2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FiboController : ControllerBase
    {
        private readonly IFiboService _fiboService;
        public FiboController(IFiboService fiboService) 
        {
            _fiboService = fiboService;
        }

        [HttpPost]
        [Route("Fibo")]
        //public async Task<IActionResult> CalculateNextAsync(CalculateNextRequest request, CancellationToken cancellationToken)
        public IActionResult CalculateNext(CalculateNextRequest request)
        {
            try
            {
                var number = new BigInteger(Convert.FromBase64String(request.Number));

                //операция может занять длительное время. Давайте вернем 200 и запустим таску
                _ = _fiboService.ProcessAsync(number, default);

                return Ok();
            }
            catch (FormatException exception)
            {
                return BadRequest(exception.Message);
            }
        }
    }
}
