using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using edk.Kchef.Domain.Common.Base;
using FluentValidation;
using FluentValidation.Results;
using Guards;
using MediatR;

namespace edk.Kchef.Application.Fusc;
public abstract class UseCase<TRequest, TResponse> :
    IUseCase<TRequest, TResponse>,
    IRequestHandler<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly AbstractValidator<TRequest> _validator;
    private readonly IPresenter<TRequest, TResponse> _presenter;
    private bool _complete;
    private ValidationResult _validationResult;
    private readonly List<IUseCase> _observers;
    private List<Notification> _notifications = new List<Notification>();

    public IPresenter<TRequest, TResponse> Presenter => _presenter;
    public List<Notification> Notifications => _notifications;

    protected abstract string NameUseCase { get; }

    protected UseCase(IPresenter<TRequest, TResponse> presenter = default, AbstractValidator<TRequest> validator = default)
    {
        _presenter = presenter ?? new PresenterNull<TRequest, TResponse>();
        _validator = validator ?? new ValidadorNull<TRequest>();
        _observers = new List<IUseCase>();
    }

    public async Task<UseCase<TRequest, TResponse>> HandleAsync(TRequest input)
    {
        try
        {
            _validationResult = _validator.Validate(input);

            _notifications.AddRange(_validationResult.Errors);

            if (_notifications.HasError())
            {
                _presenter.OnError(input, Notifications);

                Guard.ArgumentIsTrue(_presenter.Success, nameof(_presenter.Success));

                return this;
            }

            var cancelationToken = new CancellationToken();
            var result = await Handle(input, cancelationToken);

            _presenter.OnSuccess(result, Notifications, cancelationToken);

            Guard.ArgumentIsFalse(_presenter.Success, nameof(_presenter.Success));

            _complete = true;

            Notify();


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
    public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);

    /// <summary>
    /// Ação posterior ao OnExecute
    /// </summary>
    /// <param name="completed">Será true se OnExecute tiver sido executado completamente.</param>
    protected virtual void OnComplete(bool completed, List<Notification> notifications)
    {
        return;
    }

    private void Notify()
        => _observers.ForEach(o => ((IUseCase<TRequest, TResponse>)o).Handle(this));

    public void Subscribe(IUseCase observer)
    {
        if (!_observers.Contains(observer))
            _observers.Add(observer);
    }

    /// <summary>
    /// Método que será invocado pelo método Notify, quando estiver observando outro UseCase
    /// </summary>
    public virtual void Handle(IUseCase<TRequest, TResponse> other) { }

    /// <summary>
    /// Permite adicionar Notificações
    /// </summary>
    protected void SetNotification(string message, SeverityType severity)
        => _notifications.Add(new() { Message = message, Severity = severity });


}
