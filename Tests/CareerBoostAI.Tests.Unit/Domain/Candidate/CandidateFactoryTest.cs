using CareerBoostAI.Domain.CandidateContext.Factories;
using Xunit;
using Shouldly;

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