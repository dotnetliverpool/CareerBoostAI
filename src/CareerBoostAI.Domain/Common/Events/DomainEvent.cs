namespace CareerBoostAI.Domain.Common.Events;

public class DomainEvent
{
    public static EmailRegisteredEvent EmailRegistered
       (Guid candidateId, string email ) => new(candidateId, email);
    
    public static PhoneNumberRegisteredEvent PhoneNumberRegistered
        (Guid candidateId, string code, string phoneNumber) 
            => new(candidateId, code, phoneNumber);
}