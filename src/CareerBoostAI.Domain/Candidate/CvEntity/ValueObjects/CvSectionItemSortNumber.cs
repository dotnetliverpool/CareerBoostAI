namespace CareerBoostAI.Domain.ValueObjects;

public class CvSectionItemSortNumber : ValueObject
{
    public string Value { get; }

    public CvSectionItemSortNumber(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Position cannot be empty or whitespace.", nameof(value));

        Value = value;
    }

 
    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }

}