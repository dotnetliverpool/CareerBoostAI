using System.ComponentModel.DataAnnotations;

namespace CareerBoostAI.Infrastructure.EF.Models;

public class Experience
{
    public Guid Id;
    [Required]
    public string OrganisationName;
    [Required]
    public string City;
    [Required]
    public string Country;
    [Required]
    public DateOnly StartDate;
    public DateOnly? EndDate;
    [Required]
    public string Description;
    [Required]
    public uint Index;

    public Guid CvId;
    public Cv Cv;
}