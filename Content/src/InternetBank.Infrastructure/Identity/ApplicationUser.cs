using System.Text.RegularExpressions;
using ErrorOr;
using InternetBank.Domain.Common.Errors;
using InternetBank.Domain.Exceptions.User;
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

    public static ErrorOr<ApplicationUser> CreateUser(string firstName,
                                             string lastName,
                                             string nationalCode,
                                             DateTime birthDate,
                                             string userName,
                                             string email,
                                             string phoneNumber)
    {
        List<Error> errors = new();

        if (!FirstNamePersianCheck(firstName))
            errors.Add(Errors.User.FirstNameIsNotFarsi);

        if (!LastNamePersianCheck(lastName))
            errors.Add(Errors.User.LastNameIsNotFarsi);

        if (!CorrectNationalCodeCheck(nationalCode))
            errors.Add(Errors.User.IncorrectNationalCode);

        if (!Above18Check(birthDate))
            errors.Add(Errors.User.Below18);

        if (errors.Any())
            return errors;

        return new ApplicationUser(firstName,
                                   lastName,
                                   nationalCode,
                                   birthDate,
                                   userName,
                                   email,
                                   phoneNumber);
    }

    private static bool Above18Check(DateTime birthDate)
    {
        if (!(DateTime.UtcNow.Year - birthDate.Year >= 18))
            return false;
        return true;
        throw new Below18();


    }

    private static bool CorrectNationalCodeCheck(string nationalCode)
    {
        if (nationalCode.Any(x => char.IsNumber(x) == false))
            return false;

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
                    return false;
                return true;
            }
            else
            {
                if (!(11 - reminder == int.Parse(nationalCode[^1].ToString())))
                    return false;
                return true;
            }
        }
        return false;
    }

    private static bool FirstNamePersianCheck(string input)
    {
        string pattern = @"^[\u0600-\u06FF\s-]+$";
        if (!Regex.IsMatch(input, pattern))
            return false;
        return true;
    }
    private static bool LastNamePersianCheck(string input)
    {
        string pattern = @"^[\u0600-\u06FF\s-]+$";
        if (!Regex.IsMatch(input, pattern))
            return false;
        return true;
    }

}