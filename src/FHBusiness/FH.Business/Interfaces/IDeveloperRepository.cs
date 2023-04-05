using FH.Business.Models;
using System;

namespace FH.Business.Interfaces
{
    public interface IDeveloperRepository : IRepository<Developer>
    {
        Task<Developer> GetDeveloperAddress(Guid id);
        Task<Developer> GetDeveloperGamesAddress(Guid id);
    }
}
