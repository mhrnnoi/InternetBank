using System;
using FluentAssertions;
using InternetBank.Domain.Accounts;
using InternetBank.Domain.Accounts.Enums;

namespace InternetBank.Domain.Tests.Accounts;

public class AccountTypesTests
{
    [Fact]
    public void Handle_Should_Return_SavingEnum_When_ParseNumber1()
    {
        // //Arrange
        // var saving = (AccountTypes)Enum.Parse(typeof(AccountTypes), "1");
        // //Act
        // var IsSame = saving == AccountTypes.Saving;
        // //Assert
        // IsSame.Should().Be(true);

    }
    [Fact]
    public void Handle_Should_Return_CheckingEnum_When_ParseNumber2()
    {
        // //Arrange
        // var saving = (AccountTypes)Enum.Parse(typeof(AccountTypes), "2");
        // //Act
        // var IsSame = saving == AccountTypes.Checking;
        // //Assert
        // IsSame.Should().Be(true);
    }
}