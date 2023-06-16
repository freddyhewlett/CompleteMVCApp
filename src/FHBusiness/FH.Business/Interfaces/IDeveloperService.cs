using FH.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FH.Business.Interfaces
{
    public interface IDeveloperService : IDisposable
    {
        Task Add(Developer developer);
        Task Update(Developer developer);
        Task Remove(Guid id);
        Task AddressUpdate(Address address);
    }
}
