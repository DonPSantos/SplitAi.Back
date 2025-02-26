using FluentValidation;
using FluentValidation.Results;

namespace Application.Features.Commands
{
    public class BaseCommand
    {
        public bool IsValid { get; private set; }
        public ValidationResult ValidationResult { get; set; }

        public bool Validate<TModel>(TModel model, AbstractValidator<TModel> validator)
        {
            ValidationResult = validator.Validate(model);

            IsValid = ValidationResult.IsValid;

            return IsValid;
        }
    }
}