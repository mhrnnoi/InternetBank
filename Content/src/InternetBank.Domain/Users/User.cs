using System.Text.RegularExpressions;
using InternetBank.Domain.Abstracts.Entity;

namespace InternetBank.Domain.Users;

public class ApplicationUser : Entity
{
    public string IdentityUserId { get; init; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string NationalCode { get; init; }
    public DateTime BirthDate { get; init; }

    private ApplicationUser(string firstName,
                            string lastName,
                            string nationalCode,
                            DateTime birthDate,
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
                                             DateTime birthDate,
                                             string identityUserId)
    {

        if (IsPersian(firstName))
        {
            if (IsPersian(lastName))
            {
                if (IsCorrectNationalCode(nationalCode))
                {
                    if (DateTime.UtcNow.Year - birthDate.Year >= 18)
                    {
                        return new ApplicationUser(firstName, lastName, nationalCode, birthDate, identityUserId);
                    }
                }
            }
        }
        throw new Exception("something wrong when creating user");
    }
    public static bool IsCorrectPhoneNumber(string input)
    {
        Regex pattern = new Regex(@"^((0?9)|(\+?989)|(00989))((14)|(13)|(12)|(19)|(18)|(17)|(15)|(16)|(11)|(10)|(90)|(91)|(92)|(93)|(94)|(95)|(96)|(32)|(30)|(33)|(35)|(36)|(37)|(38)|(39)|(00)|(01)|(02)|(03)|(04)|(05)|(41)|(20)|(21)|(22)|(23)|(31)|(34)|(9910)|(9911)|(9913)|(9914)|(9999)|(999)|(990)|(9810)|(9811)|(9812)|(9813)|(9814)|(9815)|(9816)|(9817)|(998))\W?\d{3}\W?\d{4}$");
        return pattern.IsMatch(input);
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