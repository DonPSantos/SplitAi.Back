using FluentValidation;

namespace Application.Features.Commands.AddPeopleOnTable
{
    public class AddPeopleOnTableCommandValidate : AbstractValidator<AddPeopleOnTableCommand>
    {
        public AddPeopleOnTableCommandValidate()
        {
            RuleFor(c => c.ConsumersIds)
                .NotEmpty().WithMessage("Informe ao menos um consumidor");
        }
    }
}