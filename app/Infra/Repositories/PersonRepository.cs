using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories
{
    public class PersonRepository : Repository<Person>, IPersonRepository
    {
        public PersonRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Person> GetPersonWithConsumptionsThenItem(Guid personId)
        {
            return await _dbSet
                .Include(p => p.Consumptions)
                .ThenInclude(c => c.Item)
                .FirstOrDefaultAsync(p => p.Id == personId);
        }
    }
}