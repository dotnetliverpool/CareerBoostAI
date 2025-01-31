using CareerBoostAI.Application.Candidate.Commands.CreateOrUpdateData;
using CareerBoostAI.Application.Candidate.Commands.UpdateCvContent;

namespace CareerBoostAI.Tests.Unit.Application;

public class CommandFactory
{
    public  CreateCvCommand GetValidCreateCvCommand(
        int numberOfExperiences = 10,
        int numberOfEducations = 5,
        int numberOfSkills = 20,
        int numberOfLanguages = 10)
    {
        if (numberOfExperiences < 1 || numberOfExperiences > 10)
            throw new ArgumentOutOfRangeException(nameof(numberOfExperiences),
                "Number of experiences must be between 1 and 10.");
        if (numberOfEducations < 1 || numberOfEducations > 5)
            throw new ArgumentOutOfRangeException(nameof(numberOfEducations),
                "Number of educations must be between 1 and 5.");
        if (numberOfSkills < 1 || numberOfSkills > 20)
            throw new ArgumentOutOfRangeException(nameof(numberOfSkills), "Number of skills must be between 1 and 20.");
        if (numberOfLanguages < 1 || numberOfLanguages > 10)
            throw new ArgumentOutOfRangeException(nameof(numberOfLanguages),
                "Number of languages must be between 1 and 10.");

        return new CreateCvCommand(
            "A seasoned professional with diverse experiences in multiple industries.",
            GetValidCreateExperiences(1, numberOfExperiences),
            GetValidCreateEducations(1, numberOfEducations),
            GetValidSkills(1, numberOfSkills),
            GetValidLanguages(1, numberOfLanguages)
        );
    }
    
    

    public IEnumerable<CreateExperienceCommand> GetValidCreateExperiences(int start = 1, int count = 10)
    {
        var allExperiences = new[]
        {
            new CreateExperienceCommand("Google", "London", "United Kingdom", DateOnly.Parse("2015-01-01"),
                DateOnly.Parse("2017-01-01"), "Software Engineer working on search algorithms."),
            new CreateExperienceCommand("Microsoft", "Redmond", "United States", DateOnly.Parse("2017-02-01"),
                DateOnly.Parse("2019-02-01"), "Cloud solutions architect for Azure."),
            new CreateExperienceCommand("Amazon", "Seattle", "United States", DateOnly.Parse("2018-03-01"),
                DateOnly.Parse("2020-03-01"), "Operations manager for fulfilment centres."),
            new CreateExperienceCommand("Apple", "Cupertino", "United States", DateOnly.Parse("2019-04-01"),
                DateOnly.Parse("2021-04-01"), "Product manager for iOS applications."),
            new CreateExperienceCommand("Facebook", "Menlo Park", "United States", DateOnly.Parse("2020-05-01"),
                DateOnly.Parse("2022-05-01"), "Data scientist specialising in user behaviour analysis."),
            new CreateExperienceCommand("Tesla", "Palo Alto", "United States", DateOnly.Parse("2021-06-01"),
                DateOnly.Parse("2023-06-01"), "Battery technology researcher."),
            new CreateExperienceCommand("IBM", "Armonk", "United States", DateOnly.Parse("2022-07-01"), null,
                "AI developer for Watson technologies."),
            new CreateExperienceCommand("Intel", "Santa Clara", "United States", DateOnly.Parse("2023-08-01"), null,
                "Processor architecture engineer."),
            new CreateExperienceCommand("LinkedIn", "Sunnyvale", "United States", DateOnly.Parse("2024-09-01"), null,
                "Business development specialist."),
            new CreateExperienceCommand("Twitter", "San Francisco", "United States", DateOnly.Parse("2025-10-01"), null,
                "Content moderation lead.")
        };
        
        if (start < 1)
            throw new ArgumentException("Start index must be greater than or equal to 1.", nameof(start));

        if (start > allExperiences.Length || start + count - 1 > allExperiences.Length)
            throw new ArgumentException("Start and count exceed the number of available experiences.", nameof(count));

        return allExperiences
            .Skip(start - 1).Take(count);
    }

    public IEnumerable<CreateEducationCommand> GetValidCreateEducations(int start = 1, int count = 5)
    {
        var allEducations = new[]
        {
            new CreateEducationCommand("University of Oxford", "Oxford", "United Kingdom", DateOnly.Parse("2010-09-01"),
                DateOnly.Parse("2014-06-01"), "Bachelor of Computer Science", "First Class"),
            new CreateEducationCommand("Stanford University", "Stanford", "United States", DateOnly.Parse("2014-09-01"),
                DateOnly.Parse("2018-06-01"),  "Master of Computer Science", "Distinction"),
            new CreateEducationCommand("Massachusetts Institute of Technology (MIT)", "Cambridge", "United States",
                DateOnly.Parse("2018-09-01"), DateOnly.Parse("2022-06-01"),  "PhD in Artificial Intelligence",
                "High Honours"),
            new CreateEducationCommand("Harvard University", "Cambridge", "United States", DateOnly.Parse("2022-09-01"),
                DateOnly.Parse("2026-06-01"),  "MBA", "Distinction"),
            new CreateEducationCommand("Imperial College London", "London", "United Kingdom",
                DateOnly.Parse("2026-09-01"), null, "MSc in Data Science", "Merit")
        };
        
        if (start < 1)
            throw new ArgumentException("Start index must be greater than or equal to 1.", nameof(start));

        if (start > allEducations.Length || start + count - 1 > allEducations.Length)
            throw new ArgumentException("Start and count exceed the number of available educations.", nameof(count));

        return allEducations
            .Skip(start - 1).Take(count);
    }

    public IEnumerable<string> GetValidSkills(int start=1, int count = 20)
    {
        var allSkills = new[]
        {
            "C#", "Java", "Python", "JavaScript", "SQL", "Cloud Computing", "Machine Learning",
            "Data Analysis", "Cybersecurity", "UI/UX Design", "Project Management", "Agile Development",
            "DevOps", "Networking", "Docker", "Kubernetes", "AI Research", "Blockchain", "IoT Development",
            "Mobile App Development"
        };

        if (start < 1)
            throw new ArgumentException("Start index must be greater than or equal to 1.", nameof(start));

        if (start > allSkills.Length || start + count - 1 > allSkills.Length)
            throw new ArgumentException("Start and count exceed the number of available skills.", nameof(count));

        return allSkills.Skip(start - 1).Take(count);
    }


    public IEnumerable<string> GetValidLanguages(int start=1, int count = 10)
    {
        var allLanguages = new[]
        {
            "English", "Spanish", "Mandarin", "French", "German", "Japanese", "Hindi",
            "Portuguese", "Russian", "Arabic"
        };

        if (start < 1)
            throw new ArgumentException("Start index must be greater than or equal to 1.", nameof(start));

        if (start > allLanguages.Length || start + count - 1 > allLanguages.Length)
            throw new ArgumentException("Start and count exceed the number of available languages.", nameof(count));

        return allLanguages.Skip(start - 1).Take(count);
    }

    public (CreateCvCommand, UpdateCvCommand) GetValidUpdateCvCommand(string email)
    {
        var originalCvCommand = new CreateCvCommand("Original Summary", 
            GetValidCreateExperiences(1, 4),
            GetValidCreateEducations(1, 3),
            GetValidSkills(1, 8), GetValidLanguages(1, 4)
            );
        var updateCvCommand = new UpdateCvCommand(email, "Updated Sumamry",
            GetValidCreateExperiences(3, 5),
            GetValidCreateEducations(3, 2),
            GetValidSkills(4, 4), GetValidLanguages(3, 5)
            );

        return (originalCvCommand, updateCvCommand);
    }
}