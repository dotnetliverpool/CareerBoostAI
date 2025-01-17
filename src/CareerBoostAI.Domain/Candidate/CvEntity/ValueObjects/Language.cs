using System.Collections.Concurrent;
using CareerBoostAI.Domain.Candidate.ValueObjects;
using CareerBoostAI.Domain.Common.Exceptions;
using CareerBoostAI.Domain.ValueObjects;

namespace CareerBoostAI.Domain.Candidate.CvEntity.ValueObjects;

public class Language : ValueObject
{
    private static readonly ConcurrentDictionary<string, Language> _flyweightCache = new();
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
        return _flyweightCache.GetOrAdd(result, key => new Language(key));;
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}