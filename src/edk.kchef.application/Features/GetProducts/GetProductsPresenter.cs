using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using edk.Kchef.Application.Fusc;
using edk.Kchef.Domain.Common.Base;
using Microsoft.AspNetCore.Mvc;

namespace edk.Kchef.Application.Features.GetProducts
{
    public class GetProductsPresenter : IPresenter<GetProductsRequest, GetProductsResponse>
    {
        public GetProductsResponse Response { get; private set; }

        public bool Success { get; private set; }

        public dynamic ViewResponse { get; private set; }

        public void OnError(GetProductsRequest input, List<Notification> notifications)
        {

        }

        public void OnException(GetProductsRequest input, Exception exception)
        {
            throw new NotImplementedException();
        }

        public void OnSuccess(GetProductsResponse output, List<Notification> notifications, CancellationToken cancellationToken)
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
}