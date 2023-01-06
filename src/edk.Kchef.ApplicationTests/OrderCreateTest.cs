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
            var presenter = useCase.Execute(request);

            // assert
            Assert.Equal(request.DeskInternalCode, presenter.Result.Desk.InternalCode);
            Assert.Equal(1, presenter.Result.Orders.Count);
            Assert.NotEqual(Guid.Empty, presenter.Result.Orders.FirstOrDefault()?.Id);
            Assert.NotEqual(Guid.Empty, presenter.Result.Orders.FirstOrDefault()?.Id);
            Assert.Equal(2, presenter.Result.Orders.FirstOrDefault()?.Items.Count);
            Assert.Equal(produto1, presenter.Result.Orders.FirstOrDefault()?.Items.FirstOrDefault()?.Item);
            Assert.Equal(produto2, presenter.Result.Orders.LastOrDefault()?.Items.LastOrDefault()?.Item);
        }
    }
}