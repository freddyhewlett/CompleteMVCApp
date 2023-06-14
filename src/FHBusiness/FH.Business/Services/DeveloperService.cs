using FH.Business.Interfaces;
using FH.Business.Models;
using FH.Business.Models.Validations;
using FH.Business.Models.Validations.Docs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FH.Business.Services
{
    public class DeveloperService : BaseService, IDeveloperService
    {
        public async Task Add(Developer developer)
        {
            //valida estado da entidade
            if (!ExecuteValidation(new DeveloperValidation(), developer)
                && !ExecuteValidation(new AddressValidation(), developer.Address)) return;
        }

        public async Task AddressUpdate(Address address)
        {
            if (!ExecuteValidation(new AddressValidation(), address)) return;
        }

        public Task Remove(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task Update(Developer developer)
        {
            if (!ExecuteValidation(new DeveloperValidation(), developer)) return;
        }
    }
}
