using System;
using System.Collections.Generic;
using edk.Kchef.Domain.Ordes;

namespace edk.Kchef.Application.Features.OrderCreate
{
    public class OrderCreateRequest
    {
        public Guid OrderCard { get; set; }
        public string DeskInternalCode { get; set; }
        public string WaiterInternalCode { get; set; }
        public List<ItemOrder> Items { get; set; } = new List<ItemOrder>();
    }
}
