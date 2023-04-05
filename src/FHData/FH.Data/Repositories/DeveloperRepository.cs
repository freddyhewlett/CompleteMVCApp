using FH.Business.Interfaces;
using FH.Business.Models;
using FH.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;

namespace FH.Data.Repositories
{
    public class DeveloperRepository : Repository<Developer>, IDeveloperRepository
    {
        public DeveloperRepository(DataDbContext context) : base(context)
        {
        }

        public async Task<Developer> GetDeveloperAddress(Guid id)
        {
            return await _dbContext.Developers.AsNoTracking()
                .Include(d => d.Address)
                .FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task<Developer> GetDeveloperGamesAddress(Guid id)
        {
            return await _dbContext.Developers.AsNoTracking()
                .Include(p => p.Games)
                .Include(p => p.Address)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
