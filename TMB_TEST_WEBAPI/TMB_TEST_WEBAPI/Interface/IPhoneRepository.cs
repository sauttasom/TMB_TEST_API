using System.Data;
using TMB_TEST_WEBAPI.Models;

namespace TMB_TEST_WEBAPI.Interface
{
    public interface IPhoneRepository
    {
        public List<PhoneListModels> GetPhoneList();
        public ProductCheckoutDTO CheckoutProduct(List<ProductCheckOutRequest> productCheckOutRequests);
    }
}
