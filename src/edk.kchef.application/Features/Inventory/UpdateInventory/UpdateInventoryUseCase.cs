using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using edk.Kchef.Application.Features.OrderCreate;
using edk.Kchef.Domain.Common;
using FluentValidation;

namespace edk.Kchef.Application.Features.Inventory.UpdateInventory
{

    internal class UpdateInventoryUseCase : UseCase<UpdateInventoryRequest, UpdateInventoryResponse>
    {

        public UpdateInventoryUseCase(OrderCreateUseCase orderCreateUseCase
            , IPresenter<UpdateInventoryRequest, UpdateInventoryResponse> presenter
            , AbstractValidator<UpdateInventoryRequest> validator = null) : base(presenter, validator)
        {
            orderCreateUseCase.Subscribe(this);
            
        }

        public override UpdateInventoryResponse OnExecute(UpdateInventoryRequest input)
        {
            throw new NotImplementedException();
        }

        public override void Handler(IUseCase<UpdateInventoryRequest, UpdateInventoryResponse> other)
        {
            base.Handler(other);
        }



    }
}
