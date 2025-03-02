namespace CareerBoostAI.Domain.CandidateContext;

public interface ICandidateRepository
{
    Task CreateNewAsync(Candidate candidate, CancellationToken cancellationToken);
    
    Task<Candidate?> GetByEmailAsync(string email, CancellationToken cancellationToken);
    
    Task UpdateAsync(Candidate candidate, CancellationToken cancellationToken);
}