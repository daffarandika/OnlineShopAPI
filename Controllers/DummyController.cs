using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineShopAPI.Utils;
using System.Security.Claims;
using System.Text;

namespace OnlineShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DummyController : ControllerBase
    {
        [HttpGet]
        public ActionResult<string> Get(string token)
        {
            string dateString = Helper.GetValueFromToken(token, ClaimTypes.Expired);
            return Ok(DateTime.Parse(dateString).ToString("yyyy"));
        }
    }
}
