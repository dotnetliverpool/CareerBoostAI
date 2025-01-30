using CareerBoostAI.Application.Services.EmailService;

namespace CareerBoostAI.Application.Notifications;

public class CandidateNotifications
{
    
}

public sealed class CandidateProfileCreatedNotification(Guid candidateId, string candidateEmail)
    : IApplicationNotification
{
    public string GetMessage()
    {
        return $"A new profile has been created for candidate: \n" +
               $"Id [{candidateId}]\n" +
               $"Email [{candidateEmail}]";
    }
}

public sealed class CandidateProfileUpdatedNotification(Guid candidateId, string candidateEmail)
    : IApplicationNotification
{
    public string GetMessage()
    {
        return $"A candidate has just updated their profile data: \n" +
               $"Id [{candidateId}]\n" +
               $"Email [{candidateEmail}]";
    }
}