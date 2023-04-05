using FH.Business.Models;
using System;

namespace FH.Business.Interfaces
{
    public interface IGameRepository : IRepository<Game>
    {
        Task<IEnumerable<Game>> GetGamesByDeveloper(Guid developerId);
        Task<IEnumerable<Game>> GetGamesDevelopers();
        Task<Game> GetGameDeveloper(Guid id);
    }
}
