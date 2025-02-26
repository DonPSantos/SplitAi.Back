using AutoMapper;
using Domain.Interfaces.DomainServices;
using MediatR;

namespace Application.Features.Commands.AddPeopleOnTable
{
    public class AddPeopleOnTableHandler : IRequestHandler<AddPeopleOnTableCommand>
    {
        private readonly ITableService _tableService;

        public AddPeopleOnTableHandler(ITableService tableService)
        {
            _tableService = tableService;
        }

        public async Task Handle(AddPeopleOnTableCommand request, CancellationToken cancellationToken)
        {
            await _tableService.AddPeopleToTable(request.TableId, request.ConsumersIds);
        }
    }
}