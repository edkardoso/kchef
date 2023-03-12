using System.Collections.Generic;
using System.Linq;
using System.Threading;
using edk.Fusc.Contracts;
using edk.Fusc.Core.Presenters;
using Microsoft.AspNetCore.Mvc;

namespace edk.Kchef.Application.Features.GetProducts;

public class GetProductsPresenter : PresenterBase<GetProductsRequest, GetProductsResponse>
{
    public override void OnResult(GetProductsResponse output, IReadOnlyCollection<INotification> notifications, CancellationToken cancellationToken)
    {

        if (output == null || !output.Products.Any())
        {
            SetViewOutput(new NotFoundResult());
        }
        else
        {

            SetViewOutput(new OkObjectResult(output));
        }
    }
}