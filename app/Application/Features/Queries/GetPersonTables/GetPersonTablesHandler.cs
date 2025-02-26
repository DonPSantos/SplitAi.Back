using Application.Features.Queries.GetPersonTables.Result;
using AutoMapper;
using Domain.Interfaces;
using Domain.Interfaces.Repositories;
using MediatR;

namespace Application.Features.Queries.GetPersonTables
{
    public class GetPersonTablesHandler : IRequestHandler<GetPersonTablesQuery, IList<PersonTableResult>>
    {
        private readonly INotificationContext _notificationContext;
        private readonly IMapper _mapper;
        private readonly IPersonRepository _personRepository;

        public GetPersonTablesHandler(IMapper mapper, INotificationContext notificationContext, IPersonRepository personRepository)
        {
            _notificationContext = notificationContext;
            _mapper = mapper;
            _personRepository = personRepository;
        }

        public async Task<IList<PersonTableResult>> Handle(GetPersonTablesQuery request, CancellationToken cancellationToken)
        {
            var person = (await _personRepository.GetAsync(
                filter: p => p.Id == request.PersonId,
                includes: [x => x.Tables]
                )).FirstOrDefault();

            if (person is null)
            {
                _notificationContext.AddNotification("Pessoa não encontrada");
                return null;
            }

            return _mapper.Map<IList<PersonTableResult>>(person.Tables);
        }
    }
}