using CareerBoostAI.Application.Candidate;
using CareerBoostAI.Application.Candidate.DTO;
using CareerBoostAI.Infrastructure.EF.Contexts;
using CareerBoostAI.Infrastructure.EF.Models;
using Microsoft.EntityFrameworkCore;

namespace CareerBoostAI.Infrastructure.EF.Repositories;

internal sealed class PostgresCandidateRepository(CareerBoostDbContext context) : ICandidateRepository
{
    
    private readonly DbSet<Candidate> _candidates = context.Candidates;
    private readonly CareerBoostDbContext _context = context;

    public async Task<CandidateDto?> GetAsync(Guid id)
    {
        var candidateModel = await _candidates
            .Include(c => c.Emails)
            .Include(c => c.PhoneNumbers)
            .Include(c => c.Cvs)
            .SingleOrDefaultAsync(c => c.Id == id);

        if (candidateModel == null)
        {
            return null;
        }


        return new CandidateDto
        {
            Id = candidateModel.Id,
            FirstName = candidateModel.FirstName,
            LastName = candidateModel.LastName,
            DateOfBirth = candidateModel.DateOfBirth,
            Emails = candidateModel.Emails.Select(e => e.Address).ToList(),
            PhoneNumbers = candidateModel.PhoneNumbers.Select(p => new PhoneNumberDto
            {
                Code = p.CountryCode,
                Number = p.Number,
                
            }).ToList(),
            Cvs = candidateModel.Cvs.Select(cv => new CvDto
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
                    Sections = cv.Sections
                        .Select(section => new CvSectionDto
                        {
                            SectionName = section.Name,
                            SequenceIndex = section.SequenceIndex,
                            Items = section.SectionItems
                                .Select(item => new CvSectionItemDto
                                {
                                    OrganisationName = item.OrganisationName,
                                    OrganisationCity = item.City,
                                    OrganisationCountry = item.Country,
                                    Description = item.Description,
                                    StartDate = item.StartDate,
                                    EndDate = item.EndDate,
                                    SequenceIndex = item.SequenceIndex,
                                })
                            .ToList()
                        })
                    .ToList()
                }
            }).ToList()
        };

    }

    public Task AddAsync(CandidateDto candidate)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(CandidateDto candidate)
    {
        throw new NotImplementedException();
    }
}