using Microsoft.AspNetCore.Mvc;

namespace FM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet("Testing")]
        public IActionResult Testing()
        {
            return Ok("Testing");
        }

        [HttpGet("Testing1")]
        public IActionResult Testing1()
        {
            return Ok("Testing1");
        }

        [HttpGet("Testing2")]
        public IActionResult Testing2()
        {
            return Ok("Testing2");
        }

        [HttpGet("Testing3")]
        public IActionResult Testing3()
        {
            return Ok("Testing3");
        }
    }
}
