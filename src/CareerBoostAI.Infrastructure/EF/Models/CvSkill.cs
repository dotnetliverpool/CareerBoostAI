using System.ComponentModel.DataAnnotations;

namespace CareerBoostAI.Infrastructure.EF.Models;

public class CvSkill
{
    [Required]
    public Guid CvId { get; set; }
    public Cv Cv { get; set; }
    
    [Required]
    public Guid SkillId { get; set; }
    public Skill Skill { get; set; }
}