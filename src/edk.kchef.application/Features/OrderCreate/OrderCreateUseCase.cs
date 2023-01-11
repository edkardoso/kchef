using System.Threading;
using System.Threading.Tasks;
using edk.Kchef.Application.Features.OrderCardCreate;
using edk.Kchef.Application.Fusc;
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

        protected override string NameUseCase => "OrderCreateUseCase";

        public override async Task<OrderCard> Handle(OrderCreateRequest request, CancellationToken cancellationToken)
        {
            OrderCard orderCard;

            if (request.NoCard())
            {
                var presenter = await _orderCardCreateUseCase.HandleAsync(new OrderCardCreateRequest()
                {
                    InternalDeskCode = request.DeskInternalCode
                });

                orderCard = presenter.Response;
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

            return orderCard;
        }

       
    }
}
