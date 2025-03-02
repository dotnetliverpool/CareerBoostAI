using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CareerBoostAI.Infrastructure.EF.Models;

public class CvReadModel
{
    public Guid Id { get; set; }
    
    [MaxLength(2000)]
    public string Summary { get; set; }
    
    public ICollection<ExperienceReadModel> Experiences { get; set; }
    public ICollection<EducationReadModel> Educations { get; set; }
    public ICollection<SkillReadModel> Skills { get; set; }
    public ICollection<LanguageReadModel> Languages { get; set; }
    public string CandidateEmail { get; set; }
    public CandidateReadModel CandidateReadModel { get; set; }
}