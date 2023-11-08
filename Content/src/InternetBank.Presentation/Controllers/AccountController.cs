using Asp.Versioning;
using InternetBank.Application.Accounts.Commands.BlockAccount;
using InternetBank.Application.Accounts.Commands.ChangeAccountPassword;
using InternetBank.Application.Accounts.Commands.CreateAccount;
using InternetBank.Application.Accounts.Commands.UnBlockAccount;
using InternetBank.Application.Accounts.Queries.GetAccountBalanceById;
using InternetBank.Application.Accounts.Queries.GetAccountById;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using InternetBank.Application.Accounts.Queries.GetAllAccounts;
using InternetBank.Contracts.Requests.Accounts;

namespace InternetBank.Presentation.Controllers;

[ApiVersion("1.0")]
[Route("/api/v{version:apiVersion}/account")]
[Authorize]
public class AccountController : ApiController
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public AccountController(ISender sender, IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAccount(CreateAccountRequest request)
    {
        var userId = GetUserId(User.Claims);
        var command = _mapper.Map<CreateAccountCommand>(request);
        command = command with {UserId = userId};
        var result = await _sender.Send(command);
        int? apiVersion = GetApiVersion(HttpContext);
        return Created($"/api/v{apiVersion}/account" + $"/{result.Id}", result);
    }

    [HttpPost("/api/v{version:apiVersion}/account/change-password")]
    public async Task<IActionResult> ChangeAccountPassword(ChangeAccountPasswordRequest request)
    {
        var command = _mapper.Map<ChangeAccountPasswordCommand>(request);
        var userId = GetUserId(User.Claims);
        command = command with {UserId = userId};
        var result = await _sender.Send(command);
        return Ok(result);
    }

    [HttpPost("/api/v{version:apiVersion}/account/block/{id}")]
    public async Task<IActionResult> BlockAccountById(string id)
    {
        var userId =  GetUserId(User.Claims);
        var command = new BlockAccountCommand(id, userId);
        var result = await _sender.Send(command);
        return Ok(result);
    }
    [HttpPost("/api/v{version:apiVersion}/account/unblock/{id}")]
    public async Task<IActionResult> UnBlockAccountById(string id)
    {
        var userId =  GetUserId(User.Claims);
        var command = new UnBlockAccountCommand(id, userId);
        var result = await _sender.Send(command);
        return Ok(result);
    }
    [HttpGet("/api/v{version:apiVersion}/account/balance/{id}")]
    public async Task<IActionResult> GetBalance(string id)
    {
        var userId = GetUserId(User.Claims);
        var query = new GetAccountBalanceByIdQuery(id,
                                                   userId);
        var result = await _sender.Send(query);
        return Ok(result);
    }
    [HttpGet("/api/v{version:apiVersion}/account/{id}")]
    public async Task<IActionResult> GetAccount(string id)
    {
        var userId = GetUserId(User.Claims);
        var query = new GetAccountByIdQuery(id,
                                            userId);
        var result = await _sender.Send(query);
        return Ok(result);
    }
    [HttpGet]
    public async Task<IActionResult> GetAllAccounts()
    {
        var userId = GetUserId(User.Claims);
        var query = new GetAllAccountsQuery(userId);
        var result = await _sender.Send(query);
        return Ok(result);
    }

}
