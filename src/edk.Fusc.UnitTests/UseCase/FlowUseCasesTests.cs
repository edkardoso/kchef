using edk.Fusc.UnitTests.Helper.Flows;

namespace edk.Fusc.UnitTests.UseCase;

public class FlowUseCasesTests
{

    [Fact]
    public async Task WhenSuccessFlow()
    {
        // arrange
        var useCase = new SuccessFlowUseCase();

        // action
        var presenter = await useCase.HandleAsync();

        // assert
        Assert.Equal(ActionMethodsName.OnActionBeforeStart, useCase.Methods[0]);
        Assert.Equal(ActionMethodsName.OnExecuteAsync, useCase.Methods[1]);
        Assert.Equal(ActionMethodsName.OnActionComplete, useCase.Methods[2]);
    }


    [Fact]
    public async Task WhenExceptionFlow()
    {
        // arrange
        var useCase = new ExceptionFlowUseCase();

        // action
        var presenter = await useCase.HandleAsync();

        // assert
        Assert.Equal(ActionMethodsName.OnActionBeforeStart, useCase.Methods[0]);
        Assert.Equal(ActionMethodsName.OnExecuteAsync, useCase.Methods[1]);
        Assert.Equal(ActionMethodsName.OnActionException, useCase.Methods[2]);
        Assert.Equal(ActionMethodsName.OnActionComplete, useCase.Methods[3]);


    }
}
