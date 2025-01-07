namespace CareerBoostAI.Application.Candidate.DTO;

public class CvSectionDto
{
    public string SectionName { get; set; }
    public uint SequenceIndex { get; set; }
    public List<CvSectionItemDto> Items { get; set; } = new();
}