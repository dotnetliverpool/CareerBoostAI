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
using CareerBoostAI.Domain.ValueObjects;

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

    public static Domain.Candidate.Candidate AsDomain(
        this CandidateDto candidateDto, ICandidateFactory candidateFactory)
    {
        return candidateFactory.HydrateCreate(
                CandidateId.Create(candidateDto.Id),
                FirstName.Create(candidateDto.FirstName),
                LastName.Create(candidateDto.LastName),
                DateOfBirth.Create(candidateDto.DateOfBirth),
                candidateDto.Emails
                    .Select(em => Email.Create(em))
                    .ToList(),
                candidateDto.PhoneNumbers
                    .Select(pn => PhoneNumber.Create(pn.Code, pn.Number))
                    .ToList(),
                candidateDto.Cvs
                    .Select(cv => cv.AsDomain())
                    .ToList()
                );
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

    public static BaseCvContent AsDomain(this CvContentDto contentDto)
    {
        var result =  new CvContent(
            FirstName.Create(contentDto.FirstName), 
            LastName.Create(contentDto.LastName),
            Email.Create(contentDto.Email),
            PhoneNumber.Create(contentDto.PhoneCode, contentDto.PhoneNumber),
            CvAddress.Create(
                contentDto.HouseAddress, contentDto.City, 
                contentDto.Country, contentDto.Postcode),
            CvAbout.Create(contentDto.About));
        
        foreach (var section in contentDto.Sections)
        {
            result.AddSection(section.AsDomain());
        }
        return result;
    }

    public static CvSectionDto AsDto(this CvSection section)
    {
        return new CvSectionDto
        {
            SectionName = section.Name.Value,
            SequenceIndex = section.SequenceIndex.Value,
            Items = section.Items.
                Select(item => item.AsDto())
                .ToList()
        };
    }

    public static CvSection AsDomain(this CvSectionDto sectionDto)
    {
        var section = new CvSection(
            CvSectionName.Create(sectionDto.SectionName),
            SequenceIndex.Create(sectionDto.SequenceIndex)
            );
        foreach (var item in sectionDto.Items)
        {
            section.AddItem(item.AsDomain());
        }
        return section;
    }

    public static CvSectionItemDto AsDto(this CvSectionItem item)
    {
        return new CvSectionItemDto
        {
            OrganisationName = item.OrganisationName.Value,
            OrganisationCity = item.Location.City,
            OrganisationCountry = item.Location.Country,
            StartDate = item.TimeRange.StartDate,
            EndDate = item.TimeRange.EndDate,
            Description = item.Description.Value,
            SequenceIndex = item.SequenceIndex.Value,
        };
    }

    public static CvSectionItem AsDomain(this CvSectionItemDto itemDto)
    {
        return new CvSectionItem(
            OrganisationName.Create(itemDto.OrganisationName),
            SectionItemLocation.Create(itemDto.OrganisationCity, itemDto.OrganisationCountry),
            CandidateCvSectionItemTimeRange.Create(itemDto.StartDate, itemDto.EndDate),
            CvSectionItemDescription.Create(itemDto.Description),
            SequenceIndex.Create(itemDto.SequenceIndex)
            );
    }
}