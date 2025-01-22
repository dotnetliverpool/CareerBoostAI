﻿using CareerBoostAI.Domain.Candidate.Cv.ValueObjects;
using CareerBoostAI.Domain.Candidate.CvEntity.ValueObjects;
using CareerBoostAI.Domain.Common.Abstractions;
using CareerBoostAI.Domain.Common.ValueObjects;

namespace CareerBoostAI.Domain.Cv.Entities;

public abstract class ProfessionalEntry : Entity<EntityId>
{
    protected ProfessionalEntry(
        EntityId id,
        OrganisationName organisationName,
        Location location,
        Period timePeriod,
        SequenceIndex sequenceIndex)
    {
        Id = id;
        OrganisationName = organisationName;
        Location = location;
        TimePeriod = timePeriod;
        SequenceIndex = sequenceIndex;
    }
    
    public ProfessionalEntry() {}

    public OrganisationName OrganisationName { get; }
    public Location Location { get; }
    public Period TimePeriod { get; }
    public SequenceIndex SequenceIndex { get; }
    
}



