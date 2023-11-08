using System.Security.Claims;
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
}