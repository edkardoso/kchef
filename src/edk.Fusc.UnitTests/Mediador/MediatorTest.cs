using edk.Fusc.Core.Mediator;
using edk.Fusc.Core;
using edk.Fusc.Core.Events;

namespace edk.Fusc.UnitTests.Mediador
{
    public  class MediatorTest
    {

        [Fact]
        public async Task ShouldSubscribePublishUseCase()
        {
            // arrange
            var mediator = new UseCaseMediator();
            var useCaseSender = new UseCase1(mediator);
            var useCaseObserver = new UseCase2(mediator);
            var eventStart = new UseCaseStartEvent(useCaseSender);

            // action
            mediator.Subscribe<UseCaseStartEvent, UseCase1>(useCaseObserver);

            // assert
            mediator.Publish(eventStart);

        }

        [Fact]
        public async Task ShouldSubscribePublishUseCase2()
        {
            // arrange
            var mediator = new UseCaseMediator();
            var useCase1 = new UseCase1(mediator);
            var useCase2 = new UseCase2(mediator);
            var eventStart = new UseCaseStartEvent(useCase1);

            // action
            useCase1.HandleAsync(string.Empty);

            // assert
            mediator.Publish(eventStart);

        }

        private class UseCase1 : UseCase<string, string>
        {
            public UseCase1(IMediatorUseCase mediator)
               : base(mediator)
            { }

            protected override string NameUseCase => throw new NotImplementedException();

            public override Task<string> OnExecuteAsync(string input, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }

        private class UseCase2 : UseCase<string, string>
        {
            protected override string NameUseCase => throw new NotImplementedException();

            public UseCase2(IMediatorUseCase mediator)
                : base(mediator)
            {}


            public override Task<string> OnExecuteAsync(string input, CancellationToken cancellationToken)
            {
                return Task.FromResult(input);
            }


            public override Task OnEventAsync<TEvent>(TEvent useCaseEvent)
            {
                return base.OnEventAsync(useCaseEvent);
            }





        }


    }
}
