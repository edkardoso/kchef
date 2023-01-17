using edk.Fusc.UnitTests.Helper.Flows;


namespace edk.Fusc.UnitTests.UseCase;

public class FlowUseCasesTests
{
    #region Action methods invoked by Flow
    [Fact]
    public async Task ShouldReturnThreeMethodsInListWhenSuccessFlow()
    {
        // arrange
        var useCase = new SuccessFlowUseCase();

        // action
        var presenter = await useCase.HandleAsync();

        // assert
        Assert.Equal(3, useCase.Methods.Count);
        Assert.Equal(ActionMethodsName.OnActionBeforeStart, useCase.Methods[0]);
        Assert.Equal(ActionMethodsName.OnExecuteAsync, useCase.Methods[1]);
        Assert.Equal(ActionMethodsName.OnActionComplete, useCase.Methods[2]);
    }

    [Fact]
    public async Task ShouldReturnFourMethodsInListWhenExceptionFlow()
    {
        // arrange
        var useCase = new ExceptionFlowUseCase();

        // action
        var presenter = await useCase.HandleAsync();

        // assert
        Assert.Equal(4, useCase.Methods.Count);
        Assert.Equal(ActionMethodsName.OnActionBeforeStart, useCase.Methods[0]);
        Assert.Equal(ActionMethodsName.OnExecuteAsync, useCase.Methods[1]);
        Assert.Equal(ActionMethodsName.OnActionException, useCase.Methods[2]);
        Assert.Equal(ActionMethodsName.OnActionComplete, useCase.Methods[3]);


    }

    [Fact]
    public async Task ShouldReturnTwoMethodsInListWhenErrorFlow()
    {
        // arrange
        var useCase = new ErrorFlowUseCase(new ErrorFlowValidator());

        // action
        var presenter = await useCase.HandleAsync(-1);

        // assert
        Assert.Equal(2, useCase.Methods.Count);
        Assert.Equal(ActionMethodsName.OnActionBeforeStart, useCase.Methods[0]);
        Assert.Equal(ActionMethodsName.OnActionComplete, useCase.Methods[1]);


    }
    #endregion


    // Se o retorno de OnActionBeforeStart for igual a falso não deve continuar o processo.
    // Nem mesmo o método OnComplete deve ser chamado
    [Fact]
    public async Task OnActionBeforeStart_MustInterruptExecutionWhenReturningFalseMethod()
    {
        // arrange
        var useCase = new BeforeStartUseCase();

        // action
        var presenter = await useCase.HandleAsync();

        // assert
        Assert.Equal(1, useCase.Notifications?.Count);
        Assert.Contains(ActionMethodsName.OnActionBeforeStart, useCase.Notifications?.Select(n=>n.Message));
     
    }

   
}
