using CareerBoostAI.Application.Candidate.DTO;
using CareerBoostAI.Infrastructure.EF.Models;
using CvSection = CareerBoostAI.Infrastructure.EF.Models.CvSection;
using CvSectionItem = CareerBoostAI.Infrastructure.EF.Models.CvSectionItem;

namespace CareerBoostAI.Infrastructure.EF.MappingExtensions;

public static class CandidateMappinngExtensions
{
    public static CandidateDto AsDto(this Candidate candidate)
    {
        return new CandidateDto
        {
            Id = candidate.Id,
            FirstName = candidate.FirstName,
            LastName = candidate.LastName,
            DateOfBirth = candidate.DateOfBirth,
            Emails = candidate.Emails.Select(e => e.Address).ToList(),
            PhoneNumbers = candidate.PhoneNumbers.Select(p => p.AsDto()).ToList(),
            Cvs = candidate.Cvs.Select(cv => cv.AsDto()).ToList()
        };
    }

    public static PhoneNumberDto AsDto(this PhoneNumber phoneNumber)
    {
        return new PhoneNumberDto
        {
            Code = phoneNumber.CountryCode,
            Number = phoneNumber.Number
        };
    }

    public static CvDto AsDto(this Cv cv)
    {
        return new CvDto
        {
            Id = cv.Id,
            FileName = cv.FileName,
            StorageAddress = cv.StorageAddress,
            Storagemedium = cv.StorageMedium,
            Content = new CvContentDto
            {
                FirstName = cv.FirstName,
                LastName = cv.LastName,
                Email = cv.EmailAddress,
                PhoneCode = cv.PhoneCountryCode,
                PhoneNumber = cv.PhoneNumber,
                HouseAddress = cv.Address,
                City = cv.City,
                Country = cv.Country,
                Postcode = cv.PostalCode,
                About = cv.About,
                Sections = cv.Sections.Select(section => section.AsDto()).ToList()
            }
        };
    }
    
    public static CvSectionDto AsDto(this CvSection section)
    {
        return new CvSectionDto
        {
            SectionName = section.Name,
            SequenceIndex = section.SequenceIndex,
            Items = section.SectionItems.Select(item => item.AsDto()).ToList()
        };
    }

    public static CvSectionItemDto AsDto(this CvSectionItem item)
    {
        return new CvSectionItemDto
        {
            OrganisationName = item.OrganisationName,
            OrganisationCity = item.City,
            OrganisationCountry = item.Country,
            Description = item.Description,
            StartDate = item.StartDate,
            EndDate = item.EndDate,
            SequenceIndex = item.SequenceIndex
        };
    }
public static Candidate AsModel(this CandidateDto candidateDto)
    {
        return new Candidate
        {
            Id = candidateDto.Id,
            FirstName = candidateDto.FirstName,
            LastName = candidateDto.LastName,
            DateOfBirth = candidateDto.DateOfBirth,
            Emails = candidateDto.Emails.Select(e => new Email { Address = e }).ToList(),
            PhoneNumbers = candidateDto.PhoneNumbers.Select(p => p.AsModel()).ToList(),
            Cvs = candidateDto.Cvs.Select(cv => cv.AsModel()).ToList()
        };
    }

    public static PhoneNumber AsModel(this PhoneNumberDto phoneNumberDto)
    {
        return new PhoneNumber
        {
            CountryCode = phoneNumberDto.Code,
            Number = phoneNumberDto.Number
        };
    }

    public static Cv AsModel(this CvDto cvDto)
    {
        var content = cvDto.Content ?? new CvContentDto();
        return new Cv
        {
            Id = cvDto.Id,
            FileName = cvDto.FileName,
            StorageAddress = cvDto.StorageAddress,
            StorageMedium = cvDto.Storagemedium,
            FirstName = content.FirstName,
            LastName = content.LastName,
            EmailAddress = content.Email,
            PhoneCountryCode = content.PhoneCode,
            PhoneNumber = content.PhoneNumber,
            Address = content.HouseAddress!,
            City = content.City!,
            Country = content.Country!,
            PostalCode = content.Postcode!,
            About = content.About,
            Sections = content.Sections.Select(section => section.AsModel()).ToList()
        };
    }

    public static CvSection AsModel(this CvSectionDto sectionDto)
    {
        return new CvSection
        {
            Name = sectionDto.SectionName,
            SequenceIndex = sectionDto.SequenceIndex,
            SectionItems = sectionDto.Items.Select(item => item.AsModel()).ToList()
        };
    }

    public static CvSectionItem AsModel(this CvSectionItemDto itemDto)
    {
        return new CvSectionItem
        {
            OrganisationName = itemDto.OrganisationName,
            City = itemDto.OrganisationCity,
            Country = itemDto.OrganisationCountry,
            Description = itemDto.Description,
            StartDate = itemDto.StartDate,
            EndDate = itemDto.EndDate,
            SequenceIndex = itemDto.SequenceIndex
        };
    }
}
