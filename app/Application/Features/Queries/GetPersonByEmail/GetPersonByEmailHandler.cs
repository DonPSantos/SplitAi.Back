using Application.Features.Queries.GetPersonByEmail.Result;
using AutoMapper;
using Domain.Interfaces.DomainServices;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Queries.GetPersonByEmail
{
    public class GetPersonByEmailHandler : IRequestHandler<GetPersonByEmailQuery, PersonResult>
    {
        private readonly IMapper _mapper;
        private readonly IPersonService _personService;

        public GetPersonByEmailHandler(IMapper mapper, IPersonService personService)
        {
            _mapper = mapper;
            _personService = personService;
        }

        public async Task<PersonResult> Handle(GetPersonByEmailQuery request, CancellationToken cancellationToken)
        {
            var result = await _personService.GetPersonByEmail(request.Email);

            return _mapper.Map<PersonResult>(result);
        }
    }
}