namespace CareerBoostAI.Domain.Common.Exceptions;

public class DuplicatePropertyException : CareerBoostAIDomainException
{
    public DuplicatePropertyException(string parent, string property, object value) 
        : base($"{parent} Already has {property} property of value [{value}].")
    {
    }
    
    public DuplicatePropertyException(object value) 
        : base($"duplicate value found for [{value}].")
    {
    }
}