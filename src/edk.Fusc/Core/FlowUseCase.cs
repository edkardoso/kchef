using edk.Fusc.Contracts;
using edk.Fusc.Contracts.Common;
using edk.Fusc.Core.Validators;
using edk.Tools;

namespace edk.Fusc.Core;

public class FlowUseCase<TInput, TOutput>
{
    private readonly TInput _input;
    private readonly IUser _user;
    private readonly UseCase<TInput, TOutput> _useCase;
    private bool _complete;

    internal FlowUseCase(TInput input, IUser user, UseCase<TInput, TOutput> useCase)
    {
        _input = input;
        _user = user;
        _useCase = useCase;
    }

    private bool Continue { get; set; }

    internal FlowUseCase<TInput, TOutput> Start(Func<TInput, IUser, bool> onActionBeforeStart)
    {
        Continue = EvaluateLibrary.And(onActionBeforeStart.Invoke(_input, _user), _useCase.Notifications.NoErrors())
                    .Eval(() => { },
                          () => _useCase.Presenter.OnErrorValidation(_input, _useCase.Notifications)
                    );

        return this;

    }

    internal FlowUseCase<TInput, TOutput> Validate()
    {
        Continue.WhenTrue(() =>
        {

            _useCase.Validator
             .Validate(_input)
             .WhenNotNull((obj) => _useCase.Notifications.AddRange(obj));

            _useCase.Presenter.SetSuccess(_useCase.Notifications.NoErrors());

            _useCase.Notifications
                .HasError()
                .WhenTrue(() => _useCase.Presenter.OnErrorValidation(_input, _useCase.Notifications));

        });

        return this;
    }

    public async Task ExecuteAsync(Func<TInput, CancellationToken, Task<TOutput>> onExecuteAsync)
        => await Continue.WhenTrueAsync(() =>
            {
                var result = onExecuteAsync(_input, Task.Factory.CancellationToken).Result;
                _useCase.Presenter.SetOutput(result);
                _complete = true;
                _useCase.Presenter.OnResult(result, _useCase.Notifications, Task.Factory.CancellationToken);
            });

    public void Error(Func<List<Exception>, TInput, IUser, bool> onActionException, List<Exception> exceptions)
       => onActionException(exceptions, _input, _user)
           .Eval(
               whenTrue: () =>
               {
                   exceptions.ForEach(e => _useCase.SetNotification(e.Message, SeverityType.Warning));

                   _useCase.Presenter.OnError(exceptions, _input);
               }
               , whenFalse: () =>
               {
                   exceptions.ForEach(e => _useCase.SetNotification(e.Message, SeverityType.Warning));

               }
           );

    public void Complete(Func<bool, IReadOnlyCollection<INotification>, bool> onActionComplete)
        => EvaluateLibrary.And(Continue, onActionComplete(_complete, _useCase.Notifications))
            .WhenTrue(() =>
            {
                //_useCaseEvents.Add(new UseCaseCompleteEvent(this));
                // Notify();

            });
}

