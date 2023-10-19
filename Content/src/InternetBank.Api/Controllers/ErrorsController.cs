using FluentValidation;
using InternetBank.Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace InternetBank.Api.Controllers;

[Route("/error")]
public class ErrorsController : ApiController
{
    [HttpPost]
    public IActionResult Error()
    {
        var exceptionFeature = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;
        
        if (exceptionFeature is DomainExceptions domainExceptions)
        {
            switch (domainExceptions.StatusCode)
            {
                case 401:
                    return Unauthorized(domainExceptions.Message);
                case 400:
                    return Problem(statusCode: 400, title: "expected error : " + domainExceptions.Title);

                default:
                    return Problem(statusCode: domainExceptions.StatusCode, title: "expected error : " + domainExceptions.Title);

            }


        }
        else if (exceptionFeature is ValidationException validationException)
        {
            foreach (var item in validationException.Errors)
            {
                ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
            }

            return ValidationProblem(ModelState);


        }
        else if (exceptionFeature is NullReferenceException)
        {
            return Problem(statusCode: 404, title: "expected error :" + exceptionFeature?.Message);
        }

        else
        {
            return Problem(statusCode: 500, title: "unexpected error :" + exceptionFeature?.Message);
        }

    }
}