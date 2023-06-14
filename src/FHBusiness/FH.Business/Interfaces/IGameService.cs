using FH.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FH.Business.Interfaces
{
    public interface IGameService
    {
        Task Add(Game game);
        Task Update(Game game);
        Task Remove(Guid id);
    }
}
