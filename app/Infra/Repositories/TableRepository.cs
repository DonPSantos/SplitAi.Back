using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories
{
    public class TableRepository : Repository<Table>, ITableRepository
    {
        public TableRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Table> GetTableWithPeopleAndItemsThenConsumptionById(Guid tableId)
        {
            return await _dbSet
                .Include(t => t.People)
                .Include(t => t.Items)
                .ThenInclude(i => i.Consumptions)
                .ThenInclude(c => c.Participants)
                .FirstOrDefaultAsync(t => t.Id == tableId);
        }
    }
}