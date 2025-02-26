using Application.Features.Queries.GetPersonTables.Result;
using MediatR;

namespace Application.Features.Queries.GetPersonTables
{
    public class GetPersonTablesQuery : IRequest<IList<PersonTableResult>>
    {
        public GetPersonTablesQuery(Guid personId)
        {
            PersonId = personId;
        }

        public Guid PersonId { get; private set; }
    }
}