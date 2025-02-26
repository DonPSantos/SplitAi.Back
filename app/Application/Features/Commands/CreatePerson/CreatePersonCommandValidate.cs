using FluentValidation;

namespace Application.Features.Commands.CreatePerson
{
    public class CreatePersonCommandValidate : AbstractValidator<CreatePersonCommand>
    {
        public CreatePersonCommandValidate()
        {
            RuleFor(c => c.Name)
                .NotNull().WithMessage("Informe um nome para a pessoa")
                .NotEmpty().WithMessage("Informe um nome para a pessoa")
                .MinimumLength(3).WithMessage("O nome precisa ter ao menos 3 caracteres");

            RuleFor(c => c.Email)
                .NotNull().WithMessage("Informe um email")
                .NotEmpty().WithMessage("Informe um email")
                .EmailAddress().WithMessage("Informe um email valido");
        }
    }
}