using System.Text.RegularExpressions;
using InternetBank.Domain.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace InternetBank.Infrastructure.Identity;

public sealed class ApplicationUser : IdentityUser
{
    public string FirstName { get; private init; }
    public string LastName { get; private init; }
    public string NationalCode { get; private init; }
    public DateTime BirthDate { get; private init; }

    private ApplicationUser(string firstName,
                            string lastName,
                            string nationalCode,
                            DateTime birthDate,
                            string userName,
                            string email,
                            string phoneNumber)
                            : base()
    {
        FirstName = firstName;
        LastName = lastName;
        NationalCode = nationalCode;
        BirthDate = birthDate;
        UserName = userName;
        Email = email;
        PhoneNumber = phoneNumber;
    }

    public static ApplicationUser CreateUser(string firstName,
                                             string lastName,
                                             string nationalCode,
                                             DateOnly birthDate,
                                             string userName,
                                             string email,
                                             string phoneNumber)
    {
        FirstNamePersianCheck(firstName);
        LastNamePersianCheck(lastName);
        CorrectNationalCodeCheck(nationalCode);
        BirthDateCheck(birthDate);

        return new ApplicationUser(firstName,
                                   lastName,
                                   nationalCode,
                                   birthDate.ToDateTime(default),
                                   userName,
                                   email,
                                   phoneNumber);
    }

    private static void BirthDateCheck(DateOnly birthDate)
    {
        if (!(DateTime.UtcNow.Year - birthDate.Year >= 18))
            throw new DomainExceptions.User.Below18();


    }

    private static void CorrectNationalCodeCheck(string nationalCode)
    {
        if (nationalCode.Any(x => char.IsNumber(x) == false))
            throw new DomainExceptions.User.IncorrectNationalCode();

        if (nationalCode.Length >= 8 && nationalCode.Length <= 10)
        {
            for (int i = 0; i < 10 - nationalCode.Length; i++)
            {
                nationalCode = nationalCode.Insert(0, "0");
            }
            int j = 10;
            double sum = 0;
            for (int i = 0; i < 9; i++)
            {
                sum += int.Parse(nationalCode[i].ToString()) * j;
                j--;
            }
            var reminder = sum % 11;
            if (reminder < 2)
            {
                if (!(reminder == int.Parse(nationalCode[^1].ToString())))
                    throw new DomainExceptions.User.IncorrectNationalCode();
            }
            else
            {
                if (!(11 - reminder == int.Parse(nationalCode[^1].ToString())))
                    throw new DomainExceptions.User.IncorrectNationalCode();
            }
        }
        else
            throw new DomainExceptions.User.IncorrectNationalCode();





    }

    private static void FirstNamePersianCheck(string input)
    {
        string pattern = @"^[\u0600-\u06FF\s-]+$";
        if (!Regex.IsMatch(input, pattern))
            throw new DomainExceptions.User.FirstNameIsNotFarsi();
    }
    private static void LastNamePersianCheck(string input)
    {
        string pattern = @"^[\u0600-\u06FF\s-]+$";
        if (!Regex.IsMatch(input, pattern))
            throw new DomainExceptions.User.LastNameIsNotFarsi();
    }

}