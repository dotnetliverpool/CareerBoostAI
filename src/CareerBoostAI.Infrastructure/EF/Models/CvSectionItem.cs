namespace CareerBoostAI.Infrastructure.EF.Models;

public class CvSectionItem
{
    public int Id { get; set; }
    public string OrganisationName { get; set; }
    public string Country { get; set; } 
    public string City { get; set; }
    public string Description { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
    
    public int SectionId { get; set; }
    public CvSection Section { get; set; }
}