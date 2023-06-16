using FH.Business.Interfaces;
using FH.Business.Models;
using FH.Business.Models.Validations;
using FH.Business.Models.Validations.Docs;

namespace FH.Business.Services
{
    public class DeveloperService : BaseService, IDeveloperService
    {
        private readonly IDeveloperRepository _developerRepository;
        private readonly IAddressRepository _addressRepository;

        public DeveloperService(IDeveloperRepository developerRepository, IAddressRepository addressRepository, INotificator notificator) : base(notificator)
        {
            _developerRepository = developerRepository;
            _addressRepository = addressRepository;
        }

        public async Task Add(Developer developer)
        {
            //valida estado da entidade
            if (!ExecuteValidation(new DeveloperValidation(), developer)
                || !ExecuteValidation(new AddressValidation(), developer.Address)) return;

            if (_developerRepository.Search(d => d.Document == developer.Document).Result.Any())
            {
                Notify("Já existe um desenvolvedor com este documento...");
                return;
            }

            await _developerRepository.Add(developer);
        }

        public async Task AddressUpdate(Address address)
        {
            if (!ExecuteValidation(new AddressValidation(), address)) return;

            await _addressRepository.Update(address);
        }

        public async Task Remove(Guid id)
        {
            if (_developerRepository.GetDeveloperGamesAddress(id).Result.Games.Any())
            {
                Notify("O desenvolvedor possui jogos cadastrados");
                return;
            }

            await _developerRepository.Remove(id);
        }

        public async Task Update(Developer developer)
        {
            if (!ExecuteValidation(new DeveloperValidation(), developer)) return;

            if (_developerRepository.Search(d => d.Document == developer.Document && d.Id != developer.Id).Result.Any())
            {
                Notify("Já existe um desenvolvedor com este documento...");
                return;
            }

            await _developerRepository.Update(developer);
        }

        public void Dispose()
        {
            _developerRepository?.Dispose();
            _addressRepository?.Dispose();
        }
    }
}
