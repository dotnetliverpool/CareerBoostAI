using CareerBoostAI.Domain.Common.Exceptions;

namespace CareerBoostAI.Domain.Candidate.CvEntity.ValueObjects;

public class ProfessionalEntryId
{
    private ProfessionalEntryId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }
    public static ProfessionalEntryId Create(Guid value)
    {
        value.ThrowIfNull();
        return new(value);
    }
}