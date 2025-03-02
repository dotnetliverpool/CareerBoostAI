namespace CareerBoostAI.Infrastructure.EF.Models;

public class CvSkill
{
    public Guid CvId { get; set; }
    public CvReadModel CvReadModel { get; set; }

    public Guid SkillId { get; set; }
    public SkillReadModel SkillReadModel { get; set; }
}