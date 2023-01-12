﻿using System.Threading;
using System.Threading.Tasks;
using edk.Kchef.Application.Fusc;
using edk.Kchef.Domain.Ordes;
using FluentValidation;

namespace edk.Kchef.Application.Features.OrderCardCreate
{
    public class OrderCardCreateUseCase : UseCase<OrderCardCreateRequest, OrderCard>
    {
        public OrderCardCreateUseCase() : base(null, null)
        {
        }
        protected override string NameUseCase => "OrderCardCreateUseCase";

        public override Task<OrderCard> ExecuteAsync(OrderCardCreateRequest request, CancellationToken cancellationToken)
        {
            var desk = new Desk(request.InternalDeskCode);

            return Task.FromResult(new OrderCard(desk));
        }

       
    }
}
