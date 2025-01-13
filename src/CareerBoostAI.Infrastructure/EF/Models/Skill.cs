namespace CareerBoostAI.Infrastructure.EF.Models;

public class Skill
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public ICollection<CvSkill> CvSkills { get; set; }
}