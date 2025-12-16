using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TMB_TEST_WEBAPI.Interface;
using TMB_TEST_WEBAPI.Models;

namespace TMB_TEST_WEBAPI.Controllers
{
    [ApiController]
    [Route("[controller]/api")]
    public class PhoneController : Controller
    {
        private readonly IPhoneRepository _phoneRepository;
        public PhoneController(IPhoneRepository phoneRepository)
        {
            _phoneRepository = phoneRepository;
        }

        [HttpGet("phone-list")]
        public IActionResult GetProductList()
        {
           var data  =  _phoneRepository.GetPhoneList();
            var jdata = JsonConvert.SerializeObject(data);
            return Ok(jdata);
        }

        [HttpPost("checkout")]
        public IActionResult checkout([FromBody] List<ProductCheckOutRequest> requests)
        {
            var data = _phoneRepository.CheckoutProduct(requests);
            var jdata = JsonConvert.SerializeObject(data);
            return Ok(jdata);
        }
    }
}
