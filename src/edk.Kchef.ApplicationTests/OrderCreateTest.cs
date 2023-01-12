using edk.Kchef.Application.Features.OrderCardCreate;
using edk.Kchef.Application.Features.OrderCreate;
using edk.Kchef.Application.Fusc.Mediator;
using edk.Kchef.Domain.Ordes;
using Moq;

namespace edk.Kchef.ApplicationTests
{
    public class OrderCreateTest
    {
        [Fact]
        public void MustCreateNewOrderAndBeforeAnOrderCard()
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
            _ = useCase.HandleAsync(request);

            // assert
            Assert.Equal(request.DeskInternalCode, useCase.Presenter.Response.Desk.InternalCode);
            Assert.Equal(1, useCase.Presenter.Response.Orders.Count);
            Assert.NotEqual(Guid.Empty, useCase.Presenter.Response.Orders.FirstOrDefault()?.Id);
            Assert.NotEqual(Guid.Empty, useCase.Presenter.Response.Orders.FirstOrDefault()?.Id);
            Assert.Equal(2, useCase.Presenter.Response.Orders.FirstOrDefault()?.Items.Count);
            Assert.Equal(produto1, useCase.Presenter.Response.Orders.FirstOrDefault()?.Items.FirstOrDefault()?.Item);
            Assert.Equal(produto2, useCase.Presenter.Response.Orders.LastOrDefault()?.Items.LastOrDefault()?.Item);
        }
    }
}