using Domain.Interfaces;
using Domain.Interfaces.DomainServices;
using MediatR;

namespace Application.Features.Commands.CreateItem
{
    public class CreateItemHandler : IRequestHandler<CreateItemCommand>
    {
        private readonly INotificationContext _notificationContext;
        private readonly ITableService _tableService;

        public CreateItemHandler(INotificationContext notificationContext, ITableService tableService)
        {
            _notificationContext = notificationContext;
            _tableService = tableService;
        }

        public async Task Handle(CreateItemCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid)
            {
                _notificationContext.AddNotifications(request.ValidationResult);
                return;
            }

            await _tableService.CreateItem(request.TableId, request.ConsumersIds, request.Name, request.Value, request.Quantity);
        }
    }
}