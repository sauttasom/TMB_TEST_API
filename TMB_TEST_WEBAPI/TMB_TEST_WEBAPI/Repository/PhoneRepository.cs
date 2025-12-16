using Oracle.ManagedDataAccess.Client;
using System.Data;
using TMB_TEST_WEBAPI.Interface;
using TMB_TEST_WEBAPI.Models;

namespace TMB_TEST_WEBAPI.Repository
{
    public class PhoneRepository : IPhoneRepository
    {
        private readonly IConfiguration _configuration;
        private readonly IDBContext _dBContext;
        public PhoneRepository(IConfiguration configuration, IDBContext dBContext)
        {
            _configuration = configuration;
            _dBContext = dBContext;

        }
        public ProductCheckoutDTO CheckoutProduct(List<ProductCheckOutRequest> productCheckOutRequests)
        {
            if (productCheckOutRequests == null)
            {
                return new ProductCheckoutDTO() { message = "CheckoutProduct Fail", isSuccess = false };
            }

            decimal total = 0;

            foreach (var item in productCheckOutRequests)
            {
                string sql = @"
                    UPDATE Stock
                    SET remainingamount = remainingamount - :qty
                    WHERE product_id = :pid
                    AND remainingamount >= :qty
                    ";
                total = total + item.product_price;
                int row = _dBContext.ExecuteNonQuery(
                    sql,
                    new Dictionary<string, object>
                    {
                        { ":qty", item.AmontProduct },
                        { ":pid", item.product_id }
                    }
                );
                if (row == 0)
                {
                    return new ProductCheckoutDTO() {
                        isSuccess = false,
                        message = $"Stock not enough for product {item.product_id}"
                    };
                }
            }






            return new ProductCheckoutDTO()
            {
                isSuccess = true,
                message = $"{total}"
            };


        }

        public ProductCheckoutDTO CheckoutProduct()
        {
            throw new NotImplementedException();
        }

        public List<PhoneListModels> GetPhoneList()
        {
            string query = @"SELECT * FROM productDetail pd 
                                    JOIN  Stock st ON st.product_id = pd.product_id";

            var dataDt = _dBContext.DbQueryExe(query);

            if (dataDt == null)
                return new List<PhoneListModels>();

            if (dataDt.Rows.Count == 0)
                return new List<PhoneListModels>();

            var productlist = new List<PhoneListModels>();
            foreach (DataRow item in dataDt.Rows)
            {
                productlist.Add(new PhoneListModels()
                {
                    product_id = item["PRODUCT_ID"].ToString(),
                    product_name = item["PRODUCT_NAME"].ToString(),
                    product_price = decimal.Parse(item["PRODUCT_PRICE"].ToString()),
                    remaining_amount = int.Parse(item["REMAININGAMOUNT"].ToString()),
                    product_img = item["IMG"].ToString()
                });
            }


            return productlist;
        }
    }
}
