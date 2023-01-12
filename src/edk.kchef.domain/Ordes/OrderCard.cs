using System;
using System.Collections.Generic;
using System.Linq;
using edk.Kchef.Domain.Common.Base;

namespace edk.Kchef.Domain.Ordes
{
    public class OrderCard : EntityBase<OrderCard>
    {
        private readonly OrderSetting _orderSetting;
        private bool _chargeServiceTax;

        public OrderCard(Desk desk)
        {
            Desk = desk;
        }

        public virtual Desk Desk { get; protected set; }

        public ICollection<Order> Orders { get; protected set; } = new List<Order>();

        public OrderCardStatusType Status { get; protected set; }

        public decimal SubTotal => Orders.Where(o => o.Canceled == false).Sum(o => o.Items.Sum(i => i.Price * i.Amount));
        public decimal ServiceTax => _chargeServiceTax ? (SubTotal * _orderSetting.ServiceTaxPercent) / 100 : 0;
        public decimal OtherTaxes { get; private set; }
        public decimal Discount { get; private set; }
        public decimal Total => SubTotal + ServiceTax + OtherTaxes - Discount;
        public decimal TotalPaid { get; private set; }
        public decimal Balance => TotalPaid - Total;
        public decimal Tip { get; private set; }


        public OrderCard(OrderSetting orderSetting)
        {
            _orderSetting = orderSetting;
            _chargeServiceTax = true;
            OtherTaxes = _orderSetting.OtherTaxes;
        }
        public void Close(decimal discount, bool chargeServiceTax, bool chargeOtherTaxes)
        {
            if (Status != OrderCardStatusType.Open)
                throw new InvalidOperationException();


            _chargeServiceTax = chargeServiceTax;

            Discount = discount;

            OtherTaxes = OtherTaxes = chargeOtherTaxes ? OtherTaxes : 0;

            Status = OrderCardStatusType.Close;

        }

        public void Reopen()
        { 
            Discount = 0;
            OtherTaxes = _orderSetting.OtherTaxes;

            Status = OrderCardStatusType.Open;
        }

        public void Pay(decimal value)
        {
            TotalPaid += value;

            if (TotalPaid >= Total)
                Status = OrderCardStatusType.Pay;
        }

        public void AddOrder(Order order)
        {
           Orders.Add(order);
        }

        public void RemoveOrder(Order order)
        {
            
        }
    }
}
