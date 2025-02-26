using MediatR;

namespace Application.Features.Commands.AddPeopleOnTable
{
    public class AddPeopleOnTableCommand : BaseCommand, IRequest
    {
        public AddPeopleOnTableCommand(Guid tableId, List<Guid> consumersIds)
        {
            TableId = tableId;
            ConsumersIds = consumersIds;

            Validate(this, new AddPeopleOnTableCommandValidate());
        }

        public Guid TableId { get; set; }
        public List<Guid> ConsumersIds { get; set; }
    }
}