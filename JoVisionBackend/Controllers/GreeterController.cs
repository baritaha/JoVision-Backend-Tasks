using Microsoft.AspNetCore.Mvc;

namespace JoVisionBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GreeterController :ControllerBase
    {
        [HttpGet]
        public IActionResult GetGreeting([FromQuery] string name = "anonymous")
        {
            
            return Ok($"Hello {name} greeting");

        }
    }
}
