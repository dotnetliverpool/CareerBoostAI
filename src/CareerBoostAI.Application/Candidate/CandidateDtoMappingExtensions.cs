using System.Collections.Specialized;
using CareerBoostAI.Application.Candidate.DTO;
using CareerBoostAI.Application.Common.Extension;
using CareerBoostAI.Application.DTO;
using CareerBoostAI.Domain.Candidate.Cv;
using CareerBoostAI.Domain.Candidate.Cv.ValueObjects;
using CareerBoostAI.Domain.Candidate.Factories;
using CareerBoostAI.Domain.Candidate.ValueObjects;
using CareerBoostAI.Domain.Common.ValueObjects;
using CareerBoostAI.Domain.Enums;

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
        // TODO : Use factory to instantiate
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
        return new CvDto
        {
            Id = cv.Id.Value,
            FileName = cv.File.Name,
            Storagemedium = cv.File.StorageMedium.ToString(),
            StorageAddress = cv.File.StorageAddress,
            Content = cv.IsParsed ? cv.Content.AsDto() : null,
        };
    }

    public static Cv AsDomain(this CvDto cvDto)
    {
       
        var cv = new Cv(
            CvId.Create(cvDto.Id), 
            CvFile.Create(
                cvDto.FileName, 
                cvDto.Storagemedium.ToEnum<CvStorageMedium>(),
                cvDto.StorageAddress));
        cv.SetContent(cvDto.Content != null ? cvDto.Content.AsDomain() : NullCvContent.Instance);
        return cv;
    }

    public static CvContentDto AsDto(this BaseCvContent cvContent)
    {
        if (cvContent is not CvContent content)
        {
            throw new ApplicationException("Invalid content type.");
        }

        return new CvContentDto
        {
            FirstName = content.FirstName.Value,
            LastName = content.LastName.Value,
            Email = content.Email.Value,
            PhoneCode = content.PhoneNumber.Code,
            PhoneNumber = content.PhoneNumber.Number,
            About = content.About.Value,
            HouseAddress = content.Address.HouseAddress,
            City = content.Address.City,
            Country = content.Address.Country,
            Postcode = content.Address.Postcode,
            Sections = content.Sections
                .Select(section => section.AsDto())
                .ToList(),
            
        };
    }

    public static CvSectionDto AsDto(this CvSection section)
    {
        
    }

    public static CvSectionItem AsDto(this CvSectionItem sectionItem)
    {
        
    }
}