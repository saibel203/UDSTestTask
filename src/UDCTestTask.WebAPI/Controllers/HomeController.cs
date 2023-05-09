using Microsoft.AspNetCore.Mvc;

namespace UDCTestTask.WebAPI.Controllers;

public class HomeController : ApiController
{
    [HttpGet("test")] // /api/home/test
    public IActionResult Test()
    {
        return Ok("Hello world!");
    }
}
