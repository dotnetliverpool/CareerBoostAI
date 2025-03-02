namespace CareerBoostAI.Domain.CandidateContext.Services;

public interface ICandidateProfileUpdateDomainService
{
    public void Update(Candidate candidate, 
        string firstName, string lastName,
        DateOnly dateOfBirth,
        string phoneCode, string phoneNumber);
}