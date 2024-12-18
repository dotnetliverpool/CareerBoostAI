using CareerBoostAI.Domain.Exceptions;

namespace CareerBoostAI.Domain.ValueObjects;

public class CandidateFirstName
{
    public string Value { get;  }

    public CandidateFirstName(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new EmptyCandidateNameException();
        }

        Value = value;
    }

    public static implicit operator string(CandidateFirstName firstName)
        => firstName.Value;

    public static implicit operator CandidateFirstName(string firstName)
        => new(firstName);
}