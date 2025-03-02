using System.ComponentModel.DataAnnotations;

namespace CareerBoostAI.Infrastructure.EF.Models;

public class EducationReadModel
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
    

    public Guid CvId { get; set; }
    public CvReadModel CvReadModel { get; set; }
}