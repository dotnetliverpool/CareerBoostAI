﻿using CareerBoostAI.Application.Common.Abstractions;
using CareerBoostAI.Infrastructure.EF.Contexts;

namespace CareerBoostAI.Infrastructure.EF.Transaction;

internal sealed class UnitOfWork(CareerBoostWriteDbContext context) : IUnitOfWork
{
    private readonly CareerBoostWriteDbContext _context = context;

    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
       return _context.SaveChangesAsync(cancellationToken);
    }
}