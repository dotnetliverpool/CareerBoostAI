namespace CareerBoostAI.Infrastructure.EF.Models;

public class CvLanguage
{
    public Guid CvId { get; set; }
    public Cv Cv { get; set; }

    public Guid LanguageId { get; set; }
    public Language Language { get; set; }
}