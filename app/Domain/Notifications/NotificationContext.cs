using Domain.Interfaces;
using FluentValidation.Results;
using System.Net;

namespace Domain.Notifications
{
    public class NotificationContext : INotificationContext
    {
        private readonly List<Notification> _notifications;

        public NotificationContext()
        {
            _notifications = new List<Notification>();
            HttpStatusCode = HttpStatusCode.BadRequest;
        }

        public HttpStatusCode HttpStatusCode { get; private set; }

        public void AddNotification(string message)
        {
            _notifications.Add(new Notification(message));
        }

        public void AddNotification(Notification notification)
        {
            _notifications.Add(notification);
        }

        public void AddNotifications(IList<Notification> notifications)
        {
            _notifications.AddRange(notifications);
        }

        public void AddNotifications(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                AddNotification(error.ErrorMessage);
            }
        }

        public IList<Notification> GetNotifications() => _notifications;

        public int GetStatusCode()
        {
            return (int)this.HttpStatusCode;
        }

        public bool HasNotifications() => _notifications.Any();

        public void SetStatusCode(HttpStatusCode httpStatusCode)
        {
            this.HttpStatusCode = httpStatusCode;
        }
    }
}