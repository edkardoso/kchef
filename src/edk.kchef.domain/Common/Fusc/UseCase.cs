using System;
using System.Collections.Generic;
using System.Linq;
using edk.Kchef.Domain.Common.Base;
using FluentValidation;
using FluentValidation.Results;
using Guards;

namespace edk.Kchef.Domain.Common.Fusc
{
    public abstract class UseCase<TInput, TOutput> : IUseCase<TInput, TOutput>
    {
        private readonly AbstractValidator<TInput> _validator;
        private readonly IPresenter<TInput, TOutput> _presenter;
        private bool _complete;
        private ValidationResult _validationResult;
        private readonly List<IUseCase> _observers;
        private List<Notification> _notifications = new List<Notification>();

        public IPresenter<TInput, TOutput> Presenter => _presenter;
        public List<Notification> Notifications => _notifications;

        protected UseCase(IPresenter<TInput, TOutput> presenter = default, AbstractValidator<TInput> validator = default)
        {
            _presenter = presenter ?? new PresenterNull<TInput, TOutput>();
            _validator = validator ?? new ValidadorNull<TInput>();
            _observers = new List<IUseCase>();
        }

        public UseCase<TInput, TOutput> Execute(TInput input)
        {
            try
            {
                _validationResult = _validator.Validate(input);

                _notifications.AddRange(Notification.ConvertFrom(_validationResult.Errors));

                if (_validationResult.IsValid)
                {
                    var result = OnExecute(input);

                    _presenter.OnSuccess(result, Notifications);

                    Guard.ArgumentIsFalse(_presenter.Success, nameof(_presenter.Success));

                    _complete = true;

                    Notify();
                }
                else
                {
                    _presenter.OnError(input, Notifications);

                    Guard.ArgumentIsTrue(_presenter.Success, nameof(_presenter.Success));

                }

            }
            catch (Exception ex)
            {
                // Pode ser necessário adicionar um novo evento virtual de Exceção
                // para cenários onde há a necessidade de se desfazer alguma ação anterior
                // em caso de exceção. Embora o OnComplete(false) talvez já permita isso.
                _presenter.OnException(input, ex);

            }
            finally
            {
                OnComplete(_complete, _notifications);
            }

            return this;

        }
        /// <summary>
        /// Ação a ser executada quando os dados de entrada forem validados com sucesso
        /// </summary>
        public abstract TOutput OnExecute(TInput input);

        /// <summary>
        /// Ação posterior ao OnExecute
        /// </summary>
        /// <param name="completed">Será true se OnExecute tiver sido executado completamente.</param>
        protected virtual void OnComplete(bool completed, List<Notification> notifications)
        {
            return;
        }

        private void Notify()
        {
            _observers.ForEach(o => ((IUseCase<TInput, TOutput>)o).Handler(this));
        }

        public void Subscribe(IUseCase observer)
        {
            if (!_observers.Contains(observer))
                _observers.Add(observer);
        }

        /// <summary>
        /// Método que será invocado pelo método Notify, quando estiver observando outro UseCase
        /// </summary>
        public virtual void Handler(IUseCase<TInput, TOutput> other)
        {

        }

       /// <summary>
       /// Permite adicionar Notificações
       /// </summary>
        protected void SetNotification(string message, SeverityType severity)
        {
            _notifications.Add(new() { Message = message, Severity = severity });
        }
    }
}
