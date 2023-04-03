using edk.Fusc.Contracts;
using edk.Fusc.Core;
using edk.Fusc.Core.Validators;
using FluentAssertions;

namespace edk.Fusc.UnitTests.UseCase;

public class UseCaseOnlyOutputTests
{
    private class BooleanOutputTest : UseCaseOutput<bool>
    {
        public BooleanOutputTest()
        {
            MediatorBase = Mediator;
            ValidatorBase = Validator;
        }

        public IMediatorUseCase MediatorBase { get; }

        public IUseCaseValidator ValidatorBase { get; }

        protected override string NameUseCase => throw new NotImplementedException();

        public override Task<bool> OnExecuteAsync(NoValue input, CancellationToken cancellationToken)
            => Task.FromResult(true);

    }
    private readonly BooleanOutputTest useCase;

    /// <summary>
    /// Testing for Use Cases with only output
    /// </summary>
    public UseCaseOnlyOutputTests()
    {
        useCase = new BooleanOutputTest();
    }


    [Fact]
    public void Presenter_MustContainPresenterNullInstance()
        => useCase.Presenter.Should().NotBeNull();

    [Fact]
    public void Validator_MustContainValidatorNullInstance()
        => useCase.ValidatorBase.Should().NotBeNull();


    [Fact]
    public void Mediator_MustContaiMediatorNullInstance()
        => useCase.MediatorBase.Should().NotBeNull();

    [Fact]
    public void HasMediator_ShouldReturnFalse_WhenYourInstanceIsEqualIsNullObject()
    {
        // Assert
        useCase.MediatorBase.IsNull().Should().BeTrue();
        useCase.HasMediator.Should().BeFalse();
    }

    [Fact]
    public void HasPresenter_ShouldReturnFalse_WhenYourInstanceIsEqualIsNullObject()
    {
        // Assert
        useCase.Presenter.IsNull().Should().BeTrue();
        useCase.HasPresenter.Should().BeFalse();

    }

    [Fact]
    public async Task HandleAsync_NotShouldThrowException_WhenInputIsNull()
    {
        // Arrange 
        NoValue? obj = null;

        // Act
        Func<Task> act = async () => await useCase.HandleAsync(obj);

        // Assert
        await act.Should().NotThrowAsync<ArgumentException>();

    }

    [Fact]
    public async Task HandleAsync_ShouldReturnTrue()
    {
        // Act 
        await useCase.HandleAsync();

        // Assert
        useCase.Presenter.Output.GetValueOrDefault(false).Should().BeTrue();

    }
}
