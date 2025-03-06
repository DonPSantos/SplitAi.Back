using Domain.Entities;
using Domain.Interfaces;
using Domain.Interfaces.DomainServices;
using Domain.Interfaces.Repositories;

namespace Domain.Services
{
    public class PersonService : BaseService, IPersonService
    {
        private readonly IPersonRepository _personRepository;

        public PersonService(INotificationContext notificationContext, IPersonRepository personRepository) : base(notificationContext)
        {
            _personRepository = personRepository;
        }

        public async Task<Person> CreatePerson(string name, string email)
        {
            var person = new Person(name, email);

            var existingPerson = await GetPersonByEmail(email);
            if (existingPerson != null)
                return existingPerson;

            await _personRepository.Create(person);

            await _personRepository.SaveChanges();

            return person;
        }

        public async Task<Person> GetPersonByEmail(string email)
        {
            var result = await _personRepository.GetAsync(
                filter: x => x.Email == email
                );

            if (!result.Any())
                Notify("Pessoa não encontrada");

            return result.FirstOrDefault();
        }

        /// <summary>
        /// Retorna uma pessoa com seus consumos e o item associado ao consumo
        /// </summary>
        /// <param name="personId">Id da pessoa</param>
        /// <returns>Pessoa com consumos e o item de cada consumo</returns>
        public async Task<Person> GetPersonWithConsumptionsThenItem(Guid personId)
        {
            var result = await _personRepository.GetPersonWithConsumptionsThenItem(personId);

            if (result is null)
                Notify("Pessoa não encontrada");

            return result;
        }

        public async Task<Person> GetPersonById(Guid personId)
        {
            var result = await _personRepository.GetById(personId);

            if (result is null)
                Notify("Pessoa não encontrada");

            return result;
        }
    }
}