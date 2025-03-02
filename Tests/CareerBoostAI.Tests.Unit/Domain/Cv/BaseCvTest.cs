using CareerBoostAI.Domain.CvContext.Factory;

namespace CareerBoostAI.Tests.Unit.Domain.Cv;

public abstract class BaseCvTest
{
    protected ICvFactory GetCvFactory()
    {
        return new CvFactory();
    }

    protected CvData GetValidCvData()
    {
        return new CvData
        {
            Summary = GetValidCvSummary(),
            Experiences = GetValidCvExperiences(),
            Educations = GetValidCvEducations(),
            Skills = GetValidCvSkills(),
            Languages = GetValidCvLanguages()
        };
    }

    protected string GetValidCvSummary()
    {
        return "A highly skilled and motivated software developer with a strong background in designing, developing, " +
               "and deploying scalable and maintainable software solutions. Passionate about leveraging technology to" +
               " solve complex problems and create impactful digital experiences. Over the years, I have demonstrated " +
               "expertise in full-stack development, utilising modern frameworks and tools such as ASP.NET Core, " +
               "Angular, and SQL Server to deliver high-quality enterprise-level applications.\n\nI excel in analysing requirements, " +
               "crafting robust software architectures, and leading projects through their full lifecycle, from " +
               "inception to deployment and beyond. I have hands-on experience in implementing Domain-Driven Design (DDD)," +
               " Test-Driven Development (TDD), and clean coding principles to ensure maintainability and scalability in software solutions.";
    }

    protected List<ExperienceData> GetValidCvExperiences(uint numberOfExperiences = 10)
    {
        var experiences = new List<ExperienceData>
        {
            new()
            {
                OrganisationName = "TechCorp",
                City = "London",
                Country = "UK",
                StartDate = new DateOnly(2018, 6, 1),
                EndDate = new DateOnly(2022, 8, 31),
                Description = "Developed enterprise-level applications."
            },
            new()
            {
                OrganisationName = "InnoSoft",
                City = "Manchester",
                Country = "UK",
                StartDate = new DateOnly(2015, 3, 1),
                EndDate = new DateOnly(2018, 5, 31),
                Description = "Designed cloud-based systems and optimised software processes."
            },
            new()
            {
                OrganisationName = "Global Solutions",
                City = "Birmingham",
                Country = "UK",
                StartDate = new DateOnly(2012, 8, 1),
                EndDate = new DateOnly(2015, 2, 28),
                Description = "Worked on full-stack application development for logistics."
            },
            new()
            {
                OrganisationName = "WebCraft",
                City = "Edinburgh",
                Country = "UK",
                StartDate = new DateOnly(2010, 1, 1),
                EndDate = new DateOnly(2012, 7, 31),
                Description = "Specialised in front-end development and UI design."
            },
            new()
            {
                OrganisationName = "DataCorp",
                City = "Leeds",
                Country = "UK",
                StartDate = new DateOnly(2008, 5, 1),
                EndDate = new DateOnly(2009, 12, 31),
                Description = "Maintained and enhanced database systems."
            },
            new()
            {
                OrganisationName = "SoftVision",
                City = "Cardiff",
                Country = "UK",
                StartDate = new DateOnly(2006, 9, 1),
                EndDate = new DateOnly(2008, 4, 30),
                Description = "Developed scalable APIs for financial services."
            },
            new()
            {
                OrganisationName = "TechVista",
                City = "Liverpool",
                Country = "UK",
                StartDate = new DateOnly(2004, 3, 1),
                EndDate = new DateOnly(2006, 8, 31),
                Description = "Implemented DevOps pipelines for continuous integration."
            },
            new()
            {
                OrganisationName = "CyberLogic",
                City = "Oxford",
                Country = "UK",
                StartDate = new DateOnly(2002, 7, 1),
                EndDate = new DateOnly(2004, 2, 28),
                Description = "Developed cybersecurity solutions for enterprise clients."
            },
            new()
            {
                OrganisationName = "BrightTech",
                City = "Glasgow",
                Country = "UK",
                StartDate = new DateOnly(2000, 1, 1),
                EndDate = new DateOnly(2002, 6, 30),
                Description = "Focused on network management and infrastructure."
            },
            new()
            {
                OrganisationName = "StartTech",
                City = "Bristol",
                Country = "UK",
                StartDate = new DateOnly(1998, 9, 1),
                EndDate = new DateOnly(1999, 12, 31),
                Description = "Worked as a junior developer creating internal tools."
            }
        };

        return experiences.Take((int)Math.Min(numberOfExperiences, 10)).ToList();
    }


    protected List<EducationData> GetValidCvEducations(uint numberOfEducations = 4)
    {
        var educations = new List<EducationData>
        {
            new()
            {
                OrganisationName = "University of Cambridge",
                City = "Cambridge",
                Country = "UK",
                StartDate = new DateOnly(2010, 9, 1),
                EndDate = new DateOnly(2014, 6, 30),
                Program = "BSc Computer Science",
                Grade = "First Class"
            },
            new()
            {
                OrganisationName = "Imperial College London",
                City = "London",
                Country = "UK",
                StartDate = new DateOnly(2008, 9, 1),
                EndDate = new DateOnly(2010, 8, 31),
                Program = "MSc Artificial Intelligence",
                Grade = "Distinction"
            },
            new()
            {
                OrganisationName = "Oxford Brookes University",
                City = "Oxford",
                Country = "UK",
                StartDate = new DateOnly(2004, 9, 1),
                EndDate = new DateOnly(2008, 6, 30),
                Program = "BEng Software Engineering",
                Grade = "Upper Second Class"
            },
            new()
            {
                OrganisationName = "High School of Technology",
                City = "Manchester",
                Country = "UK",
                StartDate = new DateOnly(2002, 9, 1),
                EndDate = new DateOnly(2004, 7, 31),
                Program = "A-Levels in Maths, Physics, and Computer Science",
                Grade = "AAA"
            }
        };

        return educations.Take((int)Math.Min(numberOfEducations, 4)).ToList();
    }


    protected List<string> GetValidCvSkills(uint numberOfSkills = 20)
    {
        var skills = new List<string>
        {
            "C#", "ASP.NET Core", "SQL", "Azure", "React", "Docker",
            "Kubernetes", "TypeScript", "Python", "Java",
            "Microservices Architecture", "RESTful APIs", "GraphQL",
            "Git", "CI/CD Pipelines", "Agile Methodologies",
            "Cloud Security", "Machine Learning", "DevOps", "TDD"
        };

        return skills.Take((int)Math.Min(numberOfSkills, 20)).ToList();
    }


    protected List<string> GetValidCvLanguages(uint numberOfLanguages = 10)
    {
        var languages = new List<string>
        {
            "English", "Spanish", "French", "German", "Mandarin",
            "Japanese", "Portuguese", "Italian", "Russian", "Arabic"
        };

        return languages.Take((int)Math.Min(numberOfLanguages, 10)).ToList();
    }
}