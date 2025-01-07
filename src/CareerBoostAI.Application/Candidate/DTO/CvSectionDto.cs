namespace CareerBoostAI.Application.Candidate.DTO;

public class CvSectionDto
{
    public string SectionName { get; set; }
    public int SequenceIndex { get; set; }
    public List<CvSectionItemDto> Items { get; set; }
}