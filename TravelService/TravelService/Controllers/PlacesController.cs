using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelService.Models;

namespace TravelService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlacesController : ControllerBase
    {
        private readonly travelContext _context;

        public PlacesController(travelContext context)
        {
            _context = context;
        }

        // GET: api/Places
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Places>>> GetPlaces()
        {
            return await _context.Places.ToListAsync();
        }

        // GET: api/Places/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Places>> GetPlaces(int id)
        {
            var places = await _context.Places.FindAsync(id);

            if (places == null)
            {
                return NotFound();
            }

            return places;
        }

        // PUT: api/Places/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlaces(int id, Places places)
        {
            if (id != places.PlaceId)
            {
                return BadRequest();
            }

            _context.Entry(places).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlacesExists(id))
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

        // POST: api/Places
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Places>> PostPlaces(Places places)
        {
            Places tempPlace = await _context.Places.Where(u => u.PlaceName ==places.PlaceName).FirstOrDefaultAsync();
            if (tempPlace != null) { return Ok("failed"); }

            Users users = _context.Users.Find(places.UserId);
            places.User = users;

            _context.Places.Add(places);
            await _context.SaveChangesAsync();
            
            return Ok("success");
            
        }

        // DELETE: api/Places/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Places>> DeletePlaces(int id)
        {
            var places = await _context.Places.FindAsync(id);
            if (places == null)
            {
                return NotFound();
            }

            _context.Places.Remove(places);
            await _context.SaveChangesAsync();

            return places;
        }

        private bool PlacesExists(int id)
        {
            return _context.Places.Any(e => e.PlaceId == id);
        }
    }
}
