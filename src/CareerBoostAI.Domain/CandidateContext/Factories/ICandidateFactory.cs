using CareerBoostAI.Domain.Candidate;

namespace CareerBoostAI.Domain.CandidateContext.Factories;

public interface ICandidateFactory
{
    public Candidate Create(string firstName,
        string lastName, DateOnly dateOfBirth,
        string email, string phoneCode, string phoneNumber);

}