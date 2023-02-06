using System;
using System.Threading;
using System.Threading.Tasks;
using edk.Fusc.Contracts;
using edk.Fusc.Core;
using edk.Fusc.Core.Validators;
using edk.Kchef.Domain.Contracts.Repositories;
using edk.Kchef.Domain.Ordes;
using edk.Tools;

namespace edk.Kchef.Application.Features.OrderCardCreate;

public class OrderCardCreateUseCase : UseCase<OrderCardCreateRequest, OrderCard>
{
    private readonly IOrderCardRepository _orderCardRepository;
    private readonly IDeskRepository _deskRepository;
    private readonly IUnitOfWork _uoW;

    public OrderCardCreateUseCase(
        IOrderCardRepository orderCardRepository
        , IDeskRepository deskRepository
        , IUnitOfWork uoW) : base(null, null)
    {
        _orderCardRepository = orderCardRepository;
        _deskRepository = deskRepository;
        _uoW = uoW;
    }
    protected override string NameUseCase => "OrderCardCreateUseCase";

    public override async Task<OrderCard> OnExecuteAsync(OrderCardCreateRequest input, CancellationToken cancellationToken)
    {
        var desk = await _deskRepository.SingleByCodeAsync(input.InternalDeskCode);

        return desk.Match(
           some: obj => obj.Available.Eval(
               ifTrue: () =>
               {
                   var orderCard = new OrderCard(obj);
                   _orderCardRepository.UpdateAsync(orderCard);
                   _uoW.CommitAsync();
                   return orderCard;
               },
                ifFalse: () =>
                {
                    SetNotification(Notification.Error($"A mesa {input.InternalDeskCode} está ocupada."));
                    return new OrderCardNull();
                })
        , none: () =>
        {
            SetNotification(Notification.Error($"A Mesa {input.InternalDeskCode} não existe."));
            return new OrderCardNull();
        });
    }

    protected override bool OnActionException(Exception exception, OrderCardCreateRequest input, IUser user)
    {
        exception.WhenIsTypeEqual<ArgumentException>(() =>
        {

        });

        SetNotification(Notification.Error($"Existe um erro no cadastro de mesas. Há mais de uma mesa com o código {input.InternalDeskCode}."));

        return base.OnActionException(exception, input, user);
    }

}
