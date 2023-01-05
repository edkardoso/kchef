using edk.Kchef.Domain.Common;
using edk.Kchef.Domain.Ordes;
using FluentValidation;

namespace edk.Kchef.Application.Features.OrderCardCreate
{
    internal class OrderCardCreateUseCase : UseCase<OrderCardCreateRequest, OrderCard>
    {
        public OrderCardCreateUseCase(IPresenter<OrderCardCreateRequest, OrderCard> presenter, AbstractValidator<OrderCardCreateRequest> validator = null) : base(presenter, validator)
        {
        }

        public override OrderCard OnExecute(OrderCardCreateRequest input)
        {
            var desk = new Desk(input.InternalCodeDesk);

            return new OrderCard(desk);
        }
    }
}
