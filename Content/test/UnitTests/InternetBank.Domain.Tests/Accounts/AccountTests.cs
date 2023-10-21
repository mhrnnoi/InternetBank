using FluentAssertions;
using InternetBank.Domain.Accounts;

namespace InternetBank.Domain.Tests.Accounts;

public class AccountTests
{
    [Fact]
    public void Handle_Should_Return_AccountWithDesiredInformation_When_InformationIsCorrect()
    {
        //Arrange
        var account = Account.OpenAccount(1, 50000, "32");
        //Act
        account.BlockAccount();
        //Assert
        account.IsBlocked.Should().Be(true);

    }
}