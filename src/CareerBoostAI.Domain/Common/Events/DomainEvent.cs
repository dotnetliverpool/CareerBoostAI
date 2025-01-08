namespace CareerBoostAI.Domain.Common.Events;

public class DomainEvent
{
    public static EmailRegisteredEvent EmailRegistered
       (Guid candidateId, string email ) => new(candidateId, email);
    
    public static PhoneNumberRegisteredEvent PhoneNumberRegistered
        (Guid cadidateId, string code, string phoneNumber) => new(cadidateId, code, phoneNumber);
}