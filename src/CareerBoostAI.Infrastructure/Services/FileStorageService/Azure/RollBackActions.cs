using CareerBoostAI.Application.Common.Abstractions.Transaction;
using CareerBoostAI.Application.Services;
using Microsoft.Extensions.Logging;

namespace CareerBoostAI.Infrastructure.Services.FileStorageService.Azure;

internal class RollBackActions
{
    
}

internal class UploadDocumentRollBackAction(
    IStorageService storageService, string storageAddress) : IRollBackAction
{
    public Task RollBackAsync(CancellationToken cancellationToken)
    {
        return storageService.DeleteAsync(storageAddress, cancellationToken);
    }
}