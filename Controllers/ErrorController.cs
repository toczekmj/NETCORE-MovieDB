using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MovieApi.Controllers;

[ApiController]
// [Route("[controller]")]
public class ErrorController : Controller
{
    [Route("/Errorhandler")]
    [AllowAnonymous]
    [NonAction]
    public IActionResult Get()
    {
        return StatusCode(StatusCodes.Status500InternalServerError);
    }
}