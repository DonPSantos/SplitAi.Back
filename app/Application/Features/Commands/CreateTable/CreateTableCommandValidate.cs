using FluentValidation;

namespace Application.Features.Commands.CreateTable
{
    public class CreateTableCommandValidate : AbstractValidator<CreateTableCommand>
    {
        public CreateTableCommandValidate()
        {
            RuleFor(c => c.Name)
                .NotNull()
                .WithMessage("Informe um nome para identificar a mesa.")
                .NotEmpty()
                .WithMessage("Informe um nome para identificar a mesa.");

            RuleFor(c => c.ServiceFee)
                .NotNull()
                .WithMessage("Informa a taxa de serviço")
                .GreaterThanOrEqualTo(0)
                .WithMessage("O valor da taxa de serviço não pode ser menor que 0");

            RuleFor(c => c.Couvert)
                .NotNull()
                .WithMessage("Informa o valor do couvert")
                .GreaterThanOrEqualTo(0)
                .WithMessage("O valor do couvert não pode ser menor que 0");
        }
    }
}