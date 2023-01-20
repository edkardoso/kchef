using edk.Fusc.Core.Mediator;
using edk.Fusc.Core;
using edk.Fusc.UnitTests.Helper;
using Moq.Protected;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using edk.Fusc.Core.Events;

namespace edk.Fusc.UnitTests.Mediador
{
    public  class MediatorTest
    {

        [Fact]
        public async Task ShouldSubscribeUseCase()
        {
            // arrange
            var mediator = new UseCaseMediator();
            var useCaseSender = new UseCaseSender(mediator);
            var useCaseObserver = new UseCaseObserver(mediator);
            var eventStart = new UseCaseStartEvent(useCaseSender);

            // action
            mediator.Subscribe<UseCaseStartEvent, UseCaseSender>(useCaseObserver);

            // assert
            mediator.Publish(eventStart);

        }




        private class UseCaseSender : UseCase<string, string>
        {
            public UseCaseSender(IMediatorUseCase mediator)
               : base(mediator)
            { }

            protected override string NameUseCase => throw new NotImplementedException();

            public override Task<string> OnExecuteAsync(string input, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }

        private class UseCaseObserver : UseCase<string, string>
        {
            protected override string NameUseCase => throw new NotImplementedException();

            public UseCaseObserver(IMediatorUseCase mediator)
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
