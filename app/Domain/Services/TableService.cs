using Domain.Entities;
using Domain.Interfaces;
using Domain.Interfaces.DomainServices;
using Domain.Interfaces.Repositories;
using System.Net;

namespace Domain.Services
{
    public class TableService : BaseService, ITableService
    {
        private readonly ITableRepository _tableRepository;
        private readonly IPersonRepository _personRepository;
        private readonly IItemRepository _itemRepository;
        private readonly IConsumptionRepository _consumptionRepository;

        public TableService(INotificationContext notifier,
                            ITableRepository tableRepository,
                            IPersonRepository personRepository,
                            IItemRepository itemRepository,
                            IConsumptionRepository consumptionRepository) : base(notifier)
        {
            _tableRepository = tableRepository;
            _personRepository = personRepository;
            _itemRepository = itemRepository;
            _consumptionRepository = consumptionRepository;
        }

        public async Task AddPeopleToTable(Guid tableId, List<Guid> peopleIds)
        {
            var people = await _personRepository.GetAsync(x => peopleIds.Contains(x.Id));

            var table = (await _tableRepository.GetAsync(
                    filter: x => x.Id == tableId,
                    includes: [
                        x=>x.People,
                        x=>x.Items
                        ]
                    )
                ).FirstOrDefault();

            if (people is null)
                Notify("Nenhum consumidor foi encontrado");

            if (table is null)
                Notify("Mesa não encontrada");

            if (HasNotifications())
            {
                SetHttpStatusCode(HttpStatusCode.NotFound);
                return;
            }

            table.People.AddRange(people.ToList());

            await _tableRepository.Update(table);

            await _tableRepository.SaveChanges();
        }

        public async Task CreateItem(Guid tableId, List<Guid> consumersIds, string description, decimal unitValue, int quantity)
        {
            //Obtém objetos
            var table = await _tableRepository.GetTableWithPeopleAndItemsThenConsumptionById(tableId);
            var people = await _personRepository.GetAsync(x => consumersIds.Contains(x.Id));

            if (table is null)
                Notify("Mesa não encontrada");

            if (!people.Any())
                Notify("Nenhuma das pessoas informadas foram encontradas");

            if (HasNotifications())
            {
                SetHttpStatusCode(HttpStatusCode.NotFound);
                return;
            }

            //Monta itens
            var item = new Item(tableId, description, unitValue);
            var consumption = new Consumption(quantity, people.ToList());

            item.Table = table;
            item.Consumptions.Add(consumption);

            consumption.ItemId = item.Id;
            consumption.Participants.AddRange(people);

            await _itemRepository.Create(item);
            await _consumptionRepository.Create(consumption);

            await _itemRepository.SaveChanges();
        }

        public async Task<Table> CreateTable(Guid creatorId, string name, decimal serviceFee, decimal cover)
        {
            var table = new Table(name, serviceFee, cover);

            table.GenerateTableCode();

            var creator = await _personRepository.GetById(creatorId);

            if (creator is null)
            {
                Notify("Usuário não encontrado");
                SetHttpStatusCode(HttpStatusCode.BadRequest);
                return null;
            }

            table.People.Add(creator);

            await _tableRepository.Create(table);

            await _tableRepository.SaveChanges();

            return table;
        }

        public async Task<Table> GetTableByCode(string tableCode)
        {
            var result = await _tableRepository.GetAsync(x => x.TableCode == tableCode);

            if (!result.Any())
            {
                Notify("Mesa não encontrada");
                SetHttpStatusCode(HttpStatusCode.NotFound);
                return null;
            }

            return result.FirstOrDefault();
        }

        public async Task<Table> GetTableById(Guid id)
        {
            var result = await _tableRepository.GetById(id);

            if (result is null)
            {
                Notify("Mesa não encontrada");
                SetHttpStatusCode(HttpStatusCode.NotFound);
                return null;
            }

            return result;
        }

        public async Task<Table> GetTableWithPeopleAndItemsThenConsumptionById(Guid id)
        {
            var table = await _tableRepository.GetTableWithPeopleAndItemsThenConsumptionById(id);

            if (table is null)
            {
                Notify("Mesa não encontrada");
                return null;
            }

            return table;
        }
    }
}