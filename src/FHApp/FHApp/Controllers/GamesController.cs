using Microsoft.AspNetCore.Mvc;
using FH.App.ViewModels;
using FH.Business.Interfaces;
using AutoMapper;
using FH.Business.Models;

namespace FH.App.Controllers
{
    public class GamesController : BaseController
    {
        private readonly IGameRepository _gameRepository;
        private readonly IDeveloperRepository _developerRepository;
        private readonly IMapper _mapper;

        public GamesController(IGameRepository gameRepository, IMapper mapper, IDeveloperRepository developerRepository)
        {
            _gameRepository = gameRepository;
            _mapper = mapper;
            _developerRepository = developerRepository;
        }

        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<GameViewModel>>(await _gameRepository.GetGamesDevelopers()));
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var gameViewModel = await GetGame(id);

            if (gameViewModel == null)
            {
                return NotFound();
            }

            return View(gameViewModel);
        }

        public async Task<IActionResult> Create()
        {
            var gameViewModel = await PopulateDeveloper(new GameViewModel());
            return View(gameViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GameViewModel gameViewModel)
        {
            gameViewModel = await PopulateDeveloper(gameViewModel);

            if (!ModelState.IsValid)
            {
                return View(gameViewModel);
            }

            await _gameRepository.Add(_mapper.Map<Game>(gameViewModel));
            return View(gameViewModel);
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var gameViewModel = await GetGame(id);

            if (gameViewModel == null)
            {
                return NotFound();
            }

            return View(gameViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, GameViewModel gameViewModel)
        {
            if (id != gameViewModel.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(gameViewModel);
                
            }

            await _gameRepository.Update(_mapper.Map<Game>(gameViewModel));

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var game = await GetGame(id);

            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var game = await GetGame(id);

            if (game == null)
            {
                return NotFound();
            }

            await _gameRepository.Remove(id);

            return RedirectToAction(nameof(Index));
        }

        private async Task<GameViewModel> GetGame(Guid id)
        {
            var game = _mapper.Map<GameViewModel>(await _gameRepository.GetGameDeveloper(id));
            game.Developers = _mapper.Map<IEnumerable<DeveloperViewModel>>(await _developerRepository.GetAll());
            return game;
        }

        private async Task<GameViewModel> PopulateDeveloper(GameViewModel gameViewModel)
        {
            gameViewModel.Developers = _mapper.Map<IEnumerable<DeveloperViewModel>>(await _developerRepository.GetAll());
            return gameViewModel;
        }
    }
}
