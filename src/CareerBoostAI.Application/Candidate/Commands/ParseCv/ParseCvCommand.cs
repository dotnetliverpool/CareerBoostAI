using CareerBoostAI.Application.Candidate.DTO;
using CareerBoostAI.Application.Common.Abstractions.Mediator;
using CareerBoostAI.Application.Services.CvParseService;

namespace CareerBoostAI.Application.Candidate.Commands.ParseCv;

public sealed record ParseCvCommand(Stream DocumentContent, string DocumentName)
    : ICommand<ParsedCv>;
