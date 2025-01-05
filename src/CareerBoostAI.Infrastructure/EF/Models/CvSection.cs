using System.ComponentModel.DataAnnotations;

namespace CareerBoostAI.Infrastructure.EF.Models;

public class CvSection
{
    public Guid Id { get; set; }
    
    [MaxLength(100)]
    public string Name { get; set; }
    
    public int SequenceIndex { get; set; }
    
    public List<CvSectionItem> SectionItems { get; set; }
    
    public int CvId { get; set; }
    public Cv Cv { get; set; }
}