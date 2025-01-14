using System.ComponentModel.DataAnnotations;

namespace CareerBoostAI.Infrastructure.EF.Models;

public class Education
{
    public Guid Id { get; set; }
    
    [Required]
    public string OrganisationName { get; set; }
    
    [Required]
    public string City { get; set; }
    
    [Required]
    public string Country { get; set; }
    
    [Required]
    public DateOnly StartDate { get; set; }
    
    public DateOnly? EndDate { get; set; }
    
    [Required]
    public string Program { get; set; }
    
    [Required]
    public string Grade { get; set; }
    
    [Required]
    public uint SequenceIndex { get; set; }

    public Guid CvId { get; set; }
    public Cv Cv { get; set; }
}