using CareerBoostAI.Domain.CandidateContext.ValueObjects;
using CareerBoostAI.Domain.Common.Exceptions;
using CareerBoostAI.Domain.Common.ValueObjects;
using Shouldly;
using Xunit;

namespace CareerBoostAI.Tests.Unit.Domain.Candidate;

public class CandidateFactoryTest : BaseCandidateTest
{
    [Fact]
    public void Name_StaticFactory_ShouldCreateNameWithValidInput()
    {
        // Arrange
        var firstName = "John";
        var lastName = "Doe";

        // Act
        var candidateName = Name.Create(firstName, lastName);


        // Assert
        candidateName.ShouldNotBeNull();
        candidateName.FirstName.ShouldBe("John");
        candidateName.LastName.ShouldBe("Doe");
    }

    [Theory]
    [InlineData(null, "Doe", "Name.FirstName")]
    [InlineData("John", null, "Name.LastName")]
    [InlineData("", "Doe", "Name.FirstName")]
    [InlineData("John", "", "Name.LastName")]
    public void Name_StaticFactory_ShouldThrowException_WhenInvalidInputIsProvided(
        string firstName, string lastName, string expectedMessage)
    {
        // Act
        var exception = Record.Exception(() => Name.Create(firstName, lastName));

        // Assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<EmptyArgumentException>();
        exception.Message.ShouldContain(expectedMessage);
    }

    [Theory]
    [InlineData(null, typeof(EmptyArgumentException), "Email cannot be empty")]
    [InlineData("", typeof(EmptyArgumentException), "Email cannot be empty")]
    [InlineData("plainaddress", typeof(InvalidEmailFormatException),
        "Email [plainaddress] does not pass required format")]
    [InlineData("missingatsign.com", typeof(InvalidEmailFormatException),
        "Email [missingatsign.com] does not pass required format")]
    [InlineData("missingdomain@.com", typeof(InvalidEmailFormatException),
        "Email [missingdomain@.com] does not pass required format")]
    [InlineData("missingdot@domain", typeof(InvalidEmailFormatException),
        "Email [missingdot@domain] does not pass required format")]
    public void Email_Create_ShouldThrowException_ForInvalidEmails(
        string emailInput, Type expectedExceptionType, string expectedMessage)
    {
        // Act
        var exception = Record.Exception(() => Email.Create(emailInput));

        // Assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType(expectedExceptionType);
        exception.Message.ShouldContain(expectedMessage);
    }

    [Theory]
    [InlineData("john.doe@example.com")]
    [InlineData("user123@domain.co.uk")]
    [InlineData("name.surname@sub.domain.org")]
    public void Email_Create_ShouldReturnValidEmail_ForValidInput(string emailInput)
    {
        // Act
        var email = Email.Create(emailInput);

        // Assert
        email.ShouldNotBeNull();
        email.Value.ShouldBe(emailInput);
    }


    [Fact]
    public void Create_ShouldReturnValidCandidate_WhenPassedCorrectValues()
    {
        // Arrange
        var factory = GetCandidateFactory();
        var firstName = "John";
        var lastName = "Doe";
        var email = "john.doe@example.com";
        var dateOfBirth = DateOnly.FromDateTime(new DateTime(1990, 1, 1));
        var phoneCode = "+44";
        var phoneNumber = "1234567890";

        // Act
        var candidate = factory.Create(
            firstName, lastName, dateOfBirth, email, phoneCode, phoneNumber);

        // Assert
        candidate.ShouldNotBeNull();
        candidate.Name.FirstName.ShouldBe(firstName);
        candidate.Name.LastName.ShouldBe(lastName);
        candidate.Email.Value.ShouldBe(email);
        candidate.DateOfBirth.Value.ShouldBe(dateOfBirth);
        candidate.PhoneNumber.Code.ShouldBe(phoneCode);
        candidate.PhoneNumber.Number.ShouldBe(phoneNumber);
    }
}