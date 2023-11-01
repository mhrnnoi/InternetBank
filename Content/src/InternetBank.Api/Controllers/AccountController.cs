using Asp.Versioning;
using InternetBank.Application.Account.Commands.BlockAccount;
using InternetBank.Application.Account.Commands.ChangeAccountPassword;
using InternetBank.Application.Account.Commands.CreateAccount;
using InternetBank.Application.Account.Commands.UnBlockAccount;
using InternetBank.Application.Account.Queries.GetAccountBalanceById;
using InternetBank.Application.Account.Queries.GetById;
using InternetBank.Api.Requests.Accounts;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InternetBank.Api.Controllers;

[ApiVersion("1.0")]
[Route("/api/v{version:apiVersion}/account/")]
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
    [Route("/api/v{version:apiVersion}/account")]
    [Authorize]
    public async Task<IActionResult> CreateAccount(CreateAccountRequest request)
    {
        var command = _mapper.Map<CreateAccountCommand>(request);
        var result = await _sender.Send(command);
        var apiVersion = HttpContext.GetRequestedApiVersion()?.MajorVersion;
        return Created($"/api/v{apiVersion}/account" + $"/{result.Id}", result);
    }
    [HttpPost("change-password")]
    [Authorize]
    public async Task<IActionResult> ChangeAccountPassword(ChangeAccountPasswordRequest request)
    {
        var command = _mapper.Map<ChangeAccountPasswordCommand>(request);
        var result = await _sender.Send(command);
        return Ok("");
    }
    [HttpPost("block/{id}")]
    [Authorize]
    public async Task<IActionResult> BlockAccountById(int id)
    {
        var command = _mapper.Map<BlockAccountCommand>(id);
        var result = await _sender.Send(command);
        return Ok("");
    }
    [HttpPost("unblock/{id}")]
    [Authorize]
    public async Task<IActionResult> UnBlockAccountById(int id)
    {
        var command = _mapper.Map<UnBlockAccountCommand>(id);
        var result = await _sender.Send(command);
        return Ok("");
    }
    [HttpGet("balance/{id}")]
    [Authorize]
    public async Task<IActionResult> GetBalance(int id)
    {
        var query = _mapper.Map<GetAccountBalanceByIdQuery>(id);
        var result = await _sender.Send(query);
        return Ok("");
    }
    [HttpGet]
    [Route("/api/v{version:apiVersion}/accounts/{id}")]
    public async Task<IActionResult> GetAccount(int id)
    {
        var command = _mapper.Map<GetAccountByIdQuery>(id);
        var result = await _sender.Send(command);
        return Ok(result);
    }

    //GetAllAccounts


}
