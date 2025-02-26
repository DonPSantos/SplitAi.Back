using MediatR;

namespace Application.Features.Commands.CreateItem
{
    public class CreateItemCommand : BaseCommand, IRequest
    {
        public CreateItemCommand(Guid tableId, string name, decimal value, int quantity, List<Guid> consumersIds)
        {
            TableId = tableId;
            Name = name;
            Value = value;
            Quantity = quantity;
            ConsumersIds = consumersIds;

            Validate(this, new CreateItemCommandValidate());
        }

        public Guid TableId { get; private set; }

        public string Name { get; private set; }

        public decimal Value { get; private set; }

        public int Quantity { get; private set; }

        public List<Guid> ConsumersIds { get; private set; } = new List<Guid>();
    }
}