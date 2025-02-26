using Domain.Entities;

namespace Domain.Interfaces.DomainServices
{
    public interface ITableService
    {
        public Task<Table> CreateTable(Guid creatorId, string name, decimal serviceFee, decimal coverCharge);

        public Task<Table> GetTableById(Guid id);

        public Task<Table> GetTableWithPeopleAndItemsThenConsumptionById(Guid id);

        public Task<Table> GetTableByCode(string tableCode);

        public Task CreateItem(Guid tableId, List<Guid> consumersIds, string name, decimal itemValue, int quantity);

        public Task AddPeopleToTable(Guid tableId, List<Guid> peopleIds);
    }
}