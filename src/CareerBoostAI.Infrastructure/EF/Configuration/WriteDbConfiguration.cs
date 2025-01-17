using CareerBoostAI.Domain.Candidate;
using CareerBoostAI.Domain.Candidate.Cv.ValueObjects;
using CareerBoostAI.Domain.Candidate.CvEntity;
using CareerBoostAI.Domain.Candidate.CvEntity.ValueObjects;
using CareerBoostAI.Domain.Candidate.ValueObjects;
using CareerBoostAI.Domain.Common.ValueObjects;
using CareerBoostAI.Domain.UserUpload;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage;

namespace CareerBoostAI.Infrastructure.EF.Configuration;


internal class WriteDbConfiguration : IEntityTypeConfiguration<CandidateAggregate>, 
    IEntityTypeConfiguration<CandidateCv>, IEntityTypeConfiguration<Upload>,
    IEntityTypeConfiguration<WorkExperience>, IEntityTypeConfiguration<Education>
     
{
    public void Configure(EntityTypeBuilder<CandidateAggregate> builder)
    {
        builder
            .Property(c => c.Id)
            .HasConversion(
                id => id.Value,
                value => CandidateId.Create(value)
                );
        
        builder
            .Property(c => c.FirstName)
            .HasConversion<string>(
                objectValue => objectValue.Value,
                value => FirstName.Create(value)
            );
        
        builder
            .Property(c => c.LastName)
            .HasConversion(
                ln => ln.Value,
                value => LastName.Create(value)
            );
        
        builder
            .Property(c => c.DateOfBirth)
            .HasConversion(
                dob => dob.Value,
                value => DateOfBirth.Create(value))
        
        builder
            .Property(c => c.Email)
            .HasConversion(
                em => em.Value,
                value => Email.Create(value)
            );

        builder
            .Property(c => c.PhoneNumber)
            .HasConversion(
                phone => $"{phone.Code} {phone.Number}",
                dbValue => PhoneNumber.Parse(dbValue)
            )
            .HasColumnName("PhoneNumber");
        
        builder
            .HasOne(c => c.CandidateCv);
    }

    public void Configure(EntityTypeBuilder<CandidateCv> builder)
    {
        builder
            .Property(cv => cv.Id )
            .HasConversion(
                writeObject => writeObject.Value,
                storeValue => CvId.Create(storeValue));

        builder.OwnsMany(cv => cv.Skills, sb =>
        {
            sb.WithOwner().HasForeignKey("CvId");
            sb.Property(s => s.Value).HasColumnName("Name");
            sb.ToTable("CvSkills");
        });
        
        builder.OwnsMany(cv => cv.Languages, lb =>
        {
            lb.WithOwner().HasForeignKey("CVId");
            lb.Property(l => l.Value).HasColumnName("Name");
            lb.ToTable("CVLanguages");
        });
        
        builder.Metadata
            .FindNavigation(nameof(CandidateCv.Experiences))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
        
        builder.Metadata
            .FindNavigation(nameof(CandidateCv.Educations))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
        
        builder.Metadata
            .FindNavigation(nameof(CandidateCv.Skills))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
        
        builder.Metadata
            .FindNavigation(nameof(CandidateCv.Languages))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
    
    public void Configure(EntityTypeBuilder<WorkExperience> builder)
    {
        builder
            .Property(exp => exp.Id)
            .HasConversion(
                id => id.Value,
                value => ProfessionalEntryId.Create(value));
        
        builder
            .Property(exp => exp.OrganisationName)
            .HasConversion(
                orgName => orgName.Value,
                value => OrganisationName.Create(value));
        
        builder
            .Property(exp => exp.Description)
            .HasConversion(
                desc => desc.Value,
                value => Description.Create(value));
        
        builder
            .Property(exp => exp.SequenceIndex)
            .HasConversion(
                idx => idx.Value,
                value => SequenceIndex.Create(value));

        builder
            .OwnsOne(exp => exp.Location, lb =>
            {
                lb.Property(l => l.City).HasColumnName("City");
                lb.Property(l => l.Country).HasColumnName("Country");
            });
        
        builder
            .OwnsOne(exp => exp.TimePeriod, prd =>
            {
                prd.Property(p => p.StartDate).HasColumnName("StartDate");
                prd.Property(p => p.EndDate).HasColumnName("EndDate");
            });
    }

    public void Configure(EntityTypeBuilder<Education> builder)
    {
        builder
            .Property(edu => edu.Id)
            .HasConversion(
                id => id.Value,
                value => ProfessionalEntryId.Create(value));
        
        builder
            .Property(edu => edu.OrganisationName)
            .HasConversion(
                orgName => orgName.Value,
                value => OrganisationName.Create(value));
        
        builder
            .Property(edu => edu.SequenceIndex)
            .HasConversion(
                idx => idx.Value,
                value => SequenceIndex.Create(value));

        builder
            .OwnsOne(edu => edu.Location, lb =>
            {
                lb.Property(l => l.City).HasColumnName("City");
                lb.Property(l => l.Country).HasColumnName("Country");
            });
        
        builder
            .OwnsOne(edu => edu.TimePeriod, prd =>
            {
                prd.Property(p => p.StartDate).HasColumnName("StartDate");
                prd.Property(p => p.EndDate).HasColumnName("EndDate");
            });
        
        builder.OwnsOne(edu => edu.Grade, grd =>
        {
            grd.Property(g => g.Program).HasColumnName("Program");
            grd.Property(g => g.Grade).HasColumnName("Grade");
        });
    }
    
    // public void Configure(EntityTypeBuilder<Skill> builder)
    // {
    //     
    // }
    //
    // public void Configure(EntityTypeBuilder<Language> builder)
    // {
    //     
    // }

    public void Configure(EntityTypeBuilder<Upload> builder)
    {

    }
}
