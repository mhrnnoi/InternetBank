namespace InternetBank.Domain.Exceptions.User;

public class AlreadyExistNationalCode : DomainExceptions
{
    public const string Massage = "a user with this national code is already exist";
    public const int StatusCodeConst = 409;

    public AlreadyExistNationalCode()
: base(StatusCodeConst, Massage)
    {


    }
    public AlreadyExistNationalCode(string? massage)
        : base(StatusCodeConst, massage ?? Massage)
    {


    }

}

