using edk.Kchef.Domain.Common.Fusc;
using edk.Kchef.Domain.Ordes;
using FluentValidation;

namespace edk.Kchef.Application.Features.OrderCardCreate
{
    public class OrderCardCreateUseCase : UseCase<OrderCardCreateRequest, OrderCard>
    {
        public OrderCardCreateUseCase(IPresenter<OrderCardCreateRequest, OrderCard> presenter, AbstractValidator<OrderCardCreateRequest> validator) : base(presenter, validator)
        {
        }

        public override OrderCard OnExecute(OrderCardCreateRequest input)
        {
            var desk = new Desk(input.InternalCodeDesk);

            return new OrderCard(desk);
        }
    }
}
