using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace JoVisionBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BirthDateController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAge(
            [FromQuery] string name= "anonymous",
            [FromQuery] int? years=null,
            [FromQuery] int? months=null,
            [FromQuery] int? days=null)
        {
            if(years == null || months == null || days == null)
            {
                return Ok($"Hello {name}, I can’t calculate your age without knowing your birthdate!");
            }
            try
            {
                var birhtDate=new DateTime(years.Value, months.Value, days.Value);
                var today=DateTime.Today;
                int age=today.Year-birhtDate.Year;
                if (birhtDate.Date > today.AddYears(-age)) age--;
                return Ok($"Hello {name} your age is {age}");
            }
            catch(Exception)
            {
                return BadRequest("Invalid date components provided.");



            }
        }

    }
}
