using System;
using System.Collections.Generic;
using System.IO;
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
        public ActionResult<string> PostPlaces()
        {
            string name = null;
            string title = null;
            string info = null;
            int userId = 0;
            

            var dict = HttpContext.Request.Form.ToDictionary(x => x.Key, x => x.Value.ToString());
            foreach (string key in HttpContext.Request.Form.Keys)
            {
                if (key.Equals("placeName")) { name = dict[key]; }
                if (key.Equals("title")) { title = dict[key]; }
                if (key.Equals("info")) { info = dict[key]; }
                if (key.Equals("userId")) { userId = int.Parse(dict[key]); }
            }
            Users users = _context.Users.Find(userId);
            Places place = new Places();

            var images = HttpContext.Request.Form.Files;
            foreach (var file in images)
            {
                Stream filestream = file.OpenReadStream();
                var base64 = ConvertToBase64(filestream);
                place.ImageLink = base64;
            }
            
            place.PlaceName = name;
            place.Title = title;
            place.Info = info;
            place.User = users;

            _context.Places.Add(place);
            _context.SaveChangesAsync();

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

        public string ConvertToBase64(Stream stream)
        {
            byte[] bytes;
            using (var memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                bytes = memoryStream.ToArray();
            }

            string base64 = Convert.ToBase64String(bytes);
            return base64;
        }
    }
}
