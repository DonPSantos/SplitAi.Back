using Application.DTOs;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text.Json;

namespace Application.Configurations.Filters
{
    public class NotificationFilter : IAsyncResultFilter
    {
        private readonly INotificationContext _notificationContext;

        public NotificationFilter(INotificationContext notificationContext)
        {
            _notificationContext = notificationContext;
        }

        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            if (_notificationContext.HasNotifications())
            {
                context.HttpContext.Response.StatusCode = _notificationContext.GetStatusCode();
                context.HttpContext.Response.ContentType = "application/json";

                var response = new ApiResponse<string>(false, "Falha na requisição.", _notificationContext.GetNotifications());
                var notifications = JsonSerializer.Serialize(response);
                await context.HttpContext.Response.WriteAsync(notifications);

                return;
            }

            await next();
        }
    }
}