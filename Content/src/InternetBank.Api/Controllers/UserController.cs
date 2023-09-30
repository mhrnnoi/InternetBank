using Asp.Versioning;
using InternetBank.Application.Features.Authentication.Commands.Register;
using InternetBank.Contracts.Requests.Users;
using InternetBank.Domain.Exceptions;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InternetBank.Api.Controllers;

[ApiVersion("1.0")]
[Route("/api/v{version:apiVersion}/[controller]/[action]")]
public class UserController : ApiController
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public UserController(ISender sender, IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        throw new MyCustomEx("lele", 400, "hehe desc");



        var command = _mapper.Map<RegisterCommand>(request);
        var result = await _sender.Send(command);
        return Ok(result);
    }
}
