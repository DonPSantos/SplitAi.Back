using AutoMapper;
using Domain.Interfaces;
using Domain.Interfaces.DomainServices;
using MediatR;

namespace Application.Features.Commands.CreateTable
{
    public class CreateTableHandler : IRequestHandler<CreateTableCommand, CreateTableResult>
    {
        private readonly INotificationContext _notificationContext;
        private readonly IMapper _mapper;
        private readonly ITableService _tableService;

        public CreateTableHandler(INotificationContext notificationContext, IMapper mapper, ITableService tableService)
        {
            _notificationContext = notificationContext;
            _mapper = mapper;
            _tableService = tableService;
        }

        public async Task<CreateTableResult> Handle(CreateTableCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid)
            {
                _notificationContext.AddNotifications(request.ValidationResult);
                return null;
            }

            var table = await _tableService.CreateTable(request.CreatorId, request.Name, request.ServiceFee, request.Couvert);

            return _mapper.Map<CreateTableResult>(table);
        }
    }
}