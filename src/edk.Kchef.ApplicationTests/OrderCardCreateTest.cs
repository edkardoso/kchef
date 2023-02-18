using edk.Kchef.Application.Features.OrderCardCreate;
using edk.Kchef.Domain.Contracts.Repositories;
using Moq;

namespace edk.Kchef.ApplicationTests;

public class OrderCardCreateTest
{
    private Mock<IUnitOfWork> _unitOfWorkMock;

    public OrderCardCreateTest()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
    }

    [Fact]
    public async Task MustCreateNewOrderCardForDeskAsync()
    {
        //arrange
        var request = new OrderCardCreateRequest() { InternalDeskCode = "1234" };

        var orderCardRepositoryMock = new Mock<IOrderCardRepository>();
        var deskRepository = new Mock<IDeskRepository>();


        var useCase = new OrderCardCreateUseCase(orderCardRepositoryMock.Object, deskRepository.Object, _unitOfWorkMock.Object);

        //action 
        var presenter = await useCase.HandleAsync(request);

        //assert
        Assert.NotEqual(Guid.Empty, presenter.Output.Match(o=> Guid.NewGuid(), ()=> Guid.Empty));
        Assert.Equal(request.InternalDeskCode, presenter.Output.Match((o)=>o.Desk.InternalCode, ()=> string.Empty)) ;
    }
}