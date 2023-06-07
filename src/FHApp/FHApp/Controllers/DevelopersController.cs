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
        private readonly IAddressRepository _addressRepository;
        private readonly IMapper _mapper;

        public DevelopersController(IDeveloperRepository developerRepository, IMapper mapper, IAddressRepository addressRepository)
        {
            _developerRepository = developerRepository;
            _mapper = mapper;
            _addressRepository = addressRepository;
        }

        [Route("developer-list")]
        public async Task<IActionResult> Index()
        {
              return View(_mapper.Map<IEnumerable<DeveloperViewModel>>(await _developerRepository.GetAll()));
        }

        [Route("developer-details/{id:guid}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var developerViewModel = await GetDeveloperAddress(id);

            if (developerViewModel == null)
            {
                return NotFound();
            }

            return View(developerViewModel);
        }

        [Route("new-developer")]
        public IActionResult Create()
        {
            return View();
        }

        [Route("new-developer")]
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

        [Route("developer-update/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var developerViewModel = await GetDeveloperGamesAddress(id);

            if (developerViewModel == null)
            {
                return NotFound();
            }

            return View(developerViewModel);
        }

        [Route("developer-update/{id:guid}")]
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

        [Route("developer-remove/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var developerViewModel = await GetDeveloperAddress(id);

            if (developerViewModel == null)
            {
                return NotFound();
            }

            return View(developerViewModel);
        }

        [Route("developer-remove/{id:guid}")]
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

        [Route("developer-address-details/{id:guid}")]
        public async Task<IActionResult> GetAddress(Guid id)
        {
            var developer = await GetDeveloperAddress(id);

            if (developer == null)
            {
                return NotFound();
            }

            return PartialView("_AddressDetails", developer);
        }

        [Route("developer-address-update/{id:guid}")]
        public async Task<IActionResult> UpdateAddress(Guid id)
        {
            var developer = await GetDeveloperAddress(id);

            if (developer == null)
            {
                return NotFound();
            }

            return PartialView("_UpdateAddress", new DeveloperViewModel { Address = developer.Address });
        }

        [Route("developer-address-update/{id:guid}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateAddress(DeveloperViewModel developerViewModel)
        {
            ModelState.Remove("Name");
            ModelState.Remove("Document");

            if (!ModelState.IsValid) return PartialView("_UpdateAddress", developerViewModel);

            await _addressRepository.Update(_mapper.Map<Address>(developerViewModel.Address));

            var url = Url.Action("GetAddress", "Developers", new { id = developerViewModel.Address.DeveloperId });

            return Json(new { success = true, url });
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
