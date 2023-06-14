using FH.Business.Interfaces;
using FH.Business.Models;
using FH.Business.Models.Validations.Docs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FH.Business.Services
{
    public class GameService : BaseService, IGameService
    {
        public async Task Add(Game game)
        {
            if (!ExecuteValidation(new GameValidation(), game)) return;
        }

        public Task Remove(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task Update(Game game)
        {
            if (!ExecuteValidation(new GameValidation(), game)) return;
        }
    }
}
