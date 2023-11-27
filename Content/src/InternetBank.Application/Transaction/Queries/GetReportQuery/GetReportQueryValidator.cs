using FluentValidation;

namespace InternetBank.Application.Transactions.Queries.GetReportQuery;
public class GetReportQueryValidator : AbstractValidator<GetReportQuery>
{
    public GetReportQueryValidator()
    {
        RuleFor(x => x.From).Must((GetReportQuery, from, context) => IsNullOrValidFrom(from) == true)
                            .WithMessage("to should be after the from");

        RuleFor(x => x.To).Must((GetReportQuery, to, context) => IsNullOrValidTo(to, GetReportQuery.From) == true)
                            .WithMessage("to should be after the from");
        RuleFor(x => x.SourceCardNumber)
.Must(x => x.Where(x => char.IsDigit(x) == true).Count() == 16)
.WithMessage("card number should have 16 numeric character");
    }
    private static bool IsNullOrValidFrom(DateOnly? from)
    {
        return from is null || from <= DateOnly.FromDateTime(DateTime.UtcNow);
    }
    private static bool IsNullOrValidTo(DateOnly? to, DateOnly? from)
    {
        return to is null || to >= from;
    }
}
