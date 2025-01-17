namespace CareerBoostAI.Infrastructure.EF.Models;

public class SkillReadModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public ICollection<CvReadModel> Cvs { get; set; }
}