using System.Security.Cryptography.X509Certificates;
using InternetBank.Application.Accounts.Queries.GetAccountById;
using InternetBank.Domain.Accounts.Entities;
using Mapster;

namespace InternetBank.Presentation.Mapping;

public class MappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Account, AccountDTO>()
        .Map(dest => dest.AccountType, source => source.AccountType.ToString())
        .Map(dest => dest.AccountNumber, source => source.AccountNumber.Value)
        .Map(dest => dest.CardNumber, source => source.CardNumber.Value)
        .Map(dest => dest.Cvv2, source => source.Cvv2.Value)
        .Map(dest => dest.ExpiryMonth, source => source.ExpiryDate.Month.ToString())
        .Map(dest => dest.ExpiryYear, source => source.ExpiryDate.Year.ToString())
        .Map(dest => dest.Id, source => source.Id.Value)
        .Map(dest => dest.StaticPassword, source => source.StaticPassword.Value);
    }
}