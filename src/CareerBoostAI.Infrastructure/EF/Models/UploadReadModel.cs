using System.ComponentModel.DataAnnotations;

namespace CareerBoostAI.Infrastructure.EF.Models;

public class UploadReadModel
{
    public Guid Id { get; set; }
    public string FileName { get; set; }
    public string Extension { get; set; }
    public string StorageMedium { get; set; }
    public string StorageAddress { get; set; }
    public Guid CandidateId { get; set; }
    public CandidateReadModel CandidateReadModel { get; set; }
}