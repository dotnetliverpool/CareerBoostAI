using CareerBoostAI.Application.Candidate.DTO;

namespace CareerBoostAI.Application.DTO;

public class CvDto
{
    public Guid Id { get; set; }
    public string FileName { get; set; }
    public string Storagemedium { get; set; }
    public string StorageAddress  { get; set; }
    public CvContentDto? Content { get; set; } = null;
}