using CareerBoostAI.Domain.Common.Events;
using CareerBoostAI.Domain.Common.ValueObjects;
using Shouldly;
using Xunit;

namespace CareerBoostAI.Tests.Unit.Application.Candidate;

public class CandidateEmails : BaseCandidateTest
{
    [Fact]
    public void RegisterEmail_Should_RaiseEvent_WhenEmailIsValid()
    {
        // Arrange
        var candidate = GetNewCandidate();
        var email = Email.Create("jane.doe@example.com", isActive: true);

        // Act
        var exception = Record.Exception(() => candidate.RegisterEmail(email));
        

        // Assert
        exception.ShouldBeNull();
        
        candidate.Events.Count().ShouldBe(1);
        var @event = candidate.Events.FirstOrDefault() as EmailRegisteredEvent;
        
        
        @event.ShouldNotBeNull();
        @event.ShouldBeOfType<EmailRegisteredEvent>();
       
        @event.Email.ShouldBe(email.Value);
        
    }

    [Fact]
    public void RegisterEmail_ShouldDeactivateCurrentActiveEmail()
    {
        // Arrange
        var candidate = GetNewCandidate();
        var oldEmail = Email.Create("old.email@example.com", isActive: true);
        candidate.RegisterEmail(oldEmail);
        
        var newEmail = Email.Create("new.email@example.com", isActive: true);

        // Act
        var exception = Record.Exception(() => candidate.RegisterEmail(newEmail));
        
        // Assert
        exception.ShouldBeNull();
        
        // Check that the old email has been deactivated
        var oldRegisteredEmail = candidate.GetEmail(oldEmail.Value);
        oldRegisteredEmail.ShouldNotBeNull();
        oldRegisteredEmail.IsActive.ShouldBeFalse();
        
        // Ensure that the new email is active
        var activeEmail = candidate.ActiveEmail;
        activeEmail.ShouldNotBeNull();
        activeEmail.IsActive.ShouldBeTrue();
        activeEmail.Value.ShouldBe(newEmail.Value);
        
    }
    
    [Fact]
    public void RegisterEmail_Should_ThrowAttemptToRegisterInactiveEmailException_WhenEmailIsInactive()
    {}
    
    [Fact]
    public void RegisterEmail_Should_ThrowDuplicatePropertyException_WhenEmailIsDuplicate()
    {}
    
    [Fact]
    public void AddEmails_Should_ThrowDuplicatePropertyException_WhenEmailIsDuplicate()
    {}
}