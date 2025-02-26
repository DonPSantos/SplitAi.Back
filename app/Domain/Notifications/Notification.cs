using System.Net;

namespace Domain.Notifications
{
    /// <summary>
    /// Objeto que representa uma mensagem de notificação
    /// </summary>
    public class Notification
    {
        public string Message { get; private set; }

        public Notification(string message)
        {
            Message = message;
        }
    }
}