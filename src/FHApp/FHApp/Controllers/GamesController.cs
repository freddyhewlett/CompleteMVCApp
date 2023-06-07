using Microsoft.AspNetCore.Mvc;
using FH.App.ViewModels;
using FH.Business.Interfaces;
using AutoMapper;
using FH.Business.Models;
using System.Net;

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

        [Route("game-list")]
        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<GameViewModel>>(await _gameRepository.GetGamesDevelopers()));
        }

        [Route("game-details/{id:guid}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var gameViewModel = await GetGame(id);

            if (gameViewModel == null)
            {
                return NotFound();
            }

            return View(gameViewModel);
        }

        [Route("new-game")]
        public async Task<IActionResult> Create()
        {
            var gameViewModel = await PopulateDeveloper(new GameViewModel());
            return View(gameViewModel);
        }

        [Route("new-game")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GameViewModel gameViewModel)
        {
            gameViewModel = await PopulateDeveloper(gameViewModel);

            if (!ModelState.IsValid)
            {
                return View(gameViewModel);
            }

            var imgPrefix = Guid.NewGuid() + "_";

            if(! await UploadFile(gameViewModel.ImageUpload, imgPrefix))
            {
                return View(gameViewModel);
            }

            gameViewModel.Image = imgPrefix + gameViewModel.ImageUpload.FileName;
            await _gameRepository.Add(_mapper.Map<Game>(gameViewModel));

            return RedirectToAction(nameof(Index));
        }

        [Route("game-update/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var gameViewModel = await GetGame(id);

            if (gameViewModel == null)
            {
                return NotFound();
            }

            return View(gameViewModel);
        }

        [Route("game-update/{id:guid}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, GameViewModel gameViewModel)
        {
            if (id != gameViewModel.Id)
            {
                return NotFound();
            }

            var gameUpdate = await GetGame(id);
            gameViewModel.Developer = gameUpdate.Developer;
            gameViewModel.Image = gameUpdate.Image;

            if (!ModelState.IsValid)
            {
                return View(gameViewModel);
                
            }

            if(gameViewModel.ImageUpload != null)
            {
                var imgPrefix = Guid.NewGuid() + "_";

                if (!await UploadFile(gameViewModel.ImageUpload, imgPrefix))
                {
                    return View(gameViewModel);
                }

                gameUpdate.Image = imgPrefix + gameViewModel.ImageUpload.FileName;
            }

            gameUpdate.Name = gameViewModel.Name;
            gameUpdate.Description = gameViewModel.Description;
            gameUpdate.Value = gameViewModel.Value;
            gameUpdate.Active = gameViewModel.Active;

            await _gameRepository.Update(_mapper.Map<Game>(gameUpdate));

            return RedirectToAction(nameof(Index));
        }

        [Route("game-remove/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var game = await GetGame(id);

            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }

        [Route("game-remove/{id:guid}")]
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

        private async Task<bool> UploadFile(IFormFile file, string imgPrefix)
        {
            if (file.Length <= 0) return false;

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", imgPrefix + file.FileName);

            if(System.IO.File.Exists(path))
            {
                ModelState.AddModelError(string.Empty, "Já existe um arquivo com este nome!");
                return false;
            }

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return true;
        }
    }
}
