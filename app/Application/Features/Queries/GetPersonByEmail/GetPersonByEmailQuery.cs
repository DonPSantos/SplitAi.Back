using Application.Features.Queries.GetPersonByEmail.Result;
using MediatR;

namespace Application.Features.Queries.GetPersonByEmail
{
    public class GetPersonByEmailQuery : IRequest<PersonResult>
    {
        public string Email { get; set; }
    }
}