using CareerBoostAI.Domain.Common.Exceptions;
using CareerBoostAI.Domain.ValueObjects;

namespace CareerBoostAI.Domain.Candidate.Cv.ValueObjects;

public class OrganisationName : ValueObject
{
    public string Value { get; private set; }

    private OrganisationName(string value)
    {
        Value = value;
    }

    public static OrganisationName Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new EmptyArgumentException(nameof(OrganisationName));
        }
        return new OrganisationName(value);
    }
    protected override IEnumerable<object> GetAtomicValues()
    {
        throw new NotImplementedException();
    }
}