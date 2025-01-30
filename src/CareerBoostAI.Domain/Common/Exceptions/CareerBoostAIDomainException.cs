using CareerBoostAI.Shared.Abstractions.Exceptions;

namespace CareerBoostAI.Domain.Common.Exceptions;

public class DuplicatePropertyException : CareerBoostAiDomainException
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

public class EmptyArgumentException(string argName) 
    : CareerBoostAiDomainException($"{argName} cannot be empty");