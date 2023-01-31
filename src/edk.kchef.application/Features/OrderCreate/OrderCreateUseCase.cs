using System;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using System.Threading;
using System.Threading.Tasks;
using edk.Fusc.Contracts;
using edk.Fusc.Core;
using edk.Fusc.Core.Validators;
using edk.Kchef.Application.Features.OrderCardCreate;
using edk.Kchef.Domain.Ordes;
using edk.Kchef.Domain.Users;
using MediatR;

namespace edk.Kchef.Application.Features.OrderCreate;

public class OrderCreateUseCase : UseCase<OrderCreateRequest, OrderCard>
{
    private OrderCard _orderCard = OrderCard.InstanceNull;

    protected override string NameUseCase => "OrderCreateUseCase";

    protected override bool OnActionBeforeStart(OrderCreateRequest input, IUser user)
    {

        input.OrderCard.WhenEmpty(() => {

            var presenter = Mediator.HandleAsync<OrderCardCreateUseCase>(new OrderCardCreateRequest()
            {
                InternalDeskCode = input.DeskInternalCode
            }).Result;

            _orderCard = presenter.Output.GetValueOrDefault(OrderCard.InstanceNull);

            _orderCard.IsNull.WhenTrue(() => SetNotification(Notification.Error("Comanda não gerada.")));

        });
      
        return _orderCard.IsNull.Not();
    }


    public override Task<OrderCard> OnExecuteAsync(OrderCreateRequest input, CancellationToken cancellationToken)
    {

        // buscar a comanda no repositório
        var desk = new Desk(input.DeskInternalCode);
        var orderCard = _orderCard ?? new OrderCard(desk);

        var order = new Order(new Waiter());
        order.AddRange(input.Items);

        orderCard.AddOrder(order);

        //Emit(new CreateNewOrderEvent(order, this));

        return Task.FromResult(orderCard);
    }


}
