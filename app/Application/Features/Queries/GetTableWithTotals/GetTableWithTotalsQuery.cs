using Application.Features.Commands;
using Application.Features.Queries.GetTableWithTotals.Table;
using MediatR;

namespace Application.Features.Queries.GetTableWithTotals
{
    public class GetTableWithTotalsQuery : BaseCommand, IRequest<TableWithDetailsResult>
    {
        public GetTableWithTotalsQuery(Guid tableId)
        {
            TableId = tableId;
        }

        public Guid TableId { get; private set; }
    }
}