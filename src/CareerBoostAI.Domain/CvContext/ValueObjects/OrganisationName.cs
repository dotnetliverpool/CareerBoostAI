using CareerBoostAI.Domain.Common.Abstractions;
using CareerBoostAI.Domain.Common.Exceptions;

namespace CareerBoostAI.Domain.CvContext.ValueObjects;

public class OrganisationName : ValueObject
{
    public string Value { get; private set; }

    private OrganisationName(string value)
    {
        Value = value;
    }

    public static OrganisationName Create(string value)
    {
        value.ThrowIfNullOrEmpty(nameof(OrganisationName));
        return new OrganisationName(value);
    }
    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}