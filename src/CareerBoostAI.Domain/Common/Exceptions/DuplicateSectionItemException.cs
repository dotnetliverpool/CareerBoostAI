namespace CareerBoostAI.Domain.Common.Exceptions;

public class DuplicateSectionItemException : CareerBoostAIDomainException
{
    public DuplicateSectionItemException() : base("Duplicate section item cannot be added.")
    {
    }
}