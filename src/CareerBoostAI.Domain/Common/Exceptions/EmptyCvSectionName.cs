namespace CareerBoostAI.Domain.Common.Exceptions;

public class EmptyCvSectionName : CareerBoostAIDomainException
{
    public EmptyCvSectionName() : base("Section name cannot be empty or whitespace.")
    {
    }
}