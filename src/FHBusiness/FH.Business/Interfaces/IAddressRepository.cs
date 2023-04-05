using FH.Business.Models;
using System;

namespace FH.Business.Interfaces
{
    public interface IAddressRepository : IRepository<Address>
    {
        Task<Address> GetAddressByDeveloper(Guid developerId);
    }
}
