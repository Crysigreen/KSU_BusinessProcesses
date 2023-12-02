using Microsoft.AspNetCore.Mvc;

namespace KSU_BProcesses.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Успешно");
        }
    }
}
