namespace CareerBoostAI.Domain.Exceptions;

public class DuplicatePropertyException : CareerBoostAIDomainException
{
    public DuplicatePropertyException(string parent, string property, object value) 
        : base($"{parent} Already has {property} property of value [{value}].")
    {
    }
}