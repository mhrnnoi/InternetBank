namespace InternetBank.Domain.Exceptions.Transaction;
using InternetBank.Domain.Abstracts.Exceptions;

public sealed class InvalidDescriptionType : DomainExceptions
{
    public const string Massage = "this description type isnt valid";
    public const int StatusCodeConst = 400;
    public const string TitleConst = "Invalid Description Type";
    public InvalidDescriptionType()
: base(StatusCodeConst, Massage)
    {


    }
    public InvalidDescriptionType(string? massage)
        : base(StatusCodeConst, massage ?? Massage)
    {


    }

}

