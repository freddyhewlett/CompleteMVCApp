using FH.Business.Interfaces;
using FH.Business.Models;
using FH.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;

namespace FH.Data.Repositories
{
    public class AddressRepository : Repository<Address>, IAddressRepository
    {
        public AddressRepository(DataDbContext context) : base(context)
        {
        }

        public async Task<Address> GetAddressByDeveloper(Guid developerId)
        {
            return await _dbContext.Addresses.AsNoTracking()
                .FirstOrDefaultAsync(d => d.DeveloperId == developerId);
        }
    }
}
