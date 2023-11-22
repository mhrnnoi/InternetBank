namespace InternetBank.Domain.Accounts.ValueObjects;

using System;
using System.Transactions;
using InternetBank.Domain.Abstracts.Primitives;
using InternetBank.Domain.Accounts.Enums;
using InternetBank.Domain.Exceptions.Transaction;

public class Description : ValueObject
{
    public string Value { get; set; }

    private Description(string value)
    {
        Value = value;
    }

    public static Description GenerateDescription(DescriptionTypes descriptionTypes)
    {
        return descriptionTypes switch
        {
            DescriptionTypes.PendingToPaid => new Description("در نتظار پرداخت"),
            DescriptionTypes.BlockedDestinationAccount => new Description("عملیات ناموفق - اکانت مقصد مسدود هست"),
            DescriptionTypes.BlockedSourceAccount => new Description("عملیات ناموفق - اکانت مبدا مسدود هست"),
            DescriptionTypes.Success => new Description("عملیات موفق"),
            DescriptionTypes.LowBalance => new Description("عملیات ناموفق - عدم موجودی"),
            DescriptionTypes.IncorrectPass => new Description("عملیات ناموفق - رمز نادرست"),
            _ => throw new InvalidDescriptionType(),
        };
    }


    public override IEnumerable<object> GetAtomicValue()
    {
        yield return Value;
    }

    public static Description Create(string value)
    {
        return new Description(value);
    }
}