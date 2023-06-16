using FH.Business.Interfaces;
using FH.Business.Models;
using FH.Business.Notifications;
using FluentValidation;
using FluentValidation.Results;

namespace FH.Business.Services
{
    public abstract class BaseService
    {
        private readonly INotificator _notificator;

        public BaseService(INotificator notificator)
        {
            _notificator = notificator;
        }

        //leva um erro a camada de apresentação (FH.App)
        protected void Notify(string message) 
        {
            _notificator.Handle(new Notification(message));
        }

        protected void Notify(ValidationResult validationResult) 
        {
            foreach (var error in validationResult.Errors)
            {
                Notify(error.ErrorMessage);
            }
        }

        protected bool ExecuteValidation<TV, TE>(TV validation, TE entity) where TV : AbstractValidator<TE> where TE : Entity
        {
            var validator = validation.Validate(entity);

            if (validator.IsValid) return true;

            Notify(validator);
            return false;
        }
    }
}
