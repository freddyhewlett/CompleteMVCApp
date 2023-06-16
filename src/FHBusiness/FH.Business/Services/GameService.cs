using FH.Business.Interfaces;
using FH.Business.Models;
using FH.Business.Models.Validations.Docs;

namespace FH.Business.Services
{
    public class GameService : BaseService, IGameService
    {
        private readonly IGameRepository _gameRepository;
        public GameService(IGameRepository gameRepository, INotificator notificator) : base(notificator)
        {
            _gameRepository = gameRepository;
        }

        public async Task Add(Game game)
        {
            if (!ExecuteValidation(new GameValidation(), game)) return;

            await _gameRepository.Add(game);
        }

        public async Task Remove(Guid id)
        {
            await _gameRepository.Remove(id);
        }

        public async Task Update(Game game)
        {
            if (!ExecuteValidation(new GameValidation(), game)) return;

            await _gameRepository.Update(game);
        }

        public void Dispose()
        {
            _gameRepository?.Dispose();
        }
    }
}
