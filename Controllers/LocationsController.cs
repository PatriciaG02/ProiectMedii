using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProiectMedii.Data;
using ProiectMedii.Models;

namespace ProiectMedii.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        private readonly ProiectMediiContext _context;

        public LocationsController(ProiectMediiContext context)
        {
            _context = context;
        }

        // GET: api/Locations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Location>>> GetLocations()
        {
            return await _context.Location.ToListAsync();
        }

        // GET: api/Locations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Location>> GetLocation(int id)
        {
            var location = await _context.Location.FindAsync(id);

            if (location == null)
            {
                return NotFound();
            }

            return location;
        }

        [HttpPost]
        public async Task<ActionResult<Location>> PostLocation(Location location)
        {
            if (location == null || string.IsNullOrWhiteSpace(location.Name))
            {
                return BadRequest("Invalid location data.");
            }

            _context.Location.Add(location);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetLocation), new { id = location.Id }, location);
        }

        // PUT: api/Locations/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLocation(int id, [FromBody] Location location)
        {
            if (id != location.Id)
            {
                return BadRequest();
            }

            _context.Entry(location).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LocationExists(id))
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

        // DELETE: api/Locations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLocation(int id)
        {
            var location = await _context.Location.FindAsync(id);
            if (location == null)
            {
                return NotFound();
            }

            _context.Location.Remove(location);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LocationExists(int id)
        {
            return _context.Location.Any(e => e.Id == id);
        }
    }
}
