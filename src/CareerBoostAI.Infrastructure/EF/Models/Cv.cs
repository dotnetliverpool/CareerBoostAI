using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CareerBoostAI.Infrastructure.EF.Models;

public class Cv
{
    public Guid Id { get; set; }
    
    [MaxLength(2000)]
    public string Summary { get; set; }
    
    public ICollection<Experience> Experiences { get; set; }
    public ICollection<Education> Educations { get; set; }
    public ICollection<CvSkill> CvSkills { get; set; }
    public ICollection<CvLanguage> CvLanguages { get; set; }
    
    public Guid CandidateId { get; set; }
    public Candidate Candidate { get; set; }
}