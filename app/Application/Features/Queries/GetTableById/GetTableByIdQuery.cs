using Application.Features.Queries.GetTableWithTotals.Table;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Queries.GetTableById
{
    public class GetTableByIdQuery : IRequest<TableWithDetailsResult>
    {
        public Guid TableId { get; set; }
    }
}