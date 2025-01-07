using CareerBoostAI.Domain.Candidate.ValueObjects;
using CareerBoostAI.Infrastructure.EF.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CareerBoostAI.Infrastructure.EF.Configuration;

public class CandidateDbObjectConverstionConfiguration : 
    IEntityTypeConfiguration<Candidate>
{
    public void Configure(EntityTypeBuilder<Candidate> builder)
    {
        var firstNameConverter = new ValueConverter<FirstName, string>(
            fn => fn.ToString(), 
            value => FirstName.CreateTrusted(value));
        
        var lastNameConverter = new ValueConverter<LastName, string>(
            fn => fn.ToString(), 
            value => LastName.CreateTrusted(value));

        builder
            .Property(c => c.Id)
            .HasConversion<CandidateId>(
                id => CandidateId.Create(id), 
                id => id.Value);
    }
}