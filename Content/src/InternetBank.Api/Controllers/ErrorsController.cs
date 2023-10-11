using FluentValidation;
using InternetBank.Domain.Exceptions;
using InternetBank.Domain.Exceptions.User.AggregateExceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

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

            return Problem(statusCode: domainExceptions.StatusCode, title: "expected error : " + domainExceptions.Title);


        }
        else if (exceptionFeature is ValidationException validationException)
        {
            foreach (var item in validationException.Errors)
            {
                ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
            }

            return ValidationProblem(ModelState);


        }

        else
        {
            return Problem(statusCode: 500, title: "unexpected error :" + exceptionFeature?.Message);
        }

    }
}