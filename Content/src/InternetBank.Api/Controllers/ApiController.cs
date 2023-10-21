using Microsoft.AspNetCore.Mvc;

namespace InternetBank.Api.Controllers;

[ApiController]
[Route("api")]
public class ApiController : ControllerBase
{
    protected IActionResult Problem()
    {
        return Problem(statusCode : 500, title : "fefe");
    }
}