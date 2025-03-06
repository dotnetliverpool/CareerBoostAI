using CareerBoostAI.Domain.CandidateContext.Services;
using CareerBoostAI.Domain.CandidateContext.ValueObjects;
using CareerBoostAI.Domain.Common.ValueObjects;

namespace CareerBoostAI.Tests.Unit.Domain.Candidate.InvarianceTests;

public class CandidateInvarianceTest : BaseCandidateTest
{
    [Theory]
    [InlineData("John", "Doe", "Jane", "Meredith")]
    public void UpdateName_ShouldUpdateName_WithoutSideEffect_WhenValidDataIsProvided(
        string firstName, string lastName, string updatedFirstName, string updatedLastName)
        {
            // Arrange
            var id = EntityId.NewId();
            var initialName = Name.Create(firstName, lastName);
            var dateOfBirth = DateOfBirth.Create(DateOnly.Parse("1998-01-05"));
            var email = Email.Create("test@example.com");
            var phoneNumber = PhoneNumber.Create("+44", "123456789");
            var candidate = new CareerBoostAI.Domain.CandidateContext.Candidate(
                id, initialName, dateOfBirth, email, phoneNumber);

            // Act
            candidate.UpdateName(updatedFirstName, updatedLastName);

            // Assert
            candidate.Name.ShouldNotBeNull();
            candidate.Name.ShouldBe(Name.Create(updatedFirstName, updatedLastName));
            candidate.Id.ShouldBe(id);
            candidate.Email.ShouldBe(email);
            candidate.PhoneNumber.ShouldBe(phoneNumber);
            candidate.DateOfBirth.ShouldBe(dateOfBirth);
        }

    [Theory]
    [InlineData("1990-01-01", "1995-05-15")]
    public void UpdateDateOfBirth_ShouldUpdateDateOfBirth_WhenValidDataIsProvided(string initialDob, string updatedDob)
    {
        // Arrange
        var id = EntityId.NewId();
        var name = Name.Create("john", "doe");
        var initialDateOfBirth = DateOfBirth.Create(DateOnly.Parse(initialDob));
        var email = Email.Create("test@example.com");
        var phoneNumber = PhoneNumber.Create("+44", "123456789");
        var candidate = new CareerBoostAI.Domain.CandidateContext.Candidate(
            id, name, initialDateOfBirth, email, phoneNumber);

        // Act
        candidate.UpdateDateOfBirth(DateOnly.Parse(updatedDob), TestDateTimeProvider.FromDateString("2025-01-01"));

        // Assert
        candidate.DateOfBirth.ShouldNotBeNull();
        candidate.DateOfBirth.ShouldBe(DateOfBirth.Create(DateOnly.Parse(updatedDob)));
        candidate.Name.ShouldBe(name);
        candidate.Id.ShouldBe(id);
        candidate.Email.ShouldBe(email);
        candidate.PhoneNumber.ShouldBe(phoneNumber);
        
    }

    [Theory]
    [InlineData("+44", "123456789", "+1", "987654321")]
    public void UpdatePhoneNumber_ShouldUpdatePhoneNumber_WhenValidDataIsProvided(
        string initialPhoneCode, string initialNumber, 
        string updatedPhoneCode, string updatedNumber)
    {
        // Arrange
        var id = EntityId.NewId();
        var name = Name.Create("john", "doe");
        var dateOfBirth = DateOfBirth.Create(DateOnly.Parse("1998-01-05"));
        var email = Email.Create("test@example.com");
        var phoneNumber = PhoneNumber.Create(initialPhoneCode, initialNumber);
        var candidate = new CareerBoostAI.Domain.CandidateContext.Candidate(
            id, name, dateOfBirth, email, phoneNumber);

        // Act
        candidate.UpdatePhoneNumber(updatedPhoneCode, updatedNumber);

        // Assert
        candidate.PhoneNumber.ShouldNotBeNull();
        candidate.PhoneNumber.ShouldBe(PhoneNumber.Create(updatedPhoneCode, updatedNumber));
        candidate.DateOfBirth.ShouldBe(dateOfBirth);
        candidate.Name.ShouldBe(name);
        candidate.Id.ShouldBe(id);
        candidate.Email.ShouldBe(email);
    }
    
    [Fact]
    public void UpdateCandidateProfile_ShouldUpdateAllFields_WhenValidDataIsProvided()
    {
        // Arrange
        var id = EntityId.NewId();
        var name = Name.Create("john", "doe");
        var dateOfBirth = DateOfBirth.Create(DateOnly.Parse("1998-01-05"));
        var email = Email.Create("test@example.com");
        var phoneNumber = PhoneNumber.Create("+44", "123456789");
        var candidate = new CareerBoostAI.Domain.CandidateContext.Candidate(
            id, name, dateOfBirth, email, phoneNumber);
        var service = new CandidateProfileUpdateService(TestDateTimeProvider.FromDateString("2025-01-01"));

        // Act
        service.Update(candidate, "Jane", "Jones", 
            DateOnly.Parse("1995-02-01"), "+1", "987654321");

        // Assert
        candidate.Id.ShouldBe(id);
        candidate.Email.ShouldBe(email);
        candidate.Name.ShouldBe(Name.Create("Jane", "Jones"));
        candidate.DateOfBirth.ShouldBe(DateOfBirth.Create(DateOnly.Parse("1995-02-01")));
        candidate.PhoneNumber.ShouldBe(PhoneNumber.Create("+1", "987654321"));
    }
}