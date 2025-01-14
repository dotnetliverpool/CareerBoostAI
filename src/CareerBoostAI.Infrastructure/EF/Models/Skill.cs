namespace CareerBoostAI.Infrastructure.EF.Models;

public class Skill
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public ICollection<Cv> Cvs { get; set; }
}