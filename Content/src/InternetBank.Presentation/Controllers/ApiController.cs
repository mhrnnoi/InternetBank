using System.Net.Http.Headers;
using System.Security.Claims;
using ErrorOr;
using InternetBank.Domain.Exceptions.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InternetBank.Presentation.Controllers;

[ApiController]
[Route("api")]
public class ApiController : ControllerBase
{
    protected static int? GetApiVersion(HttpContext httpContext)
    {
        return httpContext.GetRequestedApiVersion()?.MajorVersion;
    }
    protected static string GetUserId(IEnumerable<Claim> claims)
    {
        var isParsed = claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
        if (isParsed is not null)
            return isParsed;
        else
            throw new InvalidCred();

    }
    protected IActionResult Problem(List<Error> errors)
    {
        HttpContext.Items.Add(HttpContextItemKeys.Errors, errors);
        var firstError = errors.First();
        var statusCode = firstError.Type switch
        {

            ErrorType.Failure => 400,
            ErrorType.Validation => 422,
            ErrorType.Conflict => 409,
            ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
            ErrorType.NotFound => 404,
            _ => 500

        };
        HttpContext.Response.StatusCode = statusCode;

        return Problem(statusCode: statusCode, title: firstError.Description);


    }
}