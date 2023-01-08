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
            useCase.Execute(request);

            //assert
            Assert.True(useCase.Presenter.Success);
            Assert.NotEqual(Guid.Empty, useCase.Presenter.Result.Id);
            Assert.Equal(useCase.Presenter.Result.Desk.InternalCode, request.InternalDeskCode);
        }
    }
}