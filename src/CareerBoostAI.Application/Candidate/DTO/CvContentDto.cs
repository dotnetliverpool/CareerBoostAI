using CareerBoostAI.Application.DTO;

namespace CareerBoostAI.Application.Candidate.DTO;

public class CvContentDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneCode { get; set; }
    public string PhoneNumber { get; set; }
    public string About { get; set; }
    public string? HouseAddress { get; set; } = null;
    public string? City { get;  set; } = null;
    public string? Postcode { get;  set; } = null;
    public string? Country { get;  set; } = null;
    public List<CvSectionDto> Sections { get; set; } = new();

}