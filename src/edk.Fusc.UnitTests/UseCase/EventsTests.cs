using edk.Fusc.Contracts;
using edk.Fusc.Contracts.Common;
using edk.Fusc.Core;
using edk.Fusc.Core.Events;
using edk.Fusc.Core.Mediator;
using FluentAssertions;
using Moq;

namespace edk.Fusc.UnitTests.UseCase;

public class EventsTests
{
    private readonly Mock<EventRecipientUseCase> rcpUseCaseMock;
    private readonly UseCaseMediator mediator;
    private readonly EventCreatorUseCase evtUsecase;
    #region Class for Tests
    public class EventCreatorUseCase : UseCaseInput<string>
    {
        public EventCreatorUseCase()
        {
            
        }

        public EventCreatorUseCase(IMediatorUseCase mediator):base(mediator)
        { }

        protected override string NameUseCase => "EventCreatorUseCase";

        

        public override Task<NoValue> OnExecuteAsync(string input, CancellationToken cancellationToken)
        {
            if (input == "error")
            {
                Console.WriteLine("erro lançado");
                throw new ArgumentException("error");
            }

            return Task.FromResult(NoValue.Instance);
        }

       
    }

    public class EventRecipientUseCase : UseCaseOutput<string>
    {
        protected override string NameUseCase => "EventRecipientUseCase";

       
        public override Task<string> OnExecuteAsync(NoValue input, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public override Task OnEventAsync<TEvent>(TEvent useCaseEvent)
        {
            return Task.CompletedTask;
        }
    }

    #endregion

    public EventsTests()
    {
        // arrange
        rcpUseCaseMock = new Mock<EventRecipientUseCase>();
        var factoryMock = new Mock<FactoryMediator>();
        factoryMock.Setup(s => s.Get(typeof(EventRecipientUseCase))).Returns(rcpUseCaseMock.Object);

        mediator = new UseCaseMediator(factoryMock.Object);
        evtUsecase = new EventCreatorUseCase(mediator);
    }

    [Fact]
    public void Subscription_ShouldAddEventStartToEventRecipientUseCase()
    {
        // act
        evtUsecase.Subscription<EventRecipientUseCase, UseCaseStartEvent>();

        //assert
        mediator.PubSub.Subscriptions.Should().HaveCount(1);

    }

    [Fact]
    public async Task OnEventAsync_ShouldBeCalled_WhenUseCaseSubscriptionForEventStart()
    {
       // arrange
        evtUsecase.Subscription<EventRecipientUseCase, UseCaseStartEvent>();

        // act
        await evtUsecase.HandleAsync();

        //assert
        rcpUseCaseMock.Verify(s => s.OnEventAsync(It.Is<IUseCaseEvent>(e =>
                   e.Category.Equals(UseCaseEventCategory.Start))), Times.Once());

    }

    [Fact]
    public async Task OnEventAsync_ShouldBeCalled_WhenUseCaseSubscriptionForEventSuccess()
    {
        // arrange
        evtUsecase.Subscription<EventRecipientUseCase, UseCaseSuccessEvent>();

        // act
        await evtUsecase.HandleAsync();

        //assert
        rcpUseCaseMock.Verify(s => s.OnEventAsync(It.Is<IUseCaseEvent>(e =>
                   e.Category.Equals(UseCaseEventCategory.Success))), Times.Once());

    }

    [Fact]
    public async Task OnEventAsync_ShouldBeCalled_WhenUseCaseSubscriptionForEventFailure()
    {
        // arrange
        evtUsecase.Subscription<EventRecipientUseCase, UseCaseFailureEvent>();

        // act
        await evtUsecase.HandleAsync("error");

        //assert
        rcpUseCaseMock.Verify(s => s.OnEventAsync(It.Is<IUseCaseEvent>(e =>
                    e.Category.Equals(UseCaseEventCategory.Failure))), Times.Once());

    }
}
