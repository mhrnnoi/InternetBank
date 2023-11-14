namespace InternetBank.Domain.Exceptions.User;
using InternetBank.Domain.Abstracts.Exceptions;

public sealed class AlreadyExistPhoneNumber : DomainExceptions
{
    public const string Massage = "a user with this Phone number is already exist";
    public const int StatusCodeConst = 409;

    public AlreadyExistPhoneNumber()
: base(StatusCodeConst, Massage)
    {


    }
    public AlreadyExistPhoneNumber(string? massage)
        : base(StatusCodeConst, massage ?? Massage)
    {


    }

}
