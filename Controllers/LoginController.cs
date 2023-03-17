using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineShopAPI.Models;
using OnlineShopAPI.Utils;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;

namespace OnlineShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        DbOnlineShopContext _context;
        public LoginController(DbOnlineShopContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult<string> login(string username, string password)
        {
            if (_context.Users.Where(u => u.Username == username && u.Password == Helper.Hash(password)).Count() == 0)
            {
                return BadRequest("User not found");
            }
            var user = _context.Users.First(u => u.Username == username && u.Password == Helper.Hash(password));
            return Ok(Helper.GenerateJWT(user));
        }

    }
}
