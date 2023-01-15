using edk.Kchef.Application.Features.OrderCardCreate;

namespace edk.Kchef.ApplicationTests
{

    public class OrderCardCreateTest
    {
        [Fact]
        public async Task MustCreateNewOrderCardForDeskAsync()
        {
            //arrange
            var request = new OrderCardCreateRequest() { InternalDeskCode= "1234" };
            var validator = new OrderCardCreateValidator();
            var useCase = new OrderCardCreateUseCase();

            //action 
            var presenter = await useCase.HandleAsync(request);

            //assert
            Assert.True(presenter.Success);
            Assert.NotEqual(Guid.Empty, presenter.Output.GetValue().Id);
            Assert.Equal(presenter.Output.GetValue().Desk.InternalCode, request.InternalDeskCode);
        }
    }
}