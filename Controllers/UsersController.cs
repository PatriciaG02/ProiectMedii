using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProiectMedii.Data;
using ProiectMedii.Models;



namespace ProiectMedii.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ProiectMediiContext _context;

        public UsersController(ProiectMediiContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUser()
        {
            return await _context.User.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.User.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _context.User.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.User.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.User.Any(e => e.Id == id);
        }

        // POST: api/Users/login
        [HttpPost("login")]
        public IActionResult Login([FromBody] ProiectMedii.Models.LoginRequest loginRequest)
        {
            var user = _context.User.FirstOrDefault(u => u.Username == loginRequest.Username && u.Password == loginRequest.Password);
            if (user == null)
            {
                return Unauthorized(); // Returnează 401 dacă utilizatorul nu este găsit
            }
            return Ok(user); // Returnează utilizatorul dacă login-ul este corect
        }

        // GET: api/Users?username={username}
        [HttpGet("by-username")]
        public IActionResult GetUserByUsername(string username)
        {
            var user = _context.User.FirstOrDefault(u => u.Username == username);
            if (user == null)
            {
                return NotFound(); // Returnează 404 dacă utilizatorul nu este găsit
            }
            return Ok(user); // Returnează utilizatorul dacă există
        }


        // POST: api/Users/register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            // Verifică dacă utilizatorul există deja
            var existingUser = await _context.User.FirstOrDefaultAsync(u => u.Username == user.Username);
            if (existingUser != null)
            {
                return BadRequest("Username already exists.");
            }

            // Adaugă utilizatorul în baza de date
            _context.User.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }
    }

        

}
