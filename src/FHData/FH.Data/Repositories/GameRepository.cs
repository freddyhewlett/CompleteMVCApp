using FH.Business.Interfaces;
using FH.Business.Models;
using FH.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;

namespace FH.Data.Repositories
{
    public class GameRepository : Repository<Game>, IGameRepository
    {
        public GameRepository(DataDbContext context) : base(context)
        {
        }

        public async Task<Game> GetGameDeveloper(Guid id)
        {
            return await _dbContext.Games.AsNoTracking().Include(d => d.Developer)
                .FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task<IEnumerable<Game>> GetGamesByDeveloper(Guid developerId)
        {
            return await Search(g => g.DeveloperId == developerId);
        }

        public async Task<IEnumerable<Game>> GetGamesDevelopers()
        {
            return await _dbContext.Games.AsNoTracking().Include(d => d.Developer)
                .OrderBy(g => g.Name).ToListAsync();
        }
    }
}
