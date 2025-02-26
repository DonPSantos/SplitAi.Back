using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infra.Context;

namespace Infra.Repositories
{
    public class ConsumptionRepository : Repository<Consumption>, IConsumptionRepository
    {
        public ConsumptionRepository(AppDbContext context) : base(context)
        {
        }
    }
}