using CareerBoostAI.Domain.Common.Exceptions;
using CareerBoostAI.Domain.ValueObjects;

namespace CareerBoostAI.Domain.Candidate.CvEntity.ValueObjects;

public class OrganisationName : ValueObject
{
    public string Value { get; private set; }

    private OrganisationName(string value)
    {
        Value = value;
    }

    public static OrganisationName Create(string value)
    {
        value.ThrowIfNull();
        return new OrganisationName(value);
    }
    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}