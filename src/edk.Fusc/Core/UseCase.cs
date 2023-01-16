using edk.Fusc.Core.Events;
using edk.Fusc.Core.Mediator;
using edk.Fusc.Core.Presenters;
using edk.Fusc.Core.Validators;
using FluentValidation;
using FluentValidation.Results;

namespace edk.Fusc.Core;

public abstract class UseCase<TInput, TOutput> :
    IUseCase<TInput, TOutput>
{
    private readonly AbstractValidator<TInput> _validator;
    private readonly IPresenter<TInput, TOutput> _presenter;
    private bool _complete;
    private ValidationResult _validationResult;
    private readonly Dictionary<string, IUseCase> _observers;
    private readonly List<IUseCaseEvent> _useCaseEvents = new();
    private List<Notification> _notifications = new();
    private TInput _input;

    protected IMediatorUseCase Mediator { get; private set; }
    public IPresenter<TInput, TOutput> Presenter => _presenter;
    public List<Notification> Notifications => _notifications;
    protected abstract string NameUseCase { get; }

    protected UseCase(IPresenter<TInput, TOutput> presenter = default, AbstractValidator<TInput> validator = default)
    {
        _presenter = presenter ?? new PresenterDefault<TInput, TOutput>();
        _validator = validator ?? new ValidadorNull<TInput>();
        _observers = new();
    }
    public async Task<IPresenter> HandleAsync(dynamic input) => await HandleAsync(input);

    public async Task<IPresenter<TInput, TOutput>> HandleAsync(TInput input)
    {
        _input = input;
        return await HandleAsync();
    }

    public async Task<IPresenter<TInput, TOutput>> HandleAsync()
    {
        try
        {
            IUser user = GetUserOrDefault();

            OnActionBeforeStart(_input, user);

            _useCaseEvents.Add(new UseCaseStartEvent(this));

            _validationResult = _validator.Validate(_input);

            _notifications.AddRange(_validationResult.Errors);

            if (_notifications.HasError())
            {
                _useCaseEvents.Add(new UseCaseErrorEvent(this));
                _presenter.OnError(_input, Notifications);

                _presenter.SetSuccess(false);

                return _presenter;
            }

            var cancelationToken = new CancellationToken();
            var result = await OnExecuteAsync(_input, cancelationToken);

            _useCaseEvents.Add(new UseCaseSuccessEvent(this));
            _presenter.SetOutput(result);
            _presenter.SetSuccess(true);
            _presenter.OnSuccess(result, Notifications, cancelationToken);


            _complete = true;
            _useCaseEvents.Add(new UseCaseCompleteEvent(this));

        }
        catch (Exception ex)
        {
            _useCaseEvents.Add(new UseCaseExceptionEvent(this));


            if (OnActionException(ex, _input, GetUserOrDefault()))
            {
                _presenter.OnException(ex, _input);
            }
        }
        finally
        {
            OnActionComplete(_complete, _notifications);
            Notify();
        }

        return _presenter;

        IUser GetUserOrDefault() => Mediator is null ? new UserNull() : ((UseCaseMediator)Mediator).User;
    }

    protected virtual void OnActionBeforeStart(TInput input, IUser user)
    {

    }


    /// <summary>
    /// Ação a ser executada quando os dados de entrada forem validados com sucesso
    /// </summary>
    public abstract Task<TOutput> OnExecuteAsync(TInput input, CancellationToken cancellationToken);

    /// <summary>
    /// Ação posterior ao OnExecute
    /// </summary>
    /// <param name="completed">Será true se OnExecute tiver sido executado completamente.</param>
    protected virtual void OnActionComplete(bool completed, List<Notification> notifications)
    {
        return;
    }

    protected virtual bool OnActionException(Exception exception, TInput input, IUser user)
    {
        return true;
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

    private static string GetNameEvent(IUseCase observer, Type typeEvent)
        => observer.GetType().Name + typeEvent.GetType().Name;

    /// <summary>
    /// Método que será invocado pelo método Notify, quando estiver observando outro UseCase
    /// </summary>
    public virtual Task OnEventAsync(IUseCaseEvent useCaseEvent) => Task.CompletedTask;

    /// <summary>
    /// Permite adicionar Notificações
    /// </summary>
    protected void SetNotification(string message, SeverityType severity)
        => _notifications.Add(new() { Message = message, Severity = severity });

    /// <summary>
    /// Configura o mediator no UseCase
    /// </summary>
    public void SetMediator(IMediatorUseCase mediator) => Mediator = mediator;

    protected async Task<NoValue> NoValueTask() => await Task.FromResult(NoValue.Create);
}

