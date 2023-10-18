using Asp.Versioning;
using InternetBank.Application.Authentication.Commands.Login;
using InternetBank.Application.Authentication.Commands.Register;
using InternetBank.Api.Requests.Users;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InternetBank.Api.Controllers;

[ApiVersion("1.0")]
[Route("/api/v1/user")]
public class UserController : ApiController
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public UserController(ISender sender, IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
    }

    [HttpPost("/register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var command = _mapper.Map<RegisterCommand>(request);
        var result = await _sender.Send(command);
        return Created($"/api/v1/users/{result.Id}", result);
    }
    [HttpPost("/login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var command = _mapper.Map<LoginCommand>(request);
        var result = await _sender.Send(command);
        if (result is null)
        {
            return Unauthorized("invalid cred");
        }
        return Ok(result);
    }
    [HttpGet]
    [Route("/api/v1/users/{id}")]
    public async Task<IActionResult> GetUser(string id)
    {
        var query = new GetUserQuery(id);
        var result = await _sender.Send(query);
        if (result is null)
        {
            return NotFound("there is no user with this id");
        }
        return Ok(result);
    }
    [HttpGet]
    [Route("/api/v1/users")]
    public async Task<IActionResult> GetUsers()
    {
        var query = new GetUsersQuery();
        var result = await _sender.Send(query);
        if (result is null)
        {
            return NotFound("there is no user");
        }
        return Ok(result);
    }
}
