using CareerBoostAI.Domain.Common.Services;

namespace CareerBoostAI.Domain.CandidateContext.Services;

public class CandidateProfileUpdateService(IDateTimeProvider dateTimeProvider) : ICandidateProfileUpdateDomainService
{
    public void Update(Candidate candidate, string firstName, string lastName, DateOnly dateOfBirth, string phoneCode,
        string phoneNumber)
    {
        candidate.UpdateName(firstName, lastName);
        candidate.UpdateDateOfBirth(dateOfBirth, dateTimeProvider);
        candidate.UpdatePhoneNumber(phoneCode, phoneNumber);
    }
}