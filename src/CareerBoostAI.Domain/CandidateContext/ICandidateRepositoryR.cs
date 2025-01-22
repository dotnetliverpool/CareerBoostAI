namespace CareerBoostAI.Domain.CandidateContext;

public interface ICandidateRepositoryR
{
    Task CreateNewAsync(Candidate candidate);
}