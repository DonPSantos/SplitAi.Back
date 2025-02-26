using Domain.Entities;
using Domain.Interfaces;
using Domain.Notifications;
using FluentValidation;
using FluentValidation.Results;
using System.Net;

namespace Domain.Services
{
    public abstract class BaseService
    {
        private readonly INotificationContext _notificationContext;

        protected BaseService(INotificationContext notificationContext)
        {
            _notificationContext = notificationContext;
        }

        /// <summary>
        /// Adiciona uma nova notificação a partir de um FluentValidation
        /// </summary>
        /// <param name="validationResult">Itens validados com inconsistênciacia</param>
        protected void Notify(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                Notify(error.ErrorMessage);
            }
        }

        /// <summary>
        /// Adiciona uma nova notificação
        /// </summary>
        /// <param name="mensagem">Mensagem da notificação</param>
        protected void Notify(string mensagem)
        {
            _notificationContext.AddNotification(new Notification(mensagem));
        }

        /// <summary>
        /// Define o status code no contexto da notificação
        /// </summary>
        /// <param name="httpStatusCode">Status Code</param>
        protected void SetHttpStatusCode(HttpStatusCode httpStatusCode)
        {
            _notificationContext.SetStatusCode(httpStatusCode);
        }

        /// <summary>
        /// Verifica se existem notificações
        /// </summary>
        /// <returns>Bolleano se existe notificações</returns>
        protected bool HasNotifications()
        {
            return _notificationContext.HasNotifications();
        }

        /// <summary>
        /// Executa a validação atribuída no FluentValidation
        /// </summary>
        /// <typeparam name="TV">Objeto de validação</typeparam>
        /// <typeparam name="TE">Entidade de domínio</typeparam>
        /// <param name="validation">Objeto com as configurações de validação</param>
        /// <param name="entity">Entidade a ser validada</param>
        /// <returns></returns>
        protected bool ExecuteValidation<TV, TE>(TV validation, TE entity) where TV : AbstractValidator<TE> where TE : BaseEntity
        {
            var validator = validation.Validate(entity);

            if (validator.IsValid) return true;

            Notify(validator);

            return false;
        }
    }
}