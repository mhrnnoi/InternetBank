using Asp.Versioning;
using InternetBank.Application.Features.Authentication.Commands.Login;
using InternetBank.Application.Features.Authentication.Commands.Register;
using InternetBank.Application.Features.Authentication.Queries.GetUserById;

// using InternetBank.Application.Features.Authentication.Queries.GetUserById;
using InternetBank.Contracts.Requests.Users;
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
        var command = _mapper.Map<RegisterCommand>(request);
        var result = await _sender.Send(command);
        return Created("/api/v{version:apiVersion}/[controller]/?id="+$"{result.Id}", request);
    }
    [HttpPost]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var command = _mapper.Map<LoginCommand>(request);
        var result = await _sender.Send(command);
        return Ok(result);
    }
    [HttpGet]
    public async Task<IActionResult> GetUserById(string id)
    {
        var query = new GetUserByIdQuery(id);
        var result = await _sender.Send(query);
        if (result is null)
        {
            return NotFound();
        }
        return Ok(result);
    }
}
