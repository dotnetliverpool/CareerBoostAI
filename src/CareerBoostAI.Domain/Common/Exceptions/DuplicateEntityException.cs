using CareerBoostAI.Domain.Common.Abstractions;

namespace CareerBoostAI.Domain.Common.Exceptions;

public class DuplicateEntityException(string id) : CareerBoostAIDomainException($"Entity {id} already exists.");