using CareerBoostAI.Domain.CandidateContext.ValueObjects;
using CareerBoostAI.Domain.Common.Exceptions;
using CareerBoostAI.Domain.Common.ValueObjects;

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
    [InlineData("2000-01-01", "2025-01-01")] 
    [InlineData("2010-12-31", "2025-01-01")] 
    [InlineData("1905-06-15", "2025-01-01")] 
    public void DateOfBirth_Create_ShouldReturnValidDateOfBirth_WhenAgeIsValid(
        string birthDateString, string todayString)
    {
        // Arrange
        var birthDate = DateOnly.Parse(birthDateString);
        var dateTimeProvider = TestDateTimeProvider.FromDateString(todayString);

        // Act
        var dob = DateOfBirth.Create(birthDate, dateTimeProvider);

        // Assert
        dob.ShouldNotBeNull();
        dob.Value.ShouldBe(birthDate);
    }
    
    [Theory]
    [InlineData("2020-01-01", "2025-01-01")] // Age 5, invalid
    [InlineData("1800-01-01", "2025-01-01")] // Too old, invalid
    public void DateOfBirth_Create_ShouldThrowException_WhenAgeIsLessThan10OrGreaterThan120(string birthDateString, string todayString)
    {
        // Arrange
         
        
        var birthDate = DateOnly.Parse(birthDateString);
        var dateTimeProvider = TestDateTimeProvider.FromDateString(todayString);

        // Act
        var exception = Record.Exception(() => DateOfBirth.Create(birthDate, dateTimeProvider));

        // Assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<AgeNotWithinAcceptedRangeException>();
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
    
    [Theory]
    [InlineData(null, "1234567890", typeof(EmptyArgumentException), "PhoneNumber.Code")]
    [InlineData("", "1234567890", typeof(EmptyArgumentException), "PhoneNumber.Code")]
    [InlineData("+44", null, typeof(EmptyArgumentException), "PhoneNumber.Number")]
    [InlineData("+44", "", typeof(EmptyArgumentException), "PhoneNumber.Number")]
    [InlineData("+44", "abc123", typeof(InvalidPhoneNumberException), "Invalid phone number: +44 - abc123")]
    [InlineData("+1", "123", typeof(InvalidPhoneNumberException), "Invalid phone number: +1 - 123")]
    public void PhoneNumber_Create_ShouldThrowException_ForInvalidInputs(string code, string number, Type exceptionType, string expectedMessage)
    {
        // Act
        var exception = Record.Exception(() => PhoneNumber.Create(code, number));

        // Assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType(exceptionType);
        exception.Message.ShouldContain(expectedMessage);
    }
    
    [Theory]
    [InlineData("+44", "1234567890")]
    [InlineData("+1", "9876543210")]
    [InlineData("+91", "9988776655")]
    public void PhoneNumber_Create_ShouldReturnValidPhoneNumber_ForValidInputs(string code, string number)
    {
        // Act
        var phoneNumber = PhoneNumber.Create(code, number);

        // Assert
        phoneNumber.ShouldNotBeNull();
        phoneNumber.Code.ShouldBe(code);
        phoneNumber.Number.ShouldBe(number);
    }


    [Theory]
    [InlineData("John", "Doe", "john.doe@example.com", "1990-01-01", "+44", "1234567890")]
    [InlineData("Jane", "Smith", "jane.smith@example.com", "1985-06-15", "+1", "9876543210")]
    [InlineData("Alex", "Johnson", "alex.johnson@example.com", "2000-11-23", "+91", "5555555555")]
    public void Create_ShouldReturnValidCandidate_WhenPassedCorrectValues(
        string firstName, 
        string lastName, 
        string email, 
        string dateOfBirthString, 
        string phoneCode, 
        string phoneNumber)
    {
        // Arrange
        var factory = GetCandidateFactory();
        var dateOfBirth = DateOnly.Parse(dateOfBirthString);

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