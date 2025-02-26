using Application.DTOs;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Api.Middlewares
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            _logger.LogError(
                exception, "Exception occurred: {Message} ",
                exception.Message);

            var messageDefault = new List<string> {
                "Erro no servidor, contate um administrador"
            };

            var response = new ApiResponse<string?>(false, "Erro no servidor ao processar requisição", messageDefault);

            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

            await httpContext.Response
                .WriteAsJsonAsync(response, cancellationToken);

            return true;
        }
    }
}