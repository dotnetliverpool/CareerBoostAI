using CareerBoostAI.Domain.Common.Exceptions;
using CareerBoostAI.Domain.CvContext.Entities;
using CareerBoostAI.Domain.CvContext.ValueObjects;

namespace CareerBoostAI.Domain.Services;

public static class ProfessionalEntrySequenceValidatorService
{
    public static void Validate(IEnumerable<ProfessionalEntry> entries)
    {
        
        var entriesList = entries.ToArray();
        var indexes = entriesList.Select(entry => entry.SequenceIndex).ToArray();
        
        var validRange = Enumerable
            .Range(1, indexes.Length)
            .Select(idx => SequenceIndex.Create((uint)idx)).ToArray();

        if (indexes.All(validRange.Contains) && indexes.Distinct().Count() == indexes.Length) return;
        
        var concreteClassName = entriesList.First().GetType().Name;
        throw new ProfessionalEntrySequenceInvalidException(concreteClassName);

    }
}