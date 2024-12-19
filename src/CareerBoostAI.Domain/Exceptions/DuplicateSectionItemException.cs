namespace CareerBoostAI.Domain.Exceptions;

public class DuplicateSectionItemException : CareerBoostAIDomainException
{
    public DuplicateSectionItemException() : base("Duplicate section item cannot be added.")
    {
    }
}