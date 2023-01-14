using edk.Fusc.Core;
using edk.Fusc.UnitTests.Help.Scenario01;

namespace edk.Fusc.UnitTests
{
    public class Scenario01
    {
        
        [Fact]
        public async Task OnExecuteAsync_ShouldReturnSquareOfNumber()
        {
            // arrange
            var useCase = new SimpleUseCase();

            // action
            var presenter = await useCase.HandleAsync(10);

            // assert
            Assert.Equal(100, presenter.Output);
        }

        // Caso de uso sem Input
        [Fact]
        public async Task OnExecuteAsync_ShouldReturnZero()
        {
            // arrange
            var useCase = new WithOutInputUseCase();

            // action
            var presenter = await useCase.HandleAsync(NoValue.Create);

            // assert
            Assert.Equal(0, presenter.Output);
        }

        // Caso de uso sem retorno
        [Fact]
        public async Task OnExecuteAsync_ShouldReturnNoValue()
        {
            // arrange
            var useCase = new WithOutReturnUseCase();

            // action
            var presenter = await useCase.HandleAsync(0);

            // assert
            Assert.Equal(NoValue.Create, presenter.Output);
        }

        // Caso de Uso sem Input e sem retorno
        [Fact]
        public async Task OnExecuteAsync_ShouldReturnNoValue_Version()
        {
            // arrange
            var useCase = new WithOutUseCase();

            // action
            var presenter = await useCase.HandleAsync(NoValue.Create);

            // assert
            Assert.Equal(NoValue.Create, presenter.Output);
        }
    }
}