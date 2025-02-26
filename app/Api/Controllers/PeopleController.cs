using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using MediatR;
using Application.DTOs;
using Application.Features.Queries.GetPersonByEmail;
using Application.Features.Commands.CreatePerson;
using System.ComponentModel;
using Application.Features.Queries.GetPersonWithTotals;
using Api.Requests.Person;
using Application.Features.Queries.GetPersonTables;
using Application.Features.Queries.GetPersonTables.Result;
using Application.Features.Queries.GetPersonWithTotals.Person;
using Application.Features.Queries.GetPersonByEmail.Result;

namespace Api.Controllers
{
    [Route("api/v1/[controller]")]
    public class PeopleController : BaseController
    {
        public PeopleController(INotificationContext notifier, IMediator mediator) : base(notifier, mediator)
        {
        }

        [HttpGet("{personId}/tables")]
        [EndpointSummary("Obtém todas as mesas de uma pessoa")]
        [ProducesResponseType<ApiResponse<PersonTableResult>>((int)HttpStatusCode.OK)]
        [ProducesResponseType<ApiResponse<string>>((int)HttpStatusCode.NotFound)]
        [ProducesResponseType<ApiResponse<string>>((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetPersonTables([FromRoute][Description("Id da pessoa")] Guid personId)
        {
            var command = new GetPersonTablesQuery(personId);

            var tables = await _mediator.Send(command);

            return CustomResponse(HttpStatusCode.OK, "Mesas obtidas com sucesso", tables);
        }

        [HttpGet("{personId}/tables/{tableId}")]
        [EndpointSummary("Obtém uma pessoa com o total de uma mesa")]
        [ProducesResponseType<ApiResponse<PersonWithTotalsResult>>((int)HttpStatusCode.OK)]
        [ProducesResponseType<ApiResponse<string>>((int)HttpStatusCode.NotFound)]
        [ProducesResponseType<ApiResponse<string>>((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetByEmailAsync(
            [FromRoute][Description("Id da pessoa")] Guid personId,
            [FromRoute][Description("Id da mesa")] Guid tableId)
        {
            if (!ModelState.IsValid) return CustomResponse(HttpStatusCode.BadRequest, "Solicitação invalida", ModelState);

            try
            {
                var command = new GetPersonWithTotalsCommand { PersonId = personId, TableId = tableId };

                var person = await _mediator.Send(command);
                if (person == null)
                {
                    return CustomResponse(HttpStatusCode.NotFound, "Pessoa não encontrada", string.Empty);
                }
                return CustomResponse(HttpStatusCode.OK, "Pessoa obtida com sucesso", person);
            }
            catch (Exception ex)
            {
                //TODO
                //NotifyError(ex.Message);
                return CustomResponse(HttpStatusCode.InternalServerError, "Erro inesperado", string.Empty);
            }
        }

        [HttpGet]
        [EndpointSummary("Obtém uma pessoa pelo email")]
        [ProducesResponseType<ApiResponse<PersonResult>>((int)HttpStatusCode.OK)]
        [ProducesResponseType<ApiResponse<string>>((int)HttpStatusCode.NotFound)]
        [ProducesResponseType<ApiResponse<string>>((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetByEmailAsync([FromQuery] string email)
        {
            if (!ModelState.IsValid) return CustomResponse(HttpStatusCode.BadRequest, "Solicitação invalida", ModelState);

            try
            {
                var command = new GetPersonByEmailQuery { Email = email };

                var person = await _mediator.Send(command);
                if (person == null)
                {
                    return CustomResponse(HttpStatusCode.NotFound, "Pessoa não encontrada", string.Empty);
                }
                return CustomResponse(HttpStatusCode.OK, "Pessoa obtida com sucesso", person);
            }
            catch (Exception ex)
            {
                //TODO
                //NotifyError(ex.Message);
                return CustomResponse(HttpStatusCode.InternalServerError, "Erro inesperado", string.Empty);
            }
        }

        [HttpPost]
        [EndpointSummary("Criar uma pessoa no sistema.")]
        [ProducesResponseType<ApiResponse<PersonResult>>((int)HttpStatusCode.Created)]
        [ProducesResponseType<ApiResponse<string>>((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> PostAsync([FromBody] CreatePersonRequest request)
        {
            try
            {
                var command = new CreatePersonCommand(request.Name, request.Email);

                var person = await _mediator.Send(command);

                return CustomResponse(HttpStatusCode.Created, "Consumidor criado com sucesso", person);
            }
            catch (Exception ex)
            {
                //TODO
                //NotifyError(ex.Message);
                return CustomResponse(HttpStatusCode.InternalServerError, "Erro ao criar pessoa", string.Empty);
            }
        }
    }
}