namespace CareerBoostAI.Infrastructure.EF.Models;

public class Upload
{
    public Guid Id;
    public string FileName;
    public string Extension;
    public string StorageMedium;
    public string StorageAddress;
    public Guid CandidateId;
    public Candidate Candidate;
}