using System;
using edk.Kchef.Application.Features.OrderCardCreate;
using edk.Kchef.Domain.Common;
using edk.Kchef.Domain.Ordes;
using edk.Kchef.Domain.Users;
using FluentValidation;

namespace edk.Kchef.Application.Features.OrderCreate
{
    public class OrderCreateUseCase : UseCase<OrderCreateRequest, OrderCard>
    {
        private readonly OrderCardCreateUseCase _orderCardCreateUseCase;

        public OrderCreateUseCase(OrderCardCreateUseCase orderCardCreateUseCase
            , IPresenter<OrderCreateRequest, OrderCard> presenter = null
            , AbstractValidator<OrderCreateRequest> validator = null)
            : base(presenter, validator)
        {
            _orderCardCreateUseCase = orderCardCreateUseCase;
        }

        public override OrderCard OnExecute(OrderCreateRequest input)
        {
            OrderCard orderCard;

            if (input.OrderCard == Guid.Empty)
            {
                var presenter = _orderCardCreateUseCase.Execute(new OrderCardCreateRequest()
                {
                    InternalCodeDesk = input.DeskInternalCode
                });

                orderCard = presenter.Result;
            }
            else
            {
                // buscar a comanda no repositório
                var desk = new Desk(input.DeskInternalCode);
                orderCard = new OrderCard(desk);
            }

            var order = new Order(new Waiter());
            order.AddRange(input.Items);

            orderCard.AddOrder(order);

            return orderCard;
        }
    }
}
