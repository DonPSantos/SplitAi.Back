using Domain.Entities;

namespace Domain.Interfaces.DomainServices
{
    public interface IPersonService
    {
        public Task<Person> CreatePerson(string name, string email);

        public Task<Person> GetPersonById(Guid personId);

        public Task<Person> GetPersonByEmail(string email);

        public Task<Person> GetPersonWithConsumptionsThenItem(Guid personId);
    }
}