using edk.Kchef.Application.Features.GetProducts;
using edk.Kchef.Domain.Common.Base;
using edk.Kchef.Domain.Common.Fusc;

namespace edk.Kchef.Api.Presenters
{
    public class PresenterGetProducts : IPresenter<ProductsRequest, IEnumerable<ProductsResponse>>
    {
        public IEnumerable<ProductsResponse> Result => throw new NotImplementedException();

        public bool Success => throw new NotImplementedException();

        public dynamic ViewResult => throw new NotImplementedException();

        public void OnError(ProductsRequest input, List<Notification> notifications)
        {
            throw new NotImplementedException();
        }

        public void OnException(ProductsRequest input, Exception exception)
        {
            throw new NotImplementedException();
        }

        public void OnSuccess(IEnumerable<ProductsResponse> output, List<Notification> notifications)
        {
            throw new NotImplementedException();
        }
    }
}
