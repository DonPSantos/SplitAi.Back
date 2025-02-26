using Api.Requests.Item;
using Api.Requests.Table;
using Application.DTOs;
using Application.Features.Commands.AddPeopleOnTable;
using Application.Features.Commands.CreateItem;
using Application.Features.Commands.CreateTable;
using Application.Features.Queries.GetTableWithTotals;
using Application.Features.Queries.GetTableWithTotals.Table;
using Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.Net;

namespace Api.Controllers
{
    [Route("api/v1/[controller]")]
    public class TablesController : BaseController
    {
        public TablesController(INotificationContext notifier, IMediator mediator) : base(notifier, mediator)
        {
        }

        [HttpPost]
        [EndpointSummary("Cria uma mesa")]
        [ProducesResponseType<ApiResponse<CreateTableResult>>((int)HttpStatusCode.Created)]
        [ProducesResponseType<ApiResponse<string>>((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> PostAsync([FromBody] CreateTableRequest request)
        {
            var commad = new CreateTableCommand(request.CreatorId, request.Name, request.ServiceFee, request.Couvert);

            var result = await _mediator.Send(commad);

            return CustomResponse(HttpStatusCode.Created, "Mesa criada com sucesso", result);
        }

        [HttpGet("{tableId}")]
        [EndpointSummary("Obtém uma mesa pelo ID")]
        [ProducesResponseType<ApiResponse<TableWithDetailsResult>>((int)HttpStatusCode.OK)]
        [ProducesResponseType<ApiResponse<string>>((int)HttpStatusCode.NotFound)]
        [ProducesResponseType<ApiResponse<string>>((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetByIdAsync([FromRoute][Description("Id da mesa")] Guid tableId)
        {
            var query = new GetTableWithTotalsQuery(tableId);

            var table = await _mediator.Send(query);

            return CustomResponse(HttpStatusCode.OK, "Mesa obtida com sucesso", table);
        }

        [HttpPost("{tableId}/items")]
        [EndpointSummary("Adiciona um item a mesa")]
        [ProducesResponseType<ApiResponse<string>>((int)HttpStatusCode.Created)]
        [ProducesResponseType<ApiResponse<string>>((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> PostItemAsync([FromRoute][Description("Id da mesa")] Guid tableId,
                                        [FromBody][Description("Informações para criar item")] CreateItemRequest request)
        {
            if (!ModelState.IsValid) return CustomResponse(HttpStatusCode.BadRequest, "Solicitação invalida", ModelState);

            var command = new CreateItemCommand(tableId, request.Name, request.Value, request.Quantity, request.ConsumersIds);

            await _mediator.Send(command);

            return CustomResponse(HttpStatusCode.Created, "Item criado com sucesso", string.Empty);
        }

        [HttpPost("{tableId}/people")]
        [EndpointSummary("Adiciona pessoas a mesa")]
        [ProducesResponseType<ApiResponse<string>>((int)HttpStatusCode.Created)]
        [ProducesResponseType<ApiResponse<string>>((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> PostPeopleAsync([FromRoute][Description("Id da mesa")] Guid tableId,
                                [FromBody][Description("Lista de participantes")] List<Guid> request)
        {
            var command = new AddPeopleOnTableCommand(tableId, request);

            await _mediator.Send(command);

            return CustomResponse(HttpStatusCode.Created, "Usuários adicionados com sucesso", string.Empty);
        }
    }
}