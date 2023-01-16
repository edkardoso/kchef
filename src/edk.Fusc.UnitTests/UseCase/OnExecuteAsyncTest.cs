using System.Drawing;
using edk.Fusc.Core;
using edk.Fusc.Core.Outputs;
using edk.Fusc.UnitTests.Help.Scenario01;

namespace edk.Fusc.UnitTests.UseCase;

public class OnExecuteAsyncTest
{
    // UseCase com Input e Output definidos

    [Fact]
    public async Task ShouldReturnAnPasswordWithTenCharacteres()
    {
        // arrange
        var passwordSize = 10;
        var useCase = new GeneratorPassword();

        // action
        var presenter = await useCase.HandleAsync(passwordSize);
        // assert
        Assert.Equal(passwordSize, presenter.Output.GetValueOrDefault(string.Empty).Length);
    }

    // UseCase tipado para n�o ter entrada (input)
    [Fact]
    public async Task ShouldReturnAnPasswordOfSixCharacteres()
    {
        // arrange
        var useCase = new GeneratorPasswordFix();

        // action
        var presenter = await useCase.HandleAsync();

        // assert
        Assert.Equal(6, presenter.Output.GetValueOrDefault(string.Empty).Length);
    }

    // Usando um UseCase tipado para n�o ter retorno (output)
    [Fact]
    public async Task ShouldReturnNoValueAndCreatePasswordOfTenCharacteres()
    {
        // arrange
        var useCase = new GeneratorPasswordVoid();
        var initialValue = useCase.Value;
        var passwordSize = 10;


        // action
        var presenter = await useCase.HandleAsync(passwordSize);

        // assert
        Assert.IsType<NoValue>(presenter.Output.GetValueOrDefault(NoValue.Create));
        Assert.Equal(passwordSize, useCase.Value?.Length);

    }

    // Caso de Uso sem Input e sem retorno
    [Fact]
    public async Task ShouldReturnNoValue_Version()
    {
        // arrange
        var useCase = new GeneratorPassowordVoidOtherVersion();
        var initialValue = useCase.Value;

        // action
        _ = await useCase.HandleAsync();

        // assert
        Assert.NotEqual(initialValue, useCase.Value);

    }

}