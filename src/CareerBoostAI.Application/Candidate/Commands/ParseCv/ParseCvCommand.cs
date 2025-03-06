using CareerBoostAI.Application.Candidate.DTO;
using CareerBoostAI.Application.Common.Abstractions.Mediator;

namespace CareerBoostAI.Application.Candidate.Commands.ParseCv;

public sealed record ParseCvCommand(string DocumentName, Stream DocumentStream)
    : ICommand<ParsedCvDocumentDto>;
