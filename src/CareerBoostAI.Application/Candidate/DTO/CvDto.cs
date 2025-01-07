namespace CareerBoostAI.Application.Candidate.DTO;

public class CvDto
{
    public Guid Id { get; set; }
    public string FileName { get; set; }
    public string Storagemedium { get; set; }
    public string StorageAddress  { get; set; }
    public CvContentDto? Content { get; set; } = null;
}