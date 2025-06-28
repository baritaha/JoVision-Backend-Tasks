using JoVisionBackend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JoVisionBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BirthDateTask46Controller : ControllerBase
    {
        [HttpPost]
        public IActionResult PostAge([FromForm] BirthDateFormRequestTask46 request)
        {
            var name = string.IsNullOrWhiteSpace(request.Name) ? "anonymous" : request.Name;

            if (request.Years == null || request.Months == null || request.Days == null)
            {
                return Ok($"Hello {name}, I can’t calculate your age without knowing your birthdate!");
            }

            try
            {
                var birthDate = new DateTime(request.Years.Value, request.Months.Value, request.Days.Value);
                var today = DateTime.Today;

                int age = today.Year - birthDate.Year;
                if (birthDate.Date > today.AddYears(-age)) age--;

                return Ok($"Hello {name}, your age is {age}");
            }
            catch
            {
                return BadRequest("Invalid date components. Please check your input.");
            }
        }
    }
}
