using CareerBoostAI.Application.Common.Abstractions.Mediator;

namespace CareerBoostAI.Application.Candidate.Commands.UploadCvDocument;

public sealed record UploadCvDocumentCommand(
    string Email, string DocumentName,
    Stream DocumentStream) : ICommand;

