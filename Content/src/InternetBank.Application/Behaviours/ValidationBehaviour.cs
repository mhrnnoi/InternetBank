using System.Windows.Input;
using FluentValidation;
using MediatR;

namespace InternetBank.Application.Behaviours;

public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;
    public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var context = new ValidationContext<TRequest>(request);
        var failures = _validators
                .Select(v => v.Validate(context))
                .SelectMany(res => res.Errors)
                .Where(error => error != null)
                .ToList();

        if (failures.Any())
        {
            throw new ValidationException(failures);
            
        }
        return await next();
    }
}