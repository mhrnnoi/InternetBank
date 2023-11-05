using Asp.Versioning;
using InternetBank.Api.Requests.Transactions;
using InternetBank.Application.Transaction.Commands.Send_OTP;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InternetBank.Api.Controllers;

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
        var command = new Send_OTPCommand(request.CardNumber,
                                          request.CVV2,
                                          request.ExpiryDate,
                                          request.Amount,
                                          request.DestinationCardNumber,
                                          userId);
        var result = await _sender.Send(command);
        var apiVersion = GetApiVersion(HttpContext);
        return Created($"/api/v{apiVersion}/transaction/{result}", result);

    }
    [HttpPost("/api/v{version:apiVersion}/transaction/send-otp")]
    public async Task<IActionResult> TransferMoney(TransferMoneyRequest request)
    {
        var userId = GetUserId(User.Claims);
        var command = new TransferMoneyCommand(request.CardNumber,
                                          request.CVV2,
                                          request.ExpiryDate,
                                          request.Amount,
                                          request.DestinationCardNumber,
                                          userId);
        var result = await _sender.Send(command);
        var apiVersion = GetApiVersion(HttpContext);
        return Created($"/api/v{apiVersion}/transaction/{result}", result);

    }
    // [HttpGet("/api/v{version:apiVersion}/transaction/{id}")]
    // public async Task<IActionResult> Get(int id)
    // {
    //     var query = new GetTransactionByIdQuery(id);
    //     var result = await _sender.Send(query);
    //     return Ok(result);

    // }


}

public record OTPRequest(string CardNumber,
                         string CVV2,
                         DateTime ExpiryDate,
                         double Amount,
                         string DestinationCardNumber);