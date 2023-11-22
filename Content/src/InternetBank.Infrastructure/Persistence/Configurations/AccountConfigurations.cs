using InternetBank.Domain.Accounts.Entities;
using InternetBank.Domain.Accounts.Enums;
using InternetBank.Domain.Accounts.ValueObjects;
using InternetBank.Domain.Transactions.Enums;
using InternetBank.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InternetBank.Infrastructure.Persistence.Configurations;

public class AccountConfigurations : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        ConfigureAccountTable(builder);
        ConfigureTransactionsTable(builder);
    }

    private void ConfigureTransactionsTable(EntityTypeBuilder<Account> builder)
    {
        builder.OwnsMany(a => a.Transactions, tb =>
        {
            tb.ToTable("Transactions");
            tb.WithOwner().HasForeignKey("AccountId");

            tb.HasKey("Id", "AccountId");

            tb.Property(t => t.Id)
                .HasColumnName("TransactionId")
                 .ValueGeneratedNever()
                    .HasConversion(id => id.Value,
                       value => TransactionId.Create(value));

            tb.Property(t => t.Otp)
           .HasConversion(
               id => id.Value,
               value => Otp.Create(value)
           );
            tb.Property(t => t.DestinationCardNumber)
            .HasConversion(
                id => id.Value,
                value => CardNumber.Create(value)
            );
            tb.Property(t => t.Description)
            .HasConversion(
                id => id.Value,
                value => Description.Create(value)
            );
            tb.Property(t => t.Status)
            .HasConversion(
                type => nameof(type),
                value => (Status)Enum.Parse(typeof(Status), value)
            );

        });
    }

    private void ConfigureAccountTable(EntityTypeBuilder<Account> builder)
    {
        builder.ToTable("Accounts");
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Id)
        .ValueGeneratedNever()
        .HasConversion(
            id => id.Value,
            value => AccountId.Create(value)
        );
        builder.Property(a => a.AccountNumber)
            .HasConversion(
                id => id.Value,
                value => AccountNumber.Create(value)
            );
        builder.Property(a => a.AccountType)
            .HasConversion(
                type => type.ToString(),
                value => (AccountTypes)Enum.Parse(typeof(AccountTypes), value)
            );
        builder.Property(a => a.CardNumber)
            .HasConversion(
                id => id.Value,
                value => CardNumber.Create(value)
            );
        builder.Property(a => a.Cvv2)
            .HasConversion(
                id => id.Value,
                value => Cvv2.Create(value)
            );
        builder.Property(a => a.StaticPassword)
            .HasConversion(
                id => id.Value,
                value => Password.Create(value)
            );

        // builder.Metadata.FindNavigation(nameof(Account.Transactions))!
        //     .SetPropertyAccessMode(PropertyAccessMode.Field);

    }
}