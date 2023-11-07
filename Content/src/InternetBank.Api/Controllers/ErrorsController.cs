using FluentValidation;
using InternetBank.Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace InternetBank.Api.Controllers;

[Route("/error")]
public class ErrorsController : ApiController
{
    [HttpGet, HttpPost]
    public IActionResult Error()
    {
        var exceptionFeature = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

        if (exceptionFeature is DomainExceptions domainExceptions)
            return Problem(statusCode: domainExceptions.StatusCode, title: domainExceptions.Message);

        else if (exceptionFeature is ValidationException validationException)
        {
            foreach (var item in validationException.Errors)
            {
                ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
            }
            return ValidationProblem(ModelState);
            
            
        }
        else
            return Problem(statusCode: 500, title: exceptionFeature?.Message);

    }
}