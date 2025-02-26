using FluentValidation;

namespace Application.Features.Commands.CreateItem
{
    public class CreateItemCommandValidate : AbstractValidator<CreateItemCommand>
    {
        public CreateItemCommandValidate()
        {
            RuleFor(c => c.Name)
                .NotNull().WithMessage("Informe a descrição do item")
                .NotEmpty().WithMessage("Informe a descrição do item");

            RuleFor(c => c.Value)
                .GreaterThan(0).WithMessage("O valor do item tem de ser maior que 0");

            RuleFor(c => c.Value)
                .GreaterThan(0).WithMessage("Deve haver ao menos uma unidade do item");

            RuleFor(c => c.ConsumersIds)
                .NotEmpty().WithMessage("É necessário ao menos 1 consumidor");
        }
    }
}