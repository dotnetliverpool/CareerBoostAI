namespace CareerBoostAI.Infrastructure.EF.Models;

public class CvLanguage
{
    public Guid CvId { get; set; }
    public CvReadModel CvReadModel { get; set; }

    public Guid LanguageId { get; set; }
    public LanguageReadModel LanguageReadModel { get; set; }
}