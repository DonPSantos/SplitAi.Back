using Application.DTOs;
using Domain.Interfaces;
using Domain.Notifications;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Net;

namespace Api.Controllers
{
    [ApiController]
    public class BaseController : Controller
    {
        private readonly INotificationContext _notifier;
        protected readonly IMediator _mediator;

        public BaseController(INotificationContext notifier, IMediator mediator)
        {
            _notifier = notifier;
            _mediator = mediator;
        }

        protected void NotifyError(string message)
        {
            _notifier.AddNotification(new Notification(message));
        }

        protected bool ValidOperation()
        {
            return !_notifier.HasNotifications();
        }

        private void NotifyErrorModelInvalide(ModelStateDictionary modelState)
        {
            var errors = modelState.Values.SelectMany(x => x.Errors);

            foreach (var error in errors)
            {
                var errorMessage = error.Exception == null ? error.ErrorMessage : error.ErrorMessage;
                NotifyError(errorMessage);
            }
        }

        protected IActionResult CustomResponse(HttpStatusCode code, string message, ModelStateDictionary modelState)
        {
            if (!modelState.IsValid) NotifyErrorModelInvalide(modelState);

            return CustomResponse(code, message, string.Empty);
        }

        protected IActionResult CustomResponse<T>(HttpStatusCode code, string message, T data)
        {
            if (ValidOperation())
                return StatusCode((int)code, new ApiResponse<T>(true, message, data));

            return StatusCode((int)code,
                new ApiResponse<T>(false, "Erro ao processar requisição", _notifier.GetNotifications().Select(n => n.Message).ToList()));
        }
    }
}