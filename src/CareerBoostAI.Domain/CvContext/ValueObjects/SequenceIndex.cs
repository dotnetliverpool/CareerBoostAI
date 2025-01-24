﻿using CareerBoostAI.Domain.Common.Exceptions;
using CareerBoostAI.Domain.ValueObjects;

namespace CareerBoostAI.Domain.CvContext.ValueObjects;

public class SequenceIndex : ValueObject
{
    public uint Value { get;  }

    private SequenceIndex(uint value)
    {
        Value = value;
    }

    public static SequenceIndex Create(uint value)
    {
        if (value == 0)
        {
            throw new InvalidEntrySequenceIndexException($"{value}");
        }
        value.ThrowIfNull();
        return new SequenceIndex(value);
    }
    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}