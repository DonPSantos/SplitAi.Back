using Domain.Entities;

namespace Domain.Interfaces.Repositories
{
    public interface IPersonRepository : IRepository<Person>
    {
        Task<Person> GetPersonWithConsumptionsThenItem(Guid personId);
    }
}