using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShopAPI.Attributes;
using OnlineShopAPI.Models;
using OnlineShopAPI.Utils;

namespace OnlineShopAPI.Controllers
{
    [JwtAuth(Role: "admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly DbOnlineShopContext _context;

        public UsersController(DbOnlineShopContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<dynamic>>> GetUsers()
        {
            return await _context.Users.Select(u => new
            {
                u.Username,
                u.Email,
                u.Role,
                u.Address,
                u.JoinDate
            }).ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, UserUpload newUser)
        {
            var user = _context.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            user.Username = newUser.Username;
            user.Email = newUser.Email;
            user.Role = newUser.Role;
            user.Address = newUser.Address;
            user.Password = Helper.Hash(newUser.Password);
            user.PhoneNumber = newUser.PhoneNumber;

            _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(UserUpload user)
        {
            _context.Users.Add(new User
            {
                Username = user.Username,
                Email = user.Email,
                Role = user.Role,
                Address = user.Address,
                JoinDate = DateTime.Now,
                Password= Helper.Hash(user.Password),
                PhoneNumber = user.PhoneNumber
            });
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", _context.Users.OrderBy(u => u.Username).Select(u => u.Username).Last(), user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
