using MediatR;

namespace Application.Features.Commands.CreateTable
{
    public class CreateTableCommand : BaseCommand, IRequest<CreateTableResult>
    {
        public CreateTableCommand(Guid creatorId, string name, decimal serviceFee, decimal couvert)
        {
            CreatorId = creatorId;
            Name = name;
            ServiceFee = serviceFee;
            Couvert = couvert;

            Validate(this, new CreateTableCommandValidate());
        }

        public Guid CreatorId { get; private set; }

        public string Name { get; private set; }

        public decimal ServiceFee { get; private set; }

        public decimal Couvert { get; private set; }
    }
}