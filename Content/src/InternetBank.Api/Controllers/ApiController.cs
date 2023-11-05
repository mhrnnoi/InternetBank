using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace InternetBank.Api.Controllers;

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
        return claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
    }
}