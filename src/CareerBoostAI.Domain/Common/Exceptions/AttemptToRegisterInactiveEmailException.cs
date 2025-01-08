namespace CareerBoostAI.Domain.Common.Exceptions;

public class AttemptToRegisterInactiveEmailException : CareerBoostAIDomainException
{
    internal AttemptToRegisterInactiveEmailException(Guid id, string email) 
        : base($"Attempted to register the inactive email {email} to {id}")
    {
    }
}