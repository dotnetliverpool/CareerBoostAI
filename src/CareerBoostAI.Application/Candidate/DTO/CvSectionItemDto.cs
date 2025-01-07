namespace CareerBoostAI.Application.Candidate.DTO;

public class CvSectionItemDto
{
    public string OrganisationName { get; set; }
    public string OrganisationCity { get; set; }
    public string OrganisationCountry { get; set; }
    public string Description { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly? EndDate { get; set; } = null;
    public uint SequenceIndex { get; set; }
}