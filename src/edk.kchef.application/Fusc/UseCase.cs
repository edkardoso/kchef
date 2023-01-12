using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using edk.Kchef.Application.Fusc.Events;
using edk.Kchef.Application.Fusc.Mediator;
using edk.Kchef.Application.Fusc.Presenters;
using edk.Kchef.Application.Fusc.Validators;
using edk.Kchef.Domain.Common.Base;
using FluentValidation;
using FluentValidation.Results;

namespace edk.Kchef.Application.Fusc;

public abstract class UseCase<TRequest, TResponse> :
    IUseCase<TRequest, TResponse>
{
    private readonly AbstractValidator<TRequest> _validator;
    private readonly IPresenter<TRequest, TResponse> _presenter;
    private bool _complete;
    private ValidationResult _validationResult;
    private readonly Dictionary<string, IUseCase> _observers;
    private readonly List<IUseCaseEvent> _useCaseEvents = new();
    private List<Notification> _notifications = new();


    protected IMediatorUseCase Mediator { get; private set; }
    public IPresenter<TRequest, TResponse> Presenter => _presenter;
    public List<Notification> Notifications => _notifications;

    protected abstract string NameUseCase { get; }


    protected UseCase(IPresenter<TRequest, TResponse> presenter = default, AbstractValidator<TRequest> validator = default)
    {
        _presenter = presenter ?? new PresenterDefault<TRequest, TResponse>();
        _validator = validator ?? new ValidadorNull<TRequest>();
        _observers = new();
    }
    public async Task<IPresenter> HandleAsync(dynamic input) => await HandleAsync(input);

    public async Task<IPresenter<TRequest, TResponse>> HandleAsync(TRequest input)
    {
        try
        {
            _useCaseEvents.Add(new UseCaseStartEvent(this));

            _validationResult = _validator.Validate(input);

            _notifications.AddRange(_validationResult.Errors);

            if (_notifications.HasError())
            {
                _useCaseEvents.Add(new UseCaseErrorEvent(this));
                _presenter.OnError(input, Notifications);

                // Guard.ArgumentIsTrue(_presenter.Success, nameof(_presenter.Success));

                return _presenter;
            }

            var cancelationToken = new CancellationToken();
            var result = await ExecuteAsync(input, cancelationToken);

            _useCaseEvents.Add(new UseCaseSuccessEvent(this));
            _presenter.OnSuccess(result, Notifications, cancelationToken);

            // Guard.ArgumentIsFalse(_presenter.Success, nameof(_presenter.Success));

            _complete = true;
            _useCaseEvents.Add(new UseCaseCompleteEvent(this));



        }
        catch (Exception ex)
        {
            _useCaseEvents.Add(new UseCaseExceptionEvent(this));

            // Pode ser necessário adicionar um novo evento virtual de Exceção
            // para cenários onde há a necessidade de se desfazer alguma ação anterior
            // em caso de exceção. Embora o OnComplete(false) talvez já permita isso.
            _presenter.OnException(input, ex);
            

        }
        finally
        {
            OnComplete(_complete, _notifications);
            Notify();

        }

        return _presenter;

    }
    /// <summary>
    /// Ação a ser executada quando os dados de entrada forem validados com sucesso
    /// </summary>
    public abstract Task<TResponse> ExecuteAsync(TRequest request, CancellationToken cancellationToken);

    /// <summary>
    /// Ação posterior ao OnExecute
    /// </summary>
    /// <param name="completed">Será true se OnExecute tiver sido executado completamente.</param>
    protected virtual void OnComplete(bool completed, List<Notification> notifications)
    {
        return;
    }

    protected void Emit(IUseCaseEvent useCaseEvent)
    {
        var typeEvent = useCaseEvent.GetType();

        foreach (var key in _observers.Keys)
        {
            var observer = _observers[key];
            var nameEvent = GetNameEvent(observer, typeEvent);

            if (key.Equals(nameEvent))
            {
                observer.OnEventAsync(useCaseEvent);
            }
        }
    }

    private void Notify()
        => _useCaseEvents.ForEach(@event => Emit(@event));

    public void Subscribe<TEvent>(IUseCase observer) where TEvent : IUseCaseEvent
    {
        var key = GetNameEvent(observer, typeof(TEvent));

        if (!_observers.ContainsKey(key))
            _observers.Add(key, observer);
    }

    private string GetNameEvent(IUseCase observer, Type typeEvent)
    {

        return observer.GetType().Name + typeEvent.GetType().Name;
    }

    /// <summary>
    /// Método que será invocado pelo método Notify, quando estiver observando outro UseCase
    /// </summary>
    public virtual Task OnEventAsync(IUseCaseEvent useCaseEvent)
    {
        return Task.CompletedTask;

    }

    /// <summary>
    /// Permite adicionar Notificações
    /// </summary>
    protected void SetNotification(string message, SeverityType severity)
        => _notifications.Add(new() { Message = message, Severity = severity });

    /// <summary>
    /// Configura o mediator no UseCase
    /// </summary>
    public void SetMediator(IMediatorUseCase mediator) => Mediator = mediator;
}
