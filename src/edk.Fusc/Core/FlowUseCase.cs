using edk.Fusc.Core.Mediator;
using edk.Fusc.Core.Validators;

namespace edk.Fusc.Core;

public class FlowUseCase<TInput, TOutput>
{
    private readonly TInput _input;
    private readonly IUser _user;
    private readonly UseCase<TInput, TOutput> _useCase;
    private bool _complete;

    public FlowUseCase(TInput input, IUser user, UseCase<TInput, TOutput> useCase)
    {
        _input = input;
        _user = user;
        _useCase = useCase;
    }

    private bool Continue { get; set; }
    private bool Stop => !Continue;

    public FlowUseCase<TInput, TOutput> Start(Func<TInput, IUser, bool> onActionBeforeStart)
    {
        Continue = onActionBeforeStart.Invoke(_input, _user) && _useCase.Notifications.NoErrors();

        if (Continue)
        {
            //_useCaseEvents.Add(new UseCaseStartEvent(this));
        }
        else
        {
            _useCase.Presenter.OnErrorValidation(_input, _useCase.Notifications);
        }

        return this;

    }

    public FlowUseCase<TInput, TOutput> Validate()
    {
        if (Stop)
            return this;

        var validationResult = _useCase.Validator.Validate(_input);
        _useCase.Notifications.AddRange(validationResult.Errors);
        _useCase.Presenter.SetSuccess(_useCase.Notifications.NoErrors());

        if (_useCase.Notifications.HasError())
            _useCase.Presenter.OnErrorValidation(_input, _useCase.Notifications);

        return this;
    }

    public async Task ExecuteAsync(Func<TInput, CancellationToken, Task<TOutput>> onExecuteAsync)
    {
        if (Stop)
            return;

        var result = await onExecuteAsync.Invoke(_input, Task.Factory.CancellationToken);
        _useCase.Presenter.SetOutput(result);
        _complete = true;
        _useCase.Presenter.OnResult(result, _useCase.Notifications, Task.Factory.CancellationToken);

        return;
    }

    public void Error(Func<Exception, TInput, IUser, bool> onActionException, Exception exception)
    {
        if (onActionException(exception, _input, _user))
        {
            _useCase.SetNotification(exception.Message, SeverityType.Error);
            _useCase.Presenter.OnError(exception, _input);
        }
        else
        {
            _useCase.SetNotification(exception.Message, SeverityType.Warning);
        }
    }

    public void Complete(Func<bool, IReadOnlyCollection<Notification>, bool> onActionComplete)
    {
        if (Continue && onActionComplete(_complete, _useCase.Notifications))
        {
            //_useCaseEvents.Add(new UseCaseCompleteEvent(this));

            // Notify();
        }
    }
}

