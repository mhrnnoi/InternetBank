using Asp.Versioning;
using InternetBank.Application.Transactions.Commands.Send_OTP;
using InternetBank.Application.Transactions.Commands.Transfer_Money;
using InternetBank.Application.Transactions.Queries.GetReportQuery;
using InternetBank.Contracts.Requests.Transactions;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InternetBank.Presentation.Controllers;

[ApiVersion("1.0")]
[Route("/api/v{version:apiVersion}/transaction")]
[Authorize]
public class TransactionController : ApiController
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public TransactionController(ISender sender, IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
    }

    [HttpPost("/api/v{version:apiVersion}/transaction/send-otp")]
    public async Task<IActionResult> SendOTP(OTPRequest request)
    {
        var userId = GetUserId(User.Claims);
        var command = _mapper.Map<Send_OTPCommand>(request);
        command = command with { UserId = userId };
        var result = await _sender.Send(command);
        return result.Match(value => Ok(value), Problem);

    }
    [HttpPost("/api/v{version:apiVersion}/transaction/transfer-money")]
    public async Task<IActionResult> TransferMoney(TransferMoneyRequest request)
    {
        var userId = GetUserId(User.Claims);
        var command = _mapper.Map<TransferMoneyCommand>(request);
        command = command with { UserId = userId };
        var result = await _sender.Send(command);
        return result.Match(Ok, Problem);

    }

    [HttpGet("/api/v{version:apiVersion}/transaction/report")]
    public async Task<IActionResult> ReportAsync(string SourceCardNumber,
                                                 DateOnly? From,
                                                 DateOnly? To,
                                                 bool? IsSuccess)
    {
        var userId = GetUserId(User.Claims);

        var query = new GetReportQuery(SourceCardNumber,
                                       From,
                                       To,
                                       IsSuccess,
                                       userId);
                                       
        var result = await _sender.Send(query);
        return result.Match(Ok, Problem);


    }



}

