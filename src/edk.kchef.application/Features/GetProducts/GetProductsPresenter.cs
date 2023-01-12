using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using edk.Kchef.Application.Fusc.Presenters;
using edk.Kchef.Domain.Common.Base;
using Microsoft.AspNetCore.Mvc;

namespace edk.Kchef.Application.Features.GetProducts;

public class GetProductsPresenter : PresenterBase<GetProductsRequest, GetProductsResponse>
{
    public override void OnSuccess(GetProductsResponse output, List<Notification> notifications, CancellationToken cancellationToken)
    {
        Success = true;
        Response = output;

        if (output == null || !output.Products.Any())
        {
            ViewResponse = new NotFoundResult();
        }
        else
        {

            ViewResponse = new OkObjectResult(output);
        }


    }
}