namespace CareerBoostAI.Domain.CandidateContext;

public interface ICandidateRepository
{
    Task CreateNewAsync(Candidate candidate);
}