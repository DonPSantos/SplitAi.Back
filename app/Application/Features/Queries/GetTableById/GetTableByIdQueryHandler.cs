using Application.Features.Queries.GetTableWithTotals.Table;
using AutoMapper;
using Domain.Interfaces.DomainServices;
using MediatR;

namespace Application.Features.Queries.GetTableById
{
    public class GetTableByIdQueryHandler : IRequestHandler<GetTableByIdQuery, TableWithDetailsResult>
    {
        private readonly ITableService _tableService;
        private readonly IMapper _mapper;

        public GetTableByIdQueryHandler(IMapper mapper, ITableService tableService)
        {
            _tableService = tableService;
            _mapper = mapper;
        }

        public async Task<TableWithDetailsResult> Handle(GetTableByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _tableService.GetTableById(request.TableId);

            return _mapper.Map<TableWithDetailsResult>(result);
        }
    }
}