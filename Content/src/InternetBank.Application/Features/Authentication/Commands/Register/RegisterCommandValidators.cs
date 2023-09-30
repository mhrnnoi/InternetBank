using FluentValidation;

namespace InternetBank.Application.Features.Authentication.Commands.Register;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(x => x.Email).EmailAddress().WithMessage("its not correct email address");
        RuleFor(x => x.PhoneNumber).Matches(@"^((0?9)|(\+?989)|(00989))((14)|(13)|(12)|(19)|(18)|(17)|(15)|(16)|(11)|(10)|(90)|(91)|(92)|(93)|(94)|(95)|(96)|(32)|(30)|(33)|(35)|(36)|(37)|(38)|(39)|(00)|(01)|(02)|(03)|(04)|(05)|(41)|(20)|(21)|(22)|(23)|(31)|(34)|(9910)|(9911)|(9913)|(9914)|(9999)|(999)|(990)|(9810)|(9811)|(9812)|(9813)|(9814)|(9815)|(9816)|(9817)|(998))\W?\d{3}\W?\d{4}$")
                                    .WithMessage("its not correct iranian phone number");
    }
}
