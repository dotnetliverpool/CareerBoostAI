using CareerBoostAI.Domain.Common.Exceptions;
using CareerBoostAI.Domain.CvContext.Entities;
using CareerBoostAI.Domain.CvContext.ValueObjects;

namespace CareerBoostAI.Domain.Services;

public static class ProfessionalEntrySequenceValidatorService
{
    public static void Validate(IEnumerable<ProfessionalEntry> entries)
    {
        var entriesList = entries.ToList();
        var invalidSequence = entriesList
            .Select((entry, index) => new { entry, index })
            .FirstOrDefault(x => x.entry.SequenceIndex != SequenceIndex.Create((uint)x.index));
        if (invalidSequence is null) return;
        var concreteClassName = invalidSequence.entry.GetType().Name;
        throw new ProfessionalEntrySequenceInvalidError(concreteClassName);
    }
}