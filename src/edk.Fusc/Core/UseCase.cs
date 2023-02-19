﻿using edk.Fusc.Contracts;
using edk.Fusc.Contracts.Common;
using edk.Fusc.Core.Mediator;
using edk.Fusc.Core.Presenters;
using edk.Fusc.Core.Validators;
using System;
using System.Linq;
using System.Text;

namespace edk.Fusc.Core;
public abstract class UseCase<TInput, TOutput> :
    IUseCase<TInput, TOutput>
{
    private TInput? _input;
    private FlowUseCase<TInput, TOutput>? _flow;
    private readonly List<INotification> _notifications = new();

    protected IMediatorUseCase Mediator { get; private set; }
    public IPresenter<TInput, TOutput> Presenter { get; private set; }
    public virtual List<INotification> Notifications => _notifications ?? new();
    public IUseCaseValidator<TInput> Validator { get; private set; }
    protected abstract string NameUseCase { get; }

    // Requerid for Test
    protected UseCase() : this(null, null, null)
    { }

    protected UseCase(IMediatorUseCase? mediator = default, IPresenter<TInput, TOutput>? presenter = default, IUseCaseValidator<TInput>? validator = default)
    {
        Mediator = mediator ?? new MediatorNull();
        Presenter = presenter ?? new PresenterDefault<TInput, TOutput>();
        Validator = validator ?? new ValidadorNull<TInput>();
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

        _flow = new(_input, Mediator.User, this);

        try
        {
            await _flow.Start(OnActionBeforeStart)
                       .Validate()
                       .ExecuteAsync(OnExecuteAsync);
        }
        catch (AggregateException ex)
        {
            List<Exception> exceptions = new();
      
            foreach (Exception exception in ex.InnerExceptions)
            {
                exceptions.Add(exception);
            }

            _flow.Error(OnActionException, exceptions);
        }
        catch (Exception ex)
        {
            _flow.Error(OnActionException, new() { ex });
        }
        finally
        {
            _flow.Complete(OnActionComplete);
        }

        return Presenter;
    }


    /// <summary>
    /// Ação a ser executada quando os dados de entrada forem validados com sucesso
    /// </summary>
    public abstract Task<TOutput> OnExecuteAsync(TInput input, CancellationToken cancellationToken);
    public virtual Task OnEventAsync<TEvent>(TEvent useCaseEvent) where TEvent : IUseCaseEvent => Task.CompletedTask;

    /// <summary>
    /// Ação posterior ao OnExecute
    /// </summary>
    /// <param name="completed">Será true se OnExecute tiver sido executado completamente.</param>
    protected virtual bool OnActionComplete(bool completed, IReadOnlyCollection<INotification> notifications) => true;
    protected virtual bool OnActionException(List<Exception> exceptions, TInput input, IUser user)
    {
        exceptions.ForEach(e => SetNotification(Notification.Error(e.Message)));

         return true;
    }

    protected virtual bool OnActionBeforeStart(TInput input, IUser user) => true;

    protected void Emit(IUseCaseEvent useCaseEvent)
        => Mediator.Publish(useCaseEvent);

    protected void Register<TEvent, TUseCaseSender>()
        where TEvent : IUseCaseEvent
        where TUseCaseSender : IUseCase
        => Mediator.Subscribe<TEvent, TUseCaseSender>(this);

    /// <summary>
    /// Permite adicionar Notificações
    /// </summary>
    protected void SetNotification(Notification notification)
    {
        _notifications.Add(notification);

        if (Presenter.Success)
            Presenter.SetSuccess(_notifications.NoErrors());

    }
    public void SetNotification(string message, SeverityType severity)
        => SetNotification(new Notification() { Message = message, Severity = severity });

    /// <summary>
    /// Configura o mediator no UseCase
    /// </summary>
    public void SetMediator(IMediatorUseCase mediator) => Mediator = mediator;
    protected async Task<NoValue> NoValueTask() => await Task.FromResult(NoValue.Create);

}