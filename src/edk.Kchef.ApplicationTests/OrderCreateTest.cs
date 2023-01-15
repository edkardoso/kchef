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
            var orders = presenter.Output.GetValue().Orders;

            // assert
            Assert.Equal(request.DeskInternalCode, presenter.Output.GetValue().Desk.InternalCode);
            Assert.Equal(1, orders.Count);
            Assert.NotEqual(Guid.Empty, orders.FirstOrDefault()?.Id);
            Assert.NotEqual(Guid.Empty, orders.FirstOrDefault()?.Id);
            Assert.Equal(2, orders.FirstOrDefault()?.Items.Count);
            Assert.Equal(produto1, orders.FirstOrDefault()?.Items.FirstOrDefault()?.Item);
            Assert.Equal(produto2, orders.LastOrDefault()?.Items.LastOrDefault()?.Item);
        }
    }
}