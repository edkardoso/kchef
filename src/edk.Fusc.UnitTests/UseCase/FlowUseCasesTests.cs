using edk.Fusc.Core;
using edk.Fusc.Core.Mediator;
using edk.Fusc.Core.Validators;
using edk.Fusc.UnitTests.Helper;
using Moq;
using Moq.Protected;

namespace edk.Fusc.UnitTests.UseCase;

public class FlowUseCasesTests
{
    [Fact]
    public async Task ShouldOnlyInvokeMethodsOfNormalFlow()
    {
        // arrange
        var useCaseMock = new Mock<UseCase<NoValue, bool>>() { CallBase = true }; // callbase=true não substitui o método deixando o comportamento padrão
        var useCase = useCaseMock.Object;

        // action
        _ = await useCase.HandleAsync();

        // assert
        useCaseMock.Protected().Verify(ActionMethodsName.OnActionBeforeStart, Times.Once(), ItExpr.IsAny<NoValue>(), ItExpr.IsAny<IUser>());
        useCaseMock.Verify(s => s.OnExecuteAsync(It.IsAny<NoValue>(), It.IsAny<CancellationToken>()), Times.Once());
        useCaseMock.Protected().Verify(ActionMethodsName.OnActionComplete, Times.Once(), ItExpr.IsAny<bool>(), ItExpr.IsAny<IReadOnlyCollection<Notification>>());
        useCaseMock.Protected().Verify(ActionMethodsName.OnActionException, Times.Never(), ItExpr.IsAny<Exception>(), ItExpr.IsAny<NoValue>(), ItExpr.IsAny<IUser>());

    }


    [Fact]
    public async Task MustStopExecutionAndNotInvokeAnotherMethodWhenOnActionBeforeStartReturFalse()
    {
        // arrange
        var useCaseMock = new Mock<UseCase<NoValue, bool>>();
        useCaseMock.Protected()
            .Setup<bool>(ActionMethodsName.OnActionBeforeStart, ItExpr.IsAny<NoValue>(), ItExpr.IsAny<UserNull>())
            .Returns(false);

        var useCase = useCaseMock.Object;

        // action
        _ = await useCase.HandleAsync();

        // assert
        useCaseMock.Verify(s => s.OnExecuteAsync(It.IsAny<NoValue>(), It.IsAny<CancellationToken>()), Times.Never());
        useCaseMock.Protected().Verify(ActionMethodsName.OnActionComplete, Times.Never(), ItExpr.IsAny<bool>(), ItExpr.IsAny<IReadOnlyCollection<Notification>>());
        useCaseMock.Protected().Verify(ActionMethodsName.OnActionException, Times.Never(), ItExpr.IsAny<Exception>(), ItExpr.IsAny<NoValue>(), ItExpr.IsAny<IUser>());
    }



    [Fact]
    public async Task MustStopExecutionAndInvokeOnActionCompleteWhenThereAreErrorNotificationsAfterOnActionBeforeStart()
    {
        // arrange
        var useCaseMock = new Mock<UseCase<NoValue, bool>>() { CallBase = true };
        useCaseMock.Setup(s => s.Notifications).Returns(new List<Notification>() { Notification.Error("error") });
        var useCase = useCaseMock.Object;

        // action
        var presenter = await useCase.HandleAsync();

        // assert
        useCaseMock.Protected().Verify(ActionMethodsName.OnActionBeforeStart, Times.Once(), ItExpr.IsAny<NoValue>(), ItExpr.IsAny<IUser>());
        useCaseMock.Protected().Verify(ActionMethodsName.OnActionComplete, Times.Once(), ItExpr.IsAny<bool>(), ItExpr.IsAny<IReadOnlyCollection<Notification>>());
        useCaseMock.Verify(s => s.OnExecuteAsync(It.IsAny<NoValue>(), It.IsAny<CancellationToken>()), Times.Never());
        useCaseMock.Protected().Verify(ActionMethodsName.OnActionException, Times.Never(), ItExpr.IsAny<Exception>(), ItExpr.IsAny<NoValue>(), ItExpr.IsAny<IUser>());
    }


    [Fact]
    public async Task ShouldInvokeAllMethodsToExceptionFlowWhenAnExceptionOccursInOnExecuteAsync()
    {
        // arrange
        var useCaseMock = new Mock<UseCase<NoValue, string>>() { CallBase = true };
        useCaseMock.Setup(s => s.OnExecuteAsync(It.IsAny<NoValue>(), It.IsAny<CancellationToken>())).Throws<Exception>();
        var useCase = useCaseMock.Object;

        // action
        var presenter = await useCase.HandleAsync();

        // assert
        useCaseMock.Protected().Verify(ActionMethodsName.OnActionBeforeStart, Times.Once(), ItExpr.IsAny<NoValue>(), ItExpr.IsAny<IUser>());
        useCaseMock.Protected().Verify(ActionMethodsName.OnActionException, Times.Once(), ItExpr.IsAny<Exception>(), ItExpr.IsAny<NoValue>(), ItExpr.IsAny<IUser>());
        useCaseMock.Protected().Verify(ActionMethodsName.OnActionComplete, Times.Once(), ItExpr.IsAny<bool>(), ItExpr.IsAny<IReadOnlyCollection<Notification>>());
    }

}
