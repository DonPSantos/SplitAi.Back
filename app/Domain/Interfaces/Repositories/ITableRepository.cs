using Domain.Entities;

namespace Domain.Interfaces.Repositories
{
    public interface ITableRepository : IRepository<Table>
    {
        Task<Table> GetTableWithPeopleAndItemsThenConsumptionById(Guid tableId);
    }
}