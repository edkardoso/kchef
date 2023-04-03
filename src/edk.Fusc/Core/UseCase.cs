using edk.Fusc.Contracts;
using edk.Fusc.Contracts.Common;
using edk.Fusc.Core.Events;
using edk.Fusc.Core.Mediator;
using edk.Fusc.Core.Presenters;
using edk.Fusc.Core.Validators;
using edk.Tools.NoIf.Comparer;
using edk.Tools.NoIf.Miscellaneous;
using System;

namespace edk.Fusc.Core;

public abstract class UseCase<TInput, TOutput> :
    IUseCase<TInput, TOutput>
{
    private TInput? _input;
    private FlowUseCase<TInput, TOutput>? _flow;
    private readonly List<INotification> _notifications = new();
    private readonly IPubSubMediator _pubSubMediator;

    protected IMediatorUseCase Mediator { get; private set; }
    public IPresenter<TInput, TOutput> Presenter { get; private set; }
    public virtual List<INotification> Notifications => _notifications ?? new();
    protected IUseCaseValidator<TInput> Validator { get; private set; }


    protected abstract string NameUseCase { get; }

    public bool HasMediator { get; private set; }

    public bool HasValidator { get; private set; }

    public bool HasPresenter { get; private set; }


    protected UseCase(IUseCaseValidator<TInput>? validator, IPresenter<TInput, TOutput>? presenter)
        : this(null, presenter, validator)
    { }

    protected UseCase(IMediatorUseCase? mediator = default, IPresenter<TInput, TOutput>? presenter = default, IUseCaseValidator<TInput>? validator = default)
    {
        Mediator = mediator ?? new UseCaseMediatorNull();
        Presenter = presenter ?? new PresenterDefault<TInput, TOutput>();
        Validator = validator ?? new ValidadorNull<TInput>();
        _pubSubMediator = Mediator.PubSub;
    }
    public async Task<IPresenter> HandleAsync(dynamic input)
    {
        if (input == null)
        {
            input = NoValue.Instance;
        }

        return await HandleAsync(input);
    }

    public async Task<IPresenter<TInput, TOutput>> HandleAsync(TInput input)
    {
        _input = input;
        return await HandleAsync();
    }

    public async Task<IPresenter<TInput, TOutput>> HandleAsync()
    {


        Notifications.Clear();

        _flow = new(_input, Mediator.User, this);

        var success = false;

        try
        {
            await _flow.Validate()
                        .Start(OnActionBeforeStartAsync)
                        .ExecuteAsync(OnExecuteAsync);

            success = true;
        }
        catch (AggregateException ex)
        {
            List<Exception> exceptions = new();

            foreach (Exception exception in ex.InnerExceptions)
            {
                exceptions.Add(exception);
            }

            _flow.Error(OnActionException, exceptions);

            await _pubSubMediator.PublishAsync(new UseCaseFailureEvent(this) { Exceptions = exceptions });


        }
        catch (Exception ex)
        {
            var exceptions = new List<Exception>() { ex };
            _flow.Error(OnActionException, exceptions);

            await _pubSubMediator.PublishAsync(new UseCaseFailureEvent(this) { Exceptions = exceptions });
        }
        finally
        {
            _flow.Complete(OnActionComplete);

            if (success)
            {
                await _pubSubMediator.PublishAsync(new UseCaseSuccessEvent(this));
            }
        }

        return Presenter;
    }

    /// <summary>
    /// Evento executado quando o UseCase é carregado
    /// </summary>
    public virtual void OnLoad(IMediatorUseCase mediator) { }

    /// <summary>
    /// Evento executado no método de Handler do UseCase. Se houer um Validator, esse evento
    /// somente será carregado após a validação com sucesso do mesmo.
    /// </summary>
    public abstract Task<TOutput> OnExecuteAsync(TInput input, CancellationToken cancellationToken);

    /// <summary>
    /// Evento disparado quando é publicado um Evento de UseCase
    /// </summary>
    public virtual Task OnEventAsync<TEvent>(TEvent useCaseEvent) where TEvent : IUseCaseEvent => Task.CompletedTask;

    /// <summary>
    /// Evento disparado após o Evento "OnExecuteAsync" ter sido concluído.
    /// </summary>
    /// <param name="completed">Será true se OnExecuteAsync tiver sido executado completamente.</param>
    protected virtual bool OnActionComplete(bool completed, IReadOnlyCollection<INotification> notifications) => true;
    
    /// <summary>
    /// Evento disparado quando ocorre uma exceção no processo de validação ou de execução do UseCase
    /// </summary>
    protected virtual bool OnActionException(List<Exception> exceptions, TInput? input, IUser user)
    {
        exceptions.ForEach(e => SetNotification(Notification.ErrorException(e.ToString())));

        return true;
    }

    /// <summary>
    /// Evento executado antes de iniciar o evento "OnExecuteAsync"
    /// </summary>
    protected virtual Task<bool> OnActionBeforeStartAsync(TInput? input, IUser user)
    {
        PublishEventStart(input);

        return Task.FromResult(true);
    }
  

    /// <summary>
    /// Permite adicionar Notificações
    /// </summary>
    protected void SetNotification(Notification notification) 
        => _notifications.Add(notification);

    /// <summary>
    /// Permite adicionar Notificações
    /// </summary>
    public void SetNotification(string message, SeverityType severity)
        => SetNotification(new Notification() { Message = message, Severity = severity });

    /// <summary>
    /// Configura o mediator no UseCase
    /// </summary>
    public void SetMediator(IMediatorUseCase mediator)
    {
        Mediator = mediator;
        HasMediator = Mediator.IsNull().Not();

        OnLoad(mediator);
    }

    /// <summary>
    /// Configura um Validator para o UseCase
    /// </summary>
    public void SetValidator(IUseCaseValidator validator)
    {
        Validator = (IUseCaseValidator<TInput>)validator;
        HasValidator = Validator.IsNull().Not();
    }

    /// <summary>
    /// Configura um Presenter para o UseCase
    /// </summary>
    public void SetPresenter(IPresenter presenter)
    {
        Presenter = (IPresenter<TInput, TOutput>)presenter;
        HasPresenter = Presenter.IsNull().Not();
    }

    /// <summary>
    /// Permite assinar eventos deste UseCase
    /// </summary>
    /// <typeparam name="TRecipient">UseCase que está se inscrevendo</typeparam>
    /// <typeparam name="TEvent">Evento que deseja receber</typeparam>
    public void Subscription<TRecipient, TEvent>()
          where TRecipient : IUseCase
         where TEvent : IUseCaseEvent
    {
        _pubSubMediator.SubscribeTo<TRecipient, TEvent>(this);
    }

    public IReadOnlyCollection<INotification> Validate(TInput? input) 
        => Validator.Validate(input);


    private void PublishEventStart(TInput? input)
    {
        var @event = new UseCaseStartEvent(this);
        if (input != null)
        {
            @event.Input = input;
        }

        _pubSubMediator.PublishAsync(@event);
    }
}