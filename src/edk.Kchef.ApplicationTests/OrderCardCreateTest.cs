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
            var presenter = useCase.Execute(request);

            //assert
            Assert.True(presenter.Success);
            Assert.NotEqual(Guid.Empty, presenter.Result.Id);
            Assert.Equal(presenter.Result.Desk.InternalCode, request.InternalDeskCode);
        }
    }
}