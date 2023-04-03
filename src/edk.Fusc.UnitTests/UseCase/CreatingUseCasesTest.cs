using edk.Fusc.Contracts;
using edk.Fusc.Core;
using edk.Fusc.Core.Validators;
using edk.Fusc.UnitTests.Helper.Passwords;
using edk.Tools.NoIf.Comparer;
using Microsoft.VisualStudio.TestPlatform.Utilities;

namespace edk.Fusc.UnitTests.UseCase;

public class CreatingUseCasesTest
{


    // UseCase com Input e Output definidos

    [Fact]
    public async Task ShouldReturnAnPasswordWithTenCharacteres()
    {
        // arrange
        var passwordSize = 10;
        var useCase = new GeneratorPassoword();

        // action
        var presenter = await useCase.HandleAsync(passwordSize);
        // assert
        Assert.Equal(passwordSize, presenter.Output.GetValueOrDefault(string.Empty).Length);
    }

    // UseCase tipado para não ter entrada (input)
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

    // Usando um UseCase tipado para não ter retorno (output)
    [Fact]
    public async Task ShouldReturnNoValueAndCreatePasswordOfTenCharacteres()
    {
        // arrange
        var useCase = new GeneratorPasswordVoid();
        var passwordSize = 10;


        // action
        var presenter = await useCase.HandleAsync(passwordSize);

        // assert
        Assert.IsType<NoValue>(presenter.Output.GetValueOrDefault(NoValue.Instance));
        Assert.Equal(passwordSize, useCase.Value?.Length);

    }

    // Caso de Uso sem Input e sem retorno
    [Fact]
    public async Task ShouldReturnNoValue_Version()
    {
        // arrange
        var useCase = new GeneratorPasswordVoidOtherVersion();
        var initialValue = useCase.Value;

        // action
        _ = await useCase.HandleAsync();

        // assert
        Assert.NotEqual(initialValue, useCase.Value);

    }

}