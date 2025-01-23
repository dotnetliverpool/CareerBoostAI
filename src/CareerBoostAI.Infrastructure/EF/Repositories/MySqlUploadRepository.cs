using CareerBoostAI.Domain.UploadContext;
using CareerBoostAI.Infrastructure.EF.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CareerBoostAI.Infrastructure.EF.Repositories;

public class MySqlUploadRepository(CareerBoostWriteDbContext context) : IUploadRepository
{
    private readonly DbSet<Upload> _uploads = context.Uploads;
    private readonly CareerBoostWriteDbContext _context = context;
    public async Task CreateNewAsync(Upload upload, CancellationToken cancellationToken)
    {
        await _uploads.AddAsync(upload, cancellationToken);
    }
}