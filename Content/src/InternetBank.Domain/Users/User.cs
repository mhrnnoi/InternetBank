using System.Text.RegularExpressions;
using InternetBank.Domain.Abstracts.Entity;

namespace InternetBank.Domain.Users;

public class ApplicationUser : Entity
{
    public string IdentityUserId { get; init; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string NationalCode { get; init; }
    public string BirthDate { get; init; }

    private ApplicationUser(string firstName,
                            string lastName,
                            string nationalCode,
                            string birthDate,
                            string identityUserId)
    {
        FirstName = firstName;
        LastName = lastName;
        NationalCode = nationalCode;
        BirthDate = birthDate;
        IdentityUserId = identityUserId;
    }

    public static ApplicationUser CreateUser(string firstName,
                                             string lastName,
                                             string nationalCode,
                                             string birthDate,
                                             string identityUserId)
    {

        if (IsPersian(firstName))
        {
            if (IsPersian(lastName))
            {
                if (IsCorrectNationalCode(nationalCode))
                {

                }
            }
        }
        return new ApplicationUser(firstName,
                                   lastName,
                                   nationalCode,
                                   birthDate,
                                   identityUserId);
    }

    private static bool IsCorrectNationalCode(string nationalCode)
    {
        if (nationalCode.Any(x => char.IsNumber(x) == false))
        {
            return false;
        }
        if (nationalCode.Length >= 8 && nationalCode.Length <= 10)
        {
            for (int i = 0; i < 10 - nationalCode.Length; i++)
            {
                nationalCode.Insert(0, "0");
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
                return reminder == int.Parse(nationalCode.Last().ToString());
            }
            else
            {
                return 11 - reminder == int.Parse(nationalCode.Last().ToString());
            }
        }
        return false;


    }

    private static bool IsPersian(string input)
    {
        string pattern = @"^[\u0600-\u06FF\s-]+$";
        return Regex.IsMatch(input, pattern);
    }

}