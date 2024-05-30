using System.ComponentModel.DataAnnotations;

namespace FiboApp2.Presentation.Requests
{
    public class CalculateNextRequest
    {
        [Required]
        public string Number { get; init; } = "0";
    }
}
