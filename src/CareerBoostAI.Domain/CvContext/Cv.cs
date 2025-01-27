﻿using CareerBoostAI.Domain.Common.Abstractions;
using CareerBoostAI.Domain.Common.Exceptions;
using CareerBoostAI.Domain.Common.ValueObjects;
using CareerBoostAI.Domain.CvContext.Entities;
using CareerBoostAI.Domain.CvContext.Factory;
using CareerBoostAI.Domain.CvContext.Specifications;
using CareerBoostAI.Domain.CvContext.ValueObjects;
using Education = CareerBoostAI.Domain.CvContext.Entities.Education;

namespace CareerBoostAI.Domain.CvContext;

public class Cv : AggregateRoot<EntityId>
{
    private List<Experience> _experiences;
    private List<Education> _educations;
    private List<Skill> _skills;
    private List<Language> _languages;
    
    
    public Summary Summary { get; private set; }
    public Email CandidateEmail { get; private set; }
    public IReadOnlyCollection<Experience> Experiences => _experiences.AsReadOnly();
    public IReadOnlyCollection<Education> Educations => _educations.AsReadOnly();
    public IReadOnlyCollection<Skill> Skills => _skills.AsReadOnly();
    public IReadOnlyCollection<Language> Languages => _languages.AsReadOnly();
    
    private Cv(EntityId id,
        Summary summary,
        Email candidateEmail,
        IEnumerable<Experience> experiences, 
        IEnumerable<Education> educations, 
        IEnumerable<Skill> skills, 
        IEnumerable<Language> languages)
    {
        Id = id;
        Summary = summary;
        CandidateEmail = candidateEmail;
        _experiences = experiences.ToList();
        _educations = educations.ToList();
        _skills = skills.ToList();
        _languages = languages.ToList();
    }
    
    internal static Cv Create(
        EntityId id, Summary summary,
        Email candidateEmail, IEnumerable<Experience> experiences, 
        IEnumerable<Education> educations, IEnumerable<Skill> skills, 
        IEnumerable<Language> languages)
    {
        var experienceList = experiences.ToArray();
        var educationList = educations.ToArray();
        var spec = new ProfessionalEntrySequenceRangesFrom1ToNumberOfEntriesSpec();
        if (!spec.IsSatisfiedBy(experienceList))
        {
            throw new ProfessionalEntrySequenceInvalidException(nameof(Experience));
        }
        if (!spec.IsSatisfiedBy(educationList))
        {
            throw new ProfessionalEntrySequenceInvalidException(nameof(Education));
        }

        return new(id, summary, candidateEmail, experienceList,
            educationList, skills, languages);
    }

    public void UpdateSummary(string newSummary)
    {
        var result = ValueObjects.Summary.Create(newSummary);
        Summary = result;
    }

    public void UpdateSkills(IEnumerable<string> dataSkills)
    {
        var newSkills = dataSkills.Select(Skill.Create);  
        _skills.Clear(); 
        _skills.AddRange(newSkills);

    }

    public void UpdateLanguages(IEnumerable<string> dataLanguages)
    {
        var newLanguages = dataLanguages.Select(Language.Create);
        _languages.Clear();
        _languages.AddRange(newLanguages);
    }
    
    public void UpdateExperiences(IEnumerable<ExperienceData> dataExperiences)
    {
        var newExperiences = dataExperiences.Select(
            data => Experience.Create(
                Guid.NewGuid(), data.OrganisationName,
                data.City, data.Country, data.StartDate, data.EndDate,
                data.Description, data.Index)).ToArray();
        var spec = new ProfessionalEntrySequenceRangesFrom1ToNumberOfEntriesSpec();
        if (!spec.IsSatisfiedBy(newExperiences))
        {
            throw new ProfessionalEntrySequenceInvalidException(nameof(Experience));
        }
        _experiences.Clear();
        _experiences.AddRange(newExperiences);
    }
    
    public void UpdateEducations(IEnumerable<EducationData> dataEducations)
    {
        var newEducations = dataEducations.Select(
            data => Education.Create(
                Guid.NewGuid(), data.OrganisationName,
                data.City, data.Country, data.StartDate, data.EndDate,
                data.Program, data.Grade,  data.Index)).ToArray();
        var spec = new ProfessionalEntrySequenceRangesFrom1ToNumberOfEntriesSpec();
        if (!spec.IsSatisfiedBy(newEducations))
        {
            throw new ProfessionalEntrySequenceInvalidException(nameof(Education));
        }
        _educations.Clear();
        _educations.AddRange(newEducations);
    }

    public bool HasExperienceAt(string company)
    {
        var orgName = OrganisationName.Create(company);
        var result = _experiences
            .FirstOrDefault(exp => exp.OrganisationName.Equals(orgName));
        return result is not null;
    }
    
    public bool HasEducationalBackgroundAt(string institution)
    {
        var orgName = OrganisationName.Create(institution);
        var result = _educations
            .FirstOrDefault(edu => edu.OrganisationName.Equals(orgName));
        return result is not null;
    }

    
}