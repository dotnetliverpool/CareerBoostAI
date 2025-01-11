using CareerBoostAI.Domain.Candidate.CvEntity;
using CareerBoostAI.Domain.Candidate.ValueObjects;
using CareerBoostAI.Domain.Common.ValueObjects;

namespace CareerBoostAI.Domain.Candidate.Factories;

public interface ICandidateFactory
{
    
    public CandidateAggregate Create(CandidateId id, FirstName firstName,
        LastName lastName, DateOfBirth dateOfBirth,
        Email email, PhoneNumber phoneNumber);
    
    public CandidateAggregate Create(CandidateId id, FirstName firstName,
        LastName lastName, DateOfBirth dateOfBirth,
        Email email, PhoneNumber phoneNumber, IEnumerable<CvEntity.Cv> cvs);
    
}