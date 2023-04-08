using Microsoft.AspNetCore.Mvc;
using FH.App.ViewModels;
using FH.Business.Interfaces;
using AutoMapper;
using FH.Business.Models;

namespace FH.App.Controllers
{
    public class DevelopersController : BaseController
    {
        private readonly IDeveloperRepository _developerRepository;
        private readonly IMapper _mapper;

        public DevelopersController(IDeveloperRepository developerRepository, IMapper mapper)
        {
            _developerRepository = developerRepository;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
              return View(_mapper.Map<IEnumerable<DeveloperViewModel>>(await _developerRepository.GetAll()));
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var developerViewModel = await GetDeveloperAddress(id);

            if (developerViewModel == null)
            {
                return NotFound();
            }

            return View(developerViewModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DeveloperViewModel developerViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(developerViewModel);
            }

            var developer = _mapper.Map<Developer>(developerViewModel);
            await _developerRepository.Add(developer);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var developerViewModel = await GetDeveloperGamesAddress(id);

            if (developerViewModel == null)
            {
                return NotFound();
            }

            return View(developerViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, DeveloperViewModel developerViewModel)
        {
            if (id != developerViewModel.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(developerViewModel);
            }

            var developer = _mapper.Map<Developer>(developerViewModel);
            await _developerRepository.Update(developer);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var developerViewModel = await GetDeveloperAddress(id);

            if (developerViewModel == null)
            {
                return NotFound();
            }

            return View(developerViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var developerViewModel = await GetDeveloperAddress(id);

            if (developerViewModel == null)
            {
                return NotFound();
            }

            await _developerRepository.Remove(id);

            return RedirectToAction(nameof(Index));
        }

        private async Task<DeveloperViewModel> GetDeveloperAddress(Guid id)
        {
            return _mapper.Map<DeveloperViewModel>(await _developerRepository.GetDeveloperAddress(id));
        }

        private async Task<DeveloperViewModel> GetDeveloperGamesAddress(Guid id)
        {
            return _mapper.Map<DeveloperViewModel>(await _developerRepository.GetDeveloperGamesAddress(id));
        }
    }
}
