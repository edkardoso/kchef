using edk.Kchef.Application.Features.GetProducts;
using edk.Kchef.Application.Fusc;
using edk.Kchef.Domain.Common.Base;

namespace edk.Kchef.Api.Presenters
{
    public class PresenterGetProducts : IPresenter<ProductsRequest, IEnumerable<ProductsResponse>>
    {
        public IEnumerable<ProductsResponse> Response => throw new NotImplementedException();

        public bool Success => throw new NotImplementedException();

        public dynamic ViewResponse => throw new NotImplementedException();

        public void OnError(ProductsRequest input, List<Notification> notifications)
        {
            throw new NotImplementedException();
        }

        public void OnException(ProductsRequest input, Exception exception)
        {
            throw new NotImplementedException();
        }

        public void OnSuccess(IEnumerable<ProductsResponse> output, List<Notification> notifications, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
