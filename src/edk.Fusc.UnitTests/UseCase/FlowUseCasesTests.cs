using edk.Fusc.Contracts;
using edk.Fusc.Core;
using edk.Fusc.Core.Mediator;
using edk.Fusc.Core.Validators;
using edk.Fusc.UnitTests.Helper;
using Moq;
using Moq.Protected;

namespace edk.Fusc.UnitTests.UseCase;

public class FlowUseCasesTests
{
    public class UseCaseFlowTest : UseCase<string, bool>
    {
        protected override string NameUseCase => throw new NotImplementedException();

        public override Task<bool> OnExecuteAsync(string? input, CancellationToken cancellationToken)
        {
            if(input == "error")
            {
                throw new ArgumentException(nameof(input));
            }

            return Task.FromResult(true);
        }
    }


    [Fact]
    public async Task ShouldOnlyInvokeMethodsOfNormalFlow()
    {
        // arrange
        var useCaseMock = new Mock<UseCaseFlowTest>() { CallBase = true }; // callbase=true não substitui o método deixando o comportamento padrão
        var useCase = useCaseMock.Object;

        // action
        _ = await useCase.HandleAsync();

        // assert
        useCaseMock.Protected().Verify(ActionMethodsName.OnActionBeforeStartAsync, Times.Once(), ItExpr.IsAny<string>(), ItExpr.IsAny<IUser>());
        useCaseMock.Verify(s => s.OnExecuteAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once());
        useCaseMock.Protected().Verify(ActionMethodsName.OnActionComplete, Times.Once(), ItExpr.IsAny<bool>(), ItExpr.IsAny<IReadOnlyCollection<INotification>>());
        useCaseMock.Protected().Verify(ActionMethodsName.OnActionException, Times.Never(), ItExpr.IsAny<List<Exception>>(), ItExpr.IsAny<string>(), ItExpr.IsAny<IUser>());

    }


    [Fact]
    public async Task OnActionBeforeStart_WhenReturnFalse_MustStopExecution()
    {
        // arrange
        var useCaseMock = new Mock<UseCaseFlowTest>() { CallBase = true };
        useCaseMock.Protected()
            .Setup<Task<bool>>(ActionMethodsName.OnActionBeforeStartAsync, ItExpr.IsAny<string>(), ItExpr.IsAny<UserNull>())
            .Returns(Task.FromResult(false));

        var useCase = useCaseMock.Object;

        // action
        _ = await useCase.HandleAsync();

        // assert
        useCaseMock.Verify(s => s.OnExecuteAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Never());
        useCaseMock.Protected().Verify(ActionMethodsName.OnActionComplete, Times.Never(), ItExpr.IsAny<bool>(), ItExpr.IsAny<IReadOnlyCollection<Notification>>());
        useCaseMock.Protected().Verify(ActionMethodsName.OnActionException, Times.Never(), ItExpr.IsAny<List<Exception>>(), ItExpr.IsAny<string>(), ItExpr.IsAny<IUser>());
    }

    [Fact]
    public async Task ShouldInvokeAllMethodsToExceptionFlowWhenAnExceptionOccursInOnExecuteAsync()
    {
        // arrange
        var useCaseMock = new Mock<UseCaseFlowTest>() { CallBase = true };
        useCaseMock.Setup(s => s.OnExecuteAsync(It.IsAny<string>(), It.IsAny<CancellationToken>())).Throws<Exception>();
        var useCase = useCaseMock.Object;

        // action
        var presenter = await useCase.HandleAsync();

        // assert
        useCaseMock.Protected().Verify(ActionMethodsName.OnActionBeforeStartAsync, Times.Once(), ItExpr.IsAny<string>(), ItExpr.IsAny<IUser>());
        useCaseMock.Protected().Verify(ActionMethodsName.OnActionException, Times.Once(), ItExpr.IsAny<List<Exception>>(), ItExpr.IsAny<string>(), ItExpr.IsAny<IUser>());
        useCaseMock.Protected().Verify(ActionMethodsName.OnActionComplete, Times.Once(), ItExpr.IsAny<bool>(), ItExpr.IsAny<IReadOnlyCollection<INotification>>());
    }

}
