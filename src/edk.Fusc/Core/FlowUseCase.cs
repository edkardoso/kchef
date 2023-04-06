using edk.Fusc.Contracts;
using edk.Fusc.Contracts.Common;
using edk.Fusc.Core.Mediator;
using edk.Fusc.Core.Validators;
using edk.Tools.NoIf;
using edk.Tools.NoIf.Boolean;
using edk.Tools.NoIf.Comparer;
using edk.Tools.NoIf.Miscellaneous;

namespace edk.Fusc.Core;

internal class FlowUseCase<TInput, TOutput>
{
    private readonly TInput? _input;
    private readonly IUser _user;
    private readonly UseCase<TInput, TOutput> _useCase;
    private readonly IMediatorUseCase _mediator;
    private readonly SetupUseCase _setup;

    private bool Continue { get; set; }
    public bool Completed { get; set; }


    internal FlowUseCase(UseCase<TInput, TOutput> useCase, TInput? input)
    {
        _useCase = useCase;
        _mediator = useCase.Mediator;
        _setup = useCase.Setup;
        _input = input;
        _user = useCase.Mediator.User;
    }



    internal async Task Execute(Func<TInput?, IUser, Task<bool>> onActionBeforeStartAsync
        , Func<TInput?, CancellationToken, Task<TOutput>> onExecuteAsync
        , Func<List<Exception>, TInput?, IUser, bool> onActionException
        , Func<bool, IReadOnlyCollection<INotification>, bool> onActionComplete)
    {
        try
        {
            Validate();
            PublishEventStart();
            Start(onActionBeforeStartAsync);
            await ExecuteAsync(onExecuteAsync);
            Completed = true;
        }
        catch (AggregateException ex)
        {
            ExceptionHandling(onActionException, (List<Exception>)ConvertToList(ex));

        }
        catch (Exception ex)
        {
            ExceptionHandling(onActionException, new() { ex });

        }
        finally
        {
            PublishComplete();
            Complete(onActionComplete);
        }

    }

    private static IEnumerable<Exception> ConvertToList(AggregateException ex)
    {

        foreach (Exception exception in ex.InnerExceptions)
        {
            yield return exception;
        }

    }

    private void Validate()
    {
        _useCase.Validate(_input)
             .IfNotNull((obj) => _useCase.Notifications.AddRange(obj));

        _useCase.Notifications
            .HasError()
            .IfTrue(() => _useCase.Presenter.OnErrorValidation(_input, _useCase.Notifications));

        Continue = _useCase.Notifications.NoErrors();


    }

    private void Start(Func<TInput?, IUser, Task<bool>> onActionBeforeStart)
    {
        if (Continue.IsFalse())
            return;

        Continue = NoIfMiscellaneous
                    .IfAllTrue(onActionBeforeStart.Invoke(_input, _user).Result, _useCase.Notifications.NoErrors())
                    .IfFalse(() => _useCase.Presenter.OnErrorValidation(_input, _useCase.Notifications));

    }

    private void PublishEventStart()
    {
        if (Continue && _setup.PublishStartEvent)
            _mediator.PublishEventStart(_useCase, _input, _setup.WaitingCompleteStartEvent);
    }


    private async Task ExecuteAsync(Func<TInput?, CancellationToken, Task<TOutput>> onExecuteAsync)
           => await Continue.IfTrueAsync(() =>
           {
               var result = onExecuteAsync(_input, Task.Factory.CancellationToken).Result;
               _useCase.Presenter.SetOutput(result);
               _useCase.Presenter.OnResult(result, _useCase.Notifications, Task.Factory.CancellationToken);
           });

    private void Error(Func<List<Exception>, TInput?, IUser, bool> onActionException, List<Exception> exceptions)
       => onActionException(exceptions, _input, _user)
           .If(
               whenTrue: () =>
               {
                   exceptions.ForEach(ex => _useCase.SetNotification(ex.ToString(), SeverityType.Exception));
                   _useCase.Presenter.OnError(exceptions, _input);
               },
               whenFalse: () => exceptions.ForEach(ex => _useCase.SetNotification(ex.ToString(), SeverityType.Warning))
           );

    private void PublishComplete()
    {
        if (_setup.PublishSuccessEvent && Completed)
        {
            _mediator.PublishEventSuccess(_useCase, _input, _useCase.Presenter.Output, _useCase.Notifications, _setup.WaitingCompleteSuccessEvent);
        }
    }


    private void Complete(Func<bool, IReadOnlyCollection<INotification>, bool> onActionComplete)
        => Continue.IfTrue(() => onActionComplete(Completed, _useCase.Notifications));


    private void PublishFailure(List<Exception> exceptions)
    {
        if (_setup.PublishFailureEvent)
            _mediator.PublishEventFailureAsync(_useCase, _input, exceptions, _setup.WaitingCompleteFailureEvent);
    }


    private void ExceptionHandling(Func<List<Exception>, TInput?, IUser, bool> onActionException, List<Exception> exceptions)
    {
        PublishFailure(exceptions);

        Error(onActionException, exceptions);
    }

}

