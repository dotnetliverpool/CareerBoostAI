namespace CareerBoostAI.Domain.ValueObjects;

public class SequenceIndex : ValueObject
{
    public uint Value { get; private set; }

    private SequenceIndex(uint value)
    {
        Value = value;
    }

    public SequenceIndex Create(uint value)
    {
        return new SequenceIndex(value);
    }
    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}