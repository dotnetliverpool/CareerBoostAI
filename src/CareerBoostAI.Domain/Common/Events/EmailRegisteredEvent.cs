using CareerBoostAI.Domain.Abstractions;

namespace CareerBoostAI.Domain.Common.Events;

public record EmailRegisteredEvent(Guid CandidateId, string Email) : IDomainEvent;
