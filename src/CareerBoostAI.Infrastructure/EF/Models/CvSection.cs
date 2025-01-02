namespace CareerBoostAI.Infrastructure.EF.Models;

public class CvSection
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int SortOrder { get; set; }
    public List<CvSectionItem> SectionItems { get; set; }
    
    public int CvId { get; set; }
    public Cv Cv { get; set; }
}