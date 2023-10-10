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
            var dct = new Dictionary<string, string[]>();
            // dct["errors"] = validationException.Errors.Select(x => x.PropertyName + ": " + x.ErrorMessage).ToArray();
           foreach (var item in validationException.Errors)
           {
                ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
           }
            
            // var dct2 = new ValidationProblemDetails(dct);
            return ValidationProblem(ModelState);
            
            // return Problem(statusCode: 400, title: "expected error : some input is wrong");

        }
        // else if (exceptionFeature is UserAggregateExceptions userAggregateExceptions)
        // {
        //     return Problem(statusCode: userAggregateExceptions.StatusCode, title: "expected error : " + "some input is wrong.");

        // }
        else
        {
            return Problem(statusCode: 500, title: "unexpected error : internal server error");

        }

    }
}