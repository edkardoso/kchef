using edk.Kchef.Application.Features.OrderCardCreate;

namespace edk.Kchef.ApplicationTests
{

    public class OrderCardCreateTest
    {
        [Fact]
        public void MustCreateNewOrderCardForDesk()
        {
            //arrange
            var request = new OrderCardCreateRequest() { InternalDeskCode= "1234" };
            var validator = new OrderCardCreateValidator();
            var useCase = new OrderCardCreateUseCase(null, validator);

            //action 
            useCase.HandleAsync(request);

            //assert
            Assert.True(useCase.Presenter.Success);
            Assert.NotEqual(Guid.Empty, useCase.Presenter.Response.Id);
            Assert.Equal(useCase.Presenter.Response.Desk.InternalCode, request.InternalDeskCode);
        }
    }
}