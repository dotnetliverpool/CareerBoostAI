using System.Collections.Concurrent;
using CareerBoostAI.Domain.Common.Abstractions;
using CareerBoostAI.Domain.Common.Exceptions;

namespace CareerBoostAI.Domain.CvContext.ValueObjects;

public class Language : ValueObject
{
    public string Value { get; }

    private Language(string value)
    {
        Value = value;
    }
    
    public Language() {}

    public static Language Create(string value)
    {
        value.ThrowIfNullOrEmpty(nameof(Language));
        var result = value.Trim().ToLower();
        return new Language(result);
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}