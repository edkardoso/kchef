using System;
using System.Threading;
using System.Threading.Tasks;
using edk.Fusc.Core;
using edk.Kchef.Application.Features.OrderCardCreate;
using edk.Kchef.Domain.Ordes;
using edk.Kchef.Domain.Users;

namespace edk.Kchef.Application.Features.OrderCreate;

public class OrderCreateUseCase : UseCase<OrderCreateRequest, OrderCard>
{
    protected override string NameUseCase => "OrderCreateUseCase";

    public override async Task<OrderCard> OnExecuteAsync(OrderCreateRequest request, CancellationToken cancellationToken)
    {
        OrderCard orderCard = null;

        if (request.NoCard())
        {
            var presenter = await Mediator.HandleAsync<OrderCardCreateUseCase>(new OrderCardCreateRequest()
            {
                InternalDeskCode = request.DeskInternalCode
            });


            presenter.Output.Match(
                (o) => orderCard = o,
                () => throw new Exception("Comanda não gerada.")
            );

        }
        else
        {
            // buscar a comanda no repositório
            var desk = new Desk(request.DeskInternalCode);
            orderCard = new OrderCard(desk);
        }

        var order = new Order(new Waiter());
        order.AddRange(request.Items);

        orderCard.AddOrder(order);

        Emit(new CreateNewOrderEvent(order, this));

        return orderCard;
    }


}
