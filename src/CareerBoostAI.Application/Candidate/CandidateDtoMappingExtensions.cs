using CareerBoostAI.Application.Candidate.DTO;
using CareerBoostAI.Application.DTO;
using CareerBoostAI.Domain.Candidate.Cv;
using CareerBoostAI.Domain.Candidate.Factories;
using CareerBoostAI.Domain.Candidate.ValueObjects;
using CareerBoostAI.Domain.Common.ValueObjects;

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
            Emails = candidate.Emails
                .Select(e => e.Value)
                .ToList(),
            PhoneNumbers = candidate.PhoneNumbers
                .Select(p 
                    => new PhoneNumberDto
                    {
                        Code = p.Code,
                        Number = p.Number,
                    })
                .ToList(),
            Cvs = candidate.Cvs.Select(cv => cv.AsDto()).ToList()
        };
    }

    public static Domain.Candidate.Candidate AsDomain(this CandidateDto candidateDto)
    {
        var candidate = new Domain.Candidate.Candidate(
            CandidateId.Create(candidateDto.Id),
            FirstName.Create(candidateDto.FirstName),
            LastName.Create(candidateDto.LastName),
            DateOfBirth.Create(candidateDto.DateOfBirth));
        foreach (var email in candidateDto.Emails)
        {
            candidate.AddEmail(Email.Create(email));
        }

        foreach (var phoneNumberDto in candidateDto.PhoneNumbers)
        {
            candidate.AddPhoneNumber(
                PhoneNumber.Create(phoneNumberDto.Code, phoneNumberDto.Number));
        }

        foreach (var cvDto in candidateDto.Cvs)
        {
            candidate.AddCv(cvDto.AsDomain());
        }

        return candidate;
    }

    public static CvDto AsDto(this Cv cv)
    {
        throw new NotImplementedException();
    }

    public static Cv AsDomain(this CvDto cvDto)
    {
        throw new NotImplementedException();
    }
}