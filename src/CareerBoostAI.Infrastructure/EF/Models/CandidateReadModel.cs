using System.ComponentModel.DataAnnotations;

namespace CareerBoostAI.Infrastructure.EF.Models;

public class CandidateReadModel
{
    public Guid Id { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string FirstName { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string LastName { get; set; }
    
    [Required]
    [DataType(DataType.Date)]
    public DateOnly DateOfBirth { get; set; }
    [Required]
    [MaxLength(100)]
    public string Email { get; set; }
    
    [Required]
    [MaxLength(30)]
    public string PhoneNumber { get; set; }
    public ICollection<UploadReadModel> Uploads { get; set; }
    
    public CvReadModel CvReadModel { get; set; }
}