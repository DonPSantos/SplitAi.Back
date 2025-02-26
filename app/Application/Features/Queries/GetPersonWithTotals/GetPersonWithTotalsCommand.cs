using Application.Features.Queries.GetPersonWithTotals.Person;
using MediatR;

namespace Application.Features.Queries.GetPersonWithTotals
{
    public class GetPersonWithTotalsCommand : IRequest<PersonWithTotalsResult>
    {
        public Guid PersonId { get; set; }
        public Guid TableId { get; set; }
    }
}