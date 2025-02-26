using Application.Features.Queries.GetPersonWithTotals.Person;
using AutoMapper;
using Domain.Interfaces;
using Domain.Interfaces.DomainServices;
using Domain.Notifications;
using Domain.Services;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Queries.GetPersonWithTotals
{
    public class GetPersonWithTotalsHandle : IRequestHandler<GetPersonWithTotalsCommand, PersonWithTotalsResult>
    {
        private readonly INotificationContext _notificationContext;
        private readonly IMapper _mapper;
        private readonly IPersonService _personService;
        private readonly ITableService _tableService;

        public GetPersonWithTotalsHandle(INotificationContext notificationContext, IMapper mapper, IPersonService personService, ITableService tableService)
        {
            _notificationContext = notificationContext;
            _mapper = mapper;
            _personService = personService;
            _tableService = tableService;
        }

        public async Task<PersonWithTotalsResult> Handle(GetPersonWithTotalsCommand request, CancellationToken cancellationToken)
        {
            var person = await _personService.GetPersonWithConsumptionsThenItem(request.PersonId);

            var table = await _tableService.GetTableWithPeopleAndItemsThenConsumptionById(request.TableId);

            if (person is null || table is null)
            {
                _notificationContext.SetStatusCode(System.Net.HttpStatusCode.NotFound);
                return null;
            }
            var result = _mapper.Map<PersonWithTotalsResult>(person);

            result.CoverCharge = table.Couvert;
            result.TotalPartial = Math.Round(table.GetPersonConsumption(person), 2);
            result.TotalPartialWithCoverCharge = Math.Round(table.GetPersonConsumptionWithCouvert(person), 2);
            result.TotalWithServiceAndCoverCharge = Math.Round(table.GetPersonConsumptionWithCouvertAndFee(person), 2);

            foreach (var consumption in person.Consumptions)
            {
                result.Items.Add(_mapper.Map<PersonItemsResult>(consumption.Item));
            }

            foreach (var item in result.Items)
            {
                item.Quantity = table.Items.FirstOrDefault(x => x.Description == item.Description).TotalQuantity();
                item.Total = table.Items.FirstOrDefault(x => x.Description == item.Description).TotalCost();
            }

            result.Items = result.Items.Distinct().ToList();

            return result;
        }
    }
}