using Asp.Versioning;
using InternetBank.Application.Authentication.Commands.Login;
using InternetBank.Application.Authentication.Commands.Register;
using InternetBank.Api.Requests.Users;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using InternetBank.Application.Authentication.Queries.GetUser;
using InternetBank.Application.Authentication.Queries.GetUsers;
namespace InternetBank.Api.Controllers;

[ApiVersion("1.0")]
[Route("/api/v{version:apiVersion}/user")]
public class UserController : ApiController
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public UserController(ISender sender, IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
    }

    [HttpPost("/api/v{version:apiVersion}/user/register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var command = _mapper.Map<RegisterCommand>(request);
        var result = await _sender.Send(command);
        var apiVersion = GetApiVersion(HttpContext);
        return Created($"/api/v{apiVersion}/user/{result.Id}", result);
    }
    [HttpPost("/api/v{version:apiVersion}/user/login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var command = _mapper.Map<LoginCommand>(request);
        var result = await _sender.Send(command);
        return Ok(result);
    }
    [HttpGet("/api/v{version:apiVersion}/user/{id}")]
    public async Task<IActionResult> GetUserById(string id)
    {
        var query = new GetUserQuery(id);
        var result = await _sender.Send(query);
        return Ok(result);
    }
    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        var query = new GetUsersQuery();
        var result = await _sender.Send(query);
        return Ok(result);
    }

}
