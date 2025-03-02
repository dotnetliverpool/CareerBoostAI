namespace CareerBoostAI.Domain.Common.Exceptions;

public class NoActiveEmailFoundException : CareerBoostAIDomainException
{
    public NoActiveEmailFoundException(string userName) : base($"{userName} does not have an active email")
    {
    }
}