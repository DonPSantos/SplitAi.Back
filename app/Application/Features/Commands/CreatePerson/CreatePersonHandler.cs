using Application.Features.Queries.GetPersonByEmail.Result;
using AutoMapper;
using Domain.Interfaces;
using Domain.Interfaces.DomainServices;
using MediatR;

namespace Application.Features.Commands.CreatePerson
{
    public class CreatePersonHandler : IRequestHandler<CreatePersonCommand, PersonResult>
    {
        private readonly INotificationContext _notificationContext;
        private readonly IMapper _mapper;
        private readonly IPersonService _personService;

        public CreatePersonHandler(INotificationContext notificationContext, IMapper mapper, IPersonService personService)
        {
            _notificationContext = notificationContext;
            _mapper = mapper;
            _personService = personService;
        }

        async Task<PersonResult> IRequestHandler<CreatePersonCommand, PersonResult>.Handle(CreatePersonCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid)
            {
                _notificationContext.AddNotifications(request.ValidationResult);
                return null;
            }

            var person = await _personService.CreatePerson(request.Name, request.Email);
            return _mapper.Map<PersonResult>(person);
        }
    }
}