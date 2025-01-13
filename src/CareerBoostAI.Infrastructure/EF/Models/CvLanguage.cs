using System.ComponentModel.DataAnnotations;

namespace CareerBoostAI.Infrastructure.EF.Models;

public class CvLanguage
{
    [Required]
    public Guid CvId { get; set; }
    public Cv Cv { get; set; }
    
    [Required]
    public Guid LanguageId { get; set; }
    public Language Language { get; set; }
}