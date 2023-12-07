using Microsoft.AspNetCore.Mvc;

namespace PersonalizeFIT.TrainingAPI.Controllers
{
    [Route("/")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok("Training API");
        }
    }
}
