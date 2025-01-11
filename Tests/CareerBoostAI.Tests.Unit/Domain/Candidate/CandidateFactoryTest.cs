using CareerBoostAI.Domain.Candidate;
using CareerBoostAI.Domain.Candidate.Cv;
using CareerBoostAI.Domain.Candidate.ValueObjects;
using CareerBoostAI.Domain.Common.ValueObjects;
using Xunit;
using Shouldly;

namespace CareerBoostAI.Tests.Unit.Domain.Candidate;

public class CandidateFactoryTest : BaseCandidateTest
{
    [Fact]
    public void CandidateFactory_CreatesNewCandidate_When_Valid_Data_IsGiven()
    {
        // Arrange
        var factory = GetCandidateFactory();
        var candidateId = CandidateId.New();
        var firstName = FirstName.Create("John");
        var lastName = LastName.Create("Doe");
        var dateOfBirth = DateOfBirth.Create(new DateOnly(1998, 12, 13));
        var email = Email.Create("john@doe.com");
        var phoneNumber = PhoneNumber.Create("+44", "812345678");
        var cvs = Enumerable.Empty<Cv>();
            
        // Act   
        var exception = Record.Exception(() =>
        {
            factory.Create(
                candidateId, firstName, lastName, 
                dateOfBirth, email, phoneNumber, cvs
            );
        });
        var candidate = factory.Create(
            candidateId, firstName, lastName, 
            dateOfBirth, email, phoneNumber, cvs
        );
        
        // Assert
        exception.ShouldBeNull();
        candidate.ShouldNotBeNull();
        candidate.FirstName.Value.ShouldBe("John");
        candidate.LastName.Value.ShouldBe("Doe");
        candidate.DateOfBirth.Value.ShouldBe(new DateOnly(1998, 12, 13));
        candidate.Email.Value.ShouldBe("john@doe.com");
        candidate.PhoneNumber.Code.ShouldBe("+44");
        candidate.PhoneNumber.Number.ShouldBe("812345678");
        candidate.Cvs.ShouldBeEmpty();
    }
    
}