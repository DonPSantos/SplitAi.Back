using FluentValidation;
using FluentValidation.Results;

namespace Domain.Entities
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }
        public bool Valid { get; private set; }
        public bool Invalid => !Valid;
        public ValidationResult ValidationResult { get; private set; }

        public BaseEntity()
        {
            Id = Guid.CreateVersion7();
        }

        public bool Validate<TModel>(TModel model, AbstractValidator<TModel> validator)
        {
            ValidationResult = validator.Validate(model);
            return Valid = ValidationResult.IsValid;
        }
    }
}