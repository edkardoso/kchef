using edk.Kchef.Domain.Ordes;
using MediatR;

namespace edk.Kchef.Application.Features.OrderCardCreate
{
    public class OrderCardCreateRequest:IRequest<OrderCard>
    {
        public string InternalDeskCode { get; set; }
    }

   
}
