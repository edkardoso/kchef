using edk.Kchef.Application.Features.OrderCardCreate;

namespace edk.Kchef.ApplicationTests;

public class OrderCardCreateTest
{
    [Fact]
    public async Task MustCreateNewOrderCardForDeskAsync()
    {
        //arrange
        var request = new OrderCardCreateRequest() { InternalDeskCode = "1234" };
        var useCase = new OrderCardCreateUseCase();

        //action 
        var presenter = await useCase.HandleAsync(request);

        //assert
        Assert.NotEqual(Guid.Empty, presenter.Output.Match(o=> Guid.NewGuid(), ()=> Guid.Empty));
        Assert.Equal(request.InternalDeskCode, presenter.Output.Match((o)=>o.Desk.InternalCode, ()=> string.Empty)) ;
    }
}