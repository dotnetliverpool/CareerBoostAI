using CareerBoostAI.Domain.Candidate.Cv.ValueObjects;
using CareerBoostAI.Domain.Candidate.CvEntity;
using CareerBoostAI.Domain.Candidate.CvEntity.ValueObjects;
using Xunit;
using Shouldly;

namespace CareerBoostAI.Tests.Unit.Domain.Candidate;

public class CandidateFactoryTest : BaseCandidateTest
{
    [Fact]
    public void Create_ShouldCreateValidCandidateAggregate()
    {
        // Arrange
        var id = Guid.NewGuid();
        var firstName = "John";
        var lastName = "Doe";
        var dateOfBirth = new DateOnly(1990, 5, 15);
        var email = "john.doe@example.com";
        var phoneCode = "+1";
        var phoneNumber = "1234567890";

        // Act
        
        
        // Assert
        
    }
    
    

    [Theory]
    [InlineData(10)] // Below minimum age limit
    [InlineData(121)] // Above maximum age limit
    public void Create_ShouldThrowNotAcceptedWithinAgeLimitException_WhenAgeIsOutOfRange(int age)
    {
        // Arrange
        var id = Guid.NewGuid();
        var firstName = "John";
        var lastName = "Doe";
        var dateOfBirth = new DateOnly(2000, 1, 1).AddYears(-age); 
        var email = "john.doe@example.com";
        var phoneCode = "+1";
        var phoneNumber = "1234567890";
        
        // Act
    }
    
   
    

}