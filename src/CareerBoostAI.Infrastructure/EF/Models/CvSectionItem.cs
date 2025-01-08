using System.ComponentModel.DataAnnotations;

namespace CareerBoostAI.Infrastructure.EF.Models;

public class CvSectionItem
{
    public Guid Id { get; set; }
    
    public uint SequenceIndex { get; set; }
    
    [MaxLength(255)]
    public string OrganisationName { get; set; }
    
    [MaxLength(255)]
    public string Country { get; set; } 
    
    [MaxLength(255)]
    public string City { get; set; }
    
    [MaxLength(3000)]
    public string Description { get; set; }
    
    [DataType(DataType.Date)]
    public DateOnly StartDate { get; set; }
    
    [DataType(DataType.Date)]
    public DateOnly? EndDate { get; set; }
    
    public Guid SectionId { get; set; }
    public CvSection Section { get; set; }
}