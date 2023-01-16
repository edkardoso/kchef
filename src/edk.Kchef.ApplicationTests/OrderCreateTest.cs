using edk.Fusc.Core.Mediator;
using edk.Kchef.Application.Features.OrderCardCreate;
using edk.Kchef.Application.Features.OrderCreate;
using edk.Kchef.Domain.Ordes;
using Moq;

namespace edk.Kchef.ApplicationTests
{
    public class OrderCreateTest
    {
        [Fact]
        public async Task MustCreateNewOrderAndBeforeAnOrderCardAsync()
        {
            // arrange
            var produto1 = new ItemMenu("P1", "Produto 1", 10);
            var produto2 = new ItemMenu("P2", "Produto 2", 10);
            var request = new OrderCreateRequest()
            {
                DeskInternalCode = "Mesa12",
                Items = {
                    new(produto1),
                    new(produto2)
                }
            };
            var factoryMock = new Mock<FactoryMediator>();
            factoryMock.Setup(s => s.Get<OrderCardCreateUseCase>()).Returns(new OrderCardCreateUseCase());
            var mediatorFake = new UseCaseMediator(factory: factoryMock.Object);
            var useCase = new OrderCreateUseCase();
            useCase.SetMediator(mediatorFake);

            // action
            var presenter = await useCase.HandleAsync(request);
            var orderCard = presenter.Output.GetValueOrDefault(new OrderCard(new Desk(string.Empty)));

            // assert
            Assert.Equal(request.DeskInternalCode, orderCard.Desk.InternalCode);
            Assert.Equal(1, orderCard.Orders.Count);
            Assert.NotEqual(Guid.Empty, orderCard.Orders.FirstOrDefault()?.Id);
            Assert.NotEqual(Guid.Empty, orderCard.Orders.FirstOrDefault()?.Id);
            Assert.Equal(2, orderCard.Orders.FirstOrDefault()?.Items.Count);
            Assert.Equal(produto1, orderCard.Orders.FirstOrDefault()?.Items.FirstOrDefault()?.Item);
            Assert.Equal(produto2, orderCard.Orders.LastOrDefault()?.Items.LastOrDefault()?.Item);
        }
    }
}