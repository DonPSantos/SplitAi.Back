using Application.Features.Queries.GetTableWithTotals.Table;
using AutoMapper;
using Domain.Interfaces.DomainServices;
using MediatR;

namespace Application.Features.Queries.GetTableWithTotals
{
    public class GetTableWithTotalsHandler : IRequestHandler<GetTableWithTotalsQuery, TableWithDetailsResult>
    {
        private readonly IMapper _mapper;
        private readonly ITableService _tableService;

        public GetTableWithTotalsHandler(IMapper mapper, ITableService tableService)
        {
            _mapper = mapper;
            _tableService = tableService;
        }

        public async Task<TableWithDetailsResult> Handle(GetTableWithTotalsQuery request, CancellationToken cancellationToken)
        {
            var table = await _tableService.GetTableWithPeopleAndItemsThenConsumptionById(request.TableId);

            if (table is null)
                return null;

            var result = _mapper.Map<TableWithDetailsResult>(table);

            var peopleConsumption = table.GetListPersonConsumption();

            var peopleConsumptionWithCouvert = table.GetListPersonConsumptionWithCouvert();

            var peopleConsumptionWithCouvertAndFee = table.GetListPersonConsumptionWithCouvertAndFee();

            foreach (var person in result.People)
            {
                person.ConsumptionValue = Math.Round(peopleConsumption.FirstOrDefault(d => d.Key.Id == person.Id).Value, 2);
                person.ConsumptionValueWithCouvert = Math.Round(peopleConsumptionWithCouvert.FirstOrDefault(d => d.Key.Id == person.Id).Value, 2);
                person.ConsumptionValueWithCouvertAndFee = Math.Round(peopleConsumptionWithCouvertAndFee.FirstOrDefault(d => d.Key.Id == person.Id).Value, 2);
            }
            result.TotalPartial = peopleConsumption.Sum(x => x.Value);
            result.TotalPartialWithCoverCharge = peopleConsumptionWithCouvert.Sum(x => x.Value);
            result.TotalWithServiceAndCoverCharge = peopleConsumptionWithCouvertAndFee.Sum(x => x.Value);

            foreach (var item in result.Items)
            {
                item.Quantity = table.Items.FirstOrDefault(i => i.Description == item.Description).TotalQuantity();
                item.Total = table.Items.FirstOrDefault(i => i.Description == item.Description).TotalCost();
            }

            return result;
        }
    }
}