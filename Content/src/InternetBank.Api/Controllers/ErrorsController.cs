using InternetBank.Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace InternetBank.Api.Controllers;

[Route("/error")]
public class ErrorsController : ApiController
{
    public IActionResult Error()
    {
        var exceptionFeature = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;
        if (exceptionFeature is MyCustomEx custom)
        {

            return Problem(statusCode: 400, title: custom.desc);


        }
        return Problem(statusCode: 400, title: "exceptionFeature.Message");

    }
}