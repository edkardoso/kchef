using edk.Fusc.Core.Events;
using edk.Fusc.Core.Mediator;
using edk.Fusc.Core.Presenters;
using edk.Fusc.Core.Validators;
using FluentValidation;

namespace edk.Fusc.Core;

public abstract class UseCase<TInput, TOutput> :
    IUseCase<TInput, TOutput>
{
    private TInput? _input;
    private FlowUseCase<TInput, TOutput>? _flow;
    private readonly AbstractValidator<TInput> _validator;
    private readonly IPresenter<TInput, TOutput> _presenter;
    private readonly List<Notification> _notifications = new();
    private readonly EventsCollection _useCaseEvents = new();

    protected IMediatorUseCase? Mediator { get; private set; }
    public IPresenter<TInput, TOutput> Presenter => _presenter;
    public virtual IReadOnlyCollection<Notification> Notifications => _notifications ?? new();

    public AbstractValidator<TInput> Validator => _validator;
    protected abstract string NameUseCase { get; }

    protected UseCase() : this(null, null)
    { }
    protected UseCase(IPresenter<TInput, TOutput>? presenter = default, AbstractValidator<TInput>? validator = default)
    {
        _presenter = presenter ?? new PresenterDefault<TInput, TOutput>();
        _validator = validator ?? new ValidadorNull<TInput>();
    }
    public async Task<IPresenter> HandleAsync(dynamic input) => await HandleAsync(input);

    public async Task<IPresenter<TInput, TOutput>> HandleAsync(TInput input)
    {
        _input = input;
        return await HandleAsync();
    }

    public async Task<IPresenter<TInput, TOutput>> HandleAsync()
    {
        if (_input == null)
            throw new ArgumentNullException(nameof(_input));

        _flow = new(_input, GetUserOrDefault(), this);

        try
        {
            await _flow.Start(OnActionBeforeStart)
                       .Validate()
                       .ExecuteAsync(OnExecuteAsync);
        }
        catch (Exception ex)
        {
            _flow.Error(OnActionException, ex);
        }
        finally
        {
            _flow.Complete(OnActionComplete);
        }

        return _presenter;

        IUser GetUserOrDefault() => Mediator is null ? new UserNull() : ((UseCaseMediator)Mediator).User;
    }

    protected virtual bool OnActionBeforeStart(TInput input, IUser user) => true;

    /// <summary>
    /// Ação a ser executada quando os dados de entrada forem validados com sucesso
    /// </summary>
    public abstract Task<TOutput> OnExecuteAsync(TInput input, CancellationToken cancellationToken);

    /// <summary>
    /// Ação posterior ao OnExecute
    /// </summary>
    /// <param name="completed">Será true se OnExecute tiver sido executado completamente.</param>
    protected virtual bool OnActionComplete(bool completed, IReadOnlyCollection<Notification> notifications) => true;

    protected virtual bool OnActionException(Exception exception, TInput input, IUser user) => true;



    protected void Emit(IUseCaseEvent useCaseEvent)
    {
        //var typeEvent = useCaseEvent.GetType();

        //foreach (var key in _observers.Keys)
        //{
        //    var observer = _observers[key];
        //    var nameEvent = GetNameEvent(observer, typeEvent);

        //    if (key.Equals(nameEvent))
        //    {
        //        observer.OnEventAsync(useCaseEvent);
        //    }
        //}
    }

    //private void Notify()
    //    => _useCaseEvents.ForEach(@event => Emit(@event));


    /// <summary>
    /// Método que será invocado pelo método Notify, quando estiver observando outro UseCase
    /// </summary>
    public virtual Task OnEventAsync(IUseCaseEvent useCaseEvent) => Task.CompletedTask;

    /// <summary>
    /// Permite adicionar Notificações
    /// </summary>
    public void SetNotification(string message, SeverityType severity)
    {
        _notifications.Add(new() { Message = message, Severity = severity });

        if (_presenter.Success)
            _presenter.SetSuccess(_notifications.NoErrors());

    }

    /// <summary>
    /// Configura o mediator no UseCase
    /// </summary>
    public void SetMediator(IMediatorUseCase mediator)
    {
        Mediator = mediator;
        AddEvents(_useCaseEvents, mediator);
    }

    protected virtual void AddEvents(EventsCollection @events, IMediatorUseCase mediator)
    {
        @events.Add(new UseCaseStartEvent(this, mediator));
        @events.Add(new UseCaseCompleteEvent(this, mediator));
    }

    protected async Task<NoValue> NoValueTask() => await Task.FromResult(NoValue.Create);

   

}

