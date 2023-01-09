using System;
using System.Collections.Generic;
using edk.Kchef.Domain.Ordes;
using MediatR;

namespace edk.Kchef.Application.Features.OrderCreate
{
    public class OrderCreateRequest: IRequest<OrderCard>
    {
        public Guid OrderCard { get; set; }
        public string DeskInternalCode { get; set; }
        public string WaiterInternalCode { get; set; }
        public List<ItemOrder> Items { get; set; } = new List<ItemOrder>();


        public bool NoCard()
            => OrderCard == Guid.Empty;
    }
}
