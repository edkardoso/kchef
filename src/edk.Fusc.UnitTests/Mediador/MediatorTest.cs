using edk.Fusc.Core.Mediator;
using edk.Fusc.Core;
using edk.Fusc.Contracts;
using Moq;

namespace edk.Fusc.UnitTests.Mediador
{

    public class MediatorTest
    {
        [Fact]
        public void ShouldReturnAnInstanceOfMyUseCase()
        {
            // arrange
            var factoryMediator = new Mock<IFactoryMediator>();
            var mediator = new UseCaseMediator(factoryMediator.Object);
            factoryMediator.Setup(s => s.Get<MyUseCase>())
                            .Returns(new MyUseCase(mediator));
            // act
            var useCase = mediator.Factory.Get<MyUseCase>();
            
            // assert
            Assert.NotNull(useCase);

        }

        private class MyUseCase : UseCase<NoValue, NoValue>
        {
            public MyUseCase(IMediatorUseCase mediator)
               : base(mediator)
            { }

            protected override string NameUseCase => throw new NotImplementedException();

            public override Task<NoValue> OnExecuteAsync(NoValue input, CancellationToken cancellationToken)
            {
                return Task.FromResult(default(NoValue));
            }
        }


    }
}
