using System.Collections.Generic;
using System.Linq;
using System.Threading;
using edk.Fusc.Core.Presenters;
using Microsoft.AspNetCore.Mvc;

namespace edk.Kchef.Application.Features.GetProducts;

public class GetProductsPresenter : PresenterBase<GetProductsRequest, GetProductsResponse>
{
    public override void OnResult(GetProductsResponse output, IReadOnlyCollection<Fusc.Core.Validators.Notification> notifications, CancellationToken cancellationToken)
    {

        if (output == null || !output.Products.Any())
        {
            ViewOutput = new NotFoundResult();
        }
        else
        {

            ViewOutput = new OkObjectResult(output);
        }
    }
}