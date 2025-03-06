using CareerBoostAI.Domain.Common.Abstractions;
using CareerBoostAI.Domain.Common.Exceptions;

namespace CareerBoostAI.Domain.CvContext.ValueObjects;

public class Language : ValueObject
{
    public Guid Id { get; private set; }
    public string Value { get; }

    private Language(Guid id, string value)
    {
        Id = id;
        Value = value;
    }
    
    public Language() {}

    public static Language Create(string value)
    {
        value.ThrowIfNullOrEmpty(nameof(Language));
        var result = value.Trim().ToLower();
        return new Language(Guid.NewGuid(), result);
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}