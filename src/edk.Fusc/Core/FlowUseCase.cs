using edk.Fusc.Contracts;
using edk.Fusc.Contracts.Common;
using edk.Fusc.Core.Validators;
using edk.Tools.NoIf;
using edk.Tools.NoIf.Boolean;
using edk.Tools.NoIf.Comparer;
using edk.Tools.NoIf.Miscellaneous;

namespace edk.Fusc.Core;

public class FlowUseCase<TInput, TOutput>
{
    private readonly TInput? _input;
    private readonly IUser _user;
    private readonly UseCase<TInput, TOutput> _useCase;
    private bool _complete;

    internal FlowUseCase(TInput? input, IUser user, UseCase<TInput, TOutput> useCase)
    {
        _input = input;
        _user = user;
        _useCase = useCase;
    }

    private bool Continue { get; set; }

    internal FlowUseCase<TInput, TOutput> Start(Func<TInput?, IUser, Task<bool>> onActionBeforeStart)
    {
        if (Continue.IsFalse())
            return this;


        Continue = NoIfMiscellaneous
                    .IfAllTrue(onActionBeforeStart.Invoke(_input, _user).Result, _useCase.Notifications.NoErrors())
                    .IfFalse(() => _useCase.Presenter.OnErrorValidation(_input, _useCase.Notifications));


        return this;

    }

    internal FlowUseCase<TInput, TOutput> Validate()
    {

        _useCase.Validate(_input)
             .IfNotNull((obj) => _useCase.Notifications.AddRange(obj));

        _useCase.Notifications
            .HasError()
            .IfTrue(() => _useCase.Presenter.OnErrorValidation(_input, _useCase.Notifications));

        Continue = _useCase.Notifications.NoErrors();


        return this;
    }

    public async Task ExecuteAsync(Func<TInput?, CancellationToken, Task<TOutput>> onExecuteAsync)
        => await Continue.IfTrueAsync(() =>
            {
                var result = onExecuteAsync(_input, Task.Factory.CancellationToken).Result;
                _useCase.Presenter.SetOutput(result);
                _complete = true;
                _useCase.Presenter.OnResult(result, _useCase.Notifications, Task.Factory.CancellationToken);
            });

    public void Error(Func<List<Exception>, TInput?, IUser, bool> onActionException, List<Exception> exceptions)
       => onActionException(exceptions, _input, _user)
           .If(
               whenTrue: () =>
               {
                   exceptions.ForEach(ex => _useCase.SetNotification(ex.ToString(), SeverityType.Exception));
                   _useCase.Presenter.OnError(exceptions, _input);
               },
               whenFalse: () => exceptions.ForEach(ex => _useCase.SetNotification(ex.ToString(), SeverityType.Warning))
           );

    public void Complete(Func<bool, IReadOnlyCollection<INotification>, bool> onActionComplete)
        => NoIfMiscellaneous.IfAllTrue(Continue, onActionComplete(_complete, _useCase.Notifications))
            .IfTrue(() =>
            {
               
            });
}

