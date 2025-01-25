using CareerBoostAI.Domain.Common.Abstractions.SpecificationPattern;
using CareerBoostAI.Domain.CvContext.Entities;
using CareerBoostAI.Domain.CvContext.ValueObjects;

namespace CareerBoostAI.Domain.CvContext.Specifications;

public class ProfessionalEntrySequenceRangesFrom1ToNumberOfEntriesSpec : Specification<IEnumerable<ProfessionalEntry>>
{
    public override bool IsSatisfiedBy(IEnumerable<ProfessionalEntry> candidate)
    {
        var entriesList = candidate.ToArray();
        var indexes = entriesList.Select(entry => entry.SequenceIndex).ToArray();
        
        var validRange = Enumerable
            .Range(1, indexes.Length)
            .Select(idx => SequenceIndex.Create((uint)idx)).ToArray();

        return indexes.All(validRange.Contains) && indexes.Distinct().Count() == indexes.Length;
    }
}