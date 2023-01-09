using edk.Kchef.Application.Features.OrderCardCreate;
using edk.Kchef.Application.Features.OrderCreate;
using edk.Kchef.Domain.Ordes;

namespace edk.Kchef.ApplicationTests
{
    public class OrderCreateTest
    {
        [Fact]
        public void MustCreateNewOrderAndBeforeAnOrderCard()
        {
            // arrange
            var validatorOrderCard = new OrderCardCreateValidator();
            var useCaseOrderCard = new OrderCardCreateUseCase(null, validatorOrderCard);
            var validator = new OrderCreateValidator();
            var useCase = new OrderCreateUseCase(useCaseOrderCard, validator: validator);

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