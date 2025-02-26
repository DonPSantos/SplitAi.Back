using Application.Features.Queries.GetPersonByEmail.Result;
using MediatR;

namespace Application.Features.Commands.CreatePerson
{
    public class CreatePersonCommand : BaseCommand, IRequest<PersonResult>
    {
        public CreatePersonCommand(string name, string email)
        {
            Name = name;
            Email = email;

            Validate(this, new CreatePersonCommandValidate());
        }

        public string Name { get; set; }

        public string Email { get; set; }
    }
}