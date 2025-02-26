using Domain.Notifications;

namespace Application.DTOs
{
    public class ApiResponse<T>
    {
        public bool Success { get; private set; }
        public string Message { get; private set; }
        public T? Data { get; private set; }
        public List<string> Errors { get; private set; }

        public ApiResponse(bool success, string message, T data)
        {
            Success = success;
            Message = message;
            Data = data;
            Errors = null;
        }

        public ApiResponse(bool success, string message, List<string> errors)
        {
            Success = success;
            Message = message;
            Errors = errors;
        }

        public ApiResponse(bool success, string message, IList<Notification> notifications)
        {
            Success = success;
            Message = message;
            Errors = notifications.Select(n => n.Message).ToList();
        }
    }
}