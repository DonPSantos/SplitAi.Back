using Domain.Notifications;
using FluentValidation.Results;
using System.Net;

namespace Domain.Interfaces
{
    public interface INotificationContext
    {
        /// <summary>
        /// Adiciona uma notificação no contexto
        /// </summary>
        /// <param name="message">Mensagem da notificação</param>
        void AddNotification(string message);

        /// <summary>
        /// Adiciona uma notificação
        /// </summary>
        /// <param name="notification">Objeto de notificação</param>
        void AddNotification(Notification notification);

        /// <summary>
        /// Adiciona uma lista de notificações
        /// </summary>
        /// <param name="notifications">Lista de notificações</param>
        void AddNotifications(IList<Notification> notifications);

        /// <summary>
        /// Adiciona notificações a partir de um FluentValidation
        /// </summary>
        /// <param name="validationResult">Resultado da validação do FluentValidation</param>
        void AddNotifications(ValidationResult validationResult);

        /// <summary>
        /// Obtém as notificações
        /// </summary>
        /// <returns>Lista no notificações</returns>
        IList<Notification> GetNotifications();

        /// <summary>
        /// Obtém o status code atual do contexto, por default é 400
        /// </summary>
        /// <returns>Retorna um numero que representa o status code</returns>
        int GetStatusCode();

        /// <summary>
        /// Verifica se existem notificações
        /// </summary>
        /// <returns>Booleano se existe ou não</returns>
        bool HasNotifications();

        /// <summary>
        /// Define o status code atual do contexto
        /// </summary>
        /// <param name="httpStatusCode">Status code</param>
        void SetStatusCode(HttpStatusCode httpStatusCode);
    }
}