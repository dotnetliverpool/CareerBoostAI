namespace CareerBoostAI.Infrastructure.EF.Models;

public class CvSkill
{
    public Guid CvId { get; set; }
    public Cv Cv { get; set; }

    public Guid SkillId { get; set; }
    public Skill Skill { get; set; }
}