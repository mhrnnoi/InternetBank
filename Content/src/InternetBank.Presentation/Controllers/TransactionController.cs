using Asp.Versioning;
using InternetBank.Application.Transaction.Commands.Send_OTP;
using InternetBank.Application.Transaction.Commands.Transfer_Money;
using InternetBank.Application.Transaction.Queries.GetReportQuery;
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
        command = command with {UserId = userId};
        var result = await _sender.Send(command);
        return Ok(result);

    }
    [HttpPost("/api/v{version:apiVersion}/transaction/transfer-money")]
    public async Task<IActionResult> TransferMoney(TransferMoneyRequest request)
    {
        var userId = GetUserId(User.Claims);
        var command = new TransferMoneyCommand(request.Otp,
                                               request.Amount,
                                               userId);
        var result = await _sender.Send(command);
        return Ok(result);

    }

    [HttpGet("/api/v{version:apiVersion}/transaction/report")]
    [AllowAnonymous]
    public async Task<IActionResult> ReportAsync(DateOnly? from,
                                                 DateOnly? to,
                                                 bool? isSuccess)
    {
        var query = new GetReportQuery(from,
                                       to,
                                       isSuccess);
        var result = await _sender.Send(query);
        return Ok(result);

    }



}

