using CareerBoostAI.Application.Candidate.DTO;
using CareerBoostAI.Application.DTO;
using CareerBoostAI.Domain.Candidate.Cv;

namespace CareerBoostAI.Application.Candidate;

public static class CandidateDtoMappingExtensions
{
    public static CandidateDto AsDto(this Domain.Candidate.Candidate candidate)
    {
        return new CandidateDto
        {
            Id = candidate.Id.Value,
            FirstName = candidate.FirstName.Value,
            LastName = candidate.LastName.Value,
            DateOfBirth = candidate.DateOfBirth.Value,
            Emails = candidate.Emails.Select(e => e.Value).ToList(),
            PhoneNumbers = candidate.PhoneNumbers.Select(p => p.ToString()).ToList(),
            Cvs = candidate.Cvs.Select(cv => cv.AsDto()).ToList()
        };
    }

    public static Domain.Candidate.Candidate AsDomain(this CandidateDto candidateDto)
    {
        
    }

    public static CvDto AsDto(this Cv cv)
    {
        
    }

    public static Cv AsDomain(this CvDto cvDto)
    {
        
    }
}