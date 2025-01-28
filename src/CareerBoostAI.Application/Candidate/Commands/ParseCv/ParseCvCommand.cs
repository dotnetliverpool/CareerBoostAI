using CareerBoostAI.Application.Candidate.DTO;
using CareerBoostAI.Application.Common.Abstractions.Mediator;
using CareerBoostAI.Application.Services.CvParseService;

namespace CareerBoostAI.Application.Candidate.Commands.ParseCv;

public sealed record ParseCvCommand(string DocumentName, Stream DocumentContent)
    : ICommand<ParsedCvDocumentDto>;
