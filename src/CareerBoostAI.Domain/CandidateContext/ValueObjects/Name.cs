using CareerBoostAI.Domain.Common.Exceptions;
using CareerBoostAI.Domain.ValueObjects;

namespace CareerBoostAI.Domain.CandidateContext.ValueObjects;

public class Name : ValueObject
{
    public string FirstName { get;  }
    public string LastName { get;  }

    private Name(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }

    public static Name Create(string firstName, string lastName)
    {
        firstName.ThrowIfNullOrEmpty($"{nameof(Name)}.FirstName");
        lastName.ThrowIfNullOrEmpty($"{nameof(Name)}.LastName");
        return new Name(firstName, lastName);
    }
    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return FirstName;
        yield return LastName;
    }
}