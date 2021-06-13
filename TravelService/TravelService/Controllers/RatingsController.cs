using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelService.Models;

namespace TravelService.Controllers
{
    [Route("api/rating")]
    [ApiController]
    [Authorize]
    public class RatingsController : ControllerBase
    {
        private readonly travelContext _context;

        public RatingsController(travelContext context)
        {
            _context = context;
        }

        // GET: api/Ratings
        [HttpGet]
        [Route("place/{placeId}")]
        public async Task<ActionResult<IEnumerable<Rating>>> GetPlaceRating(int placeId)
        {
            return await _context.Rating.Where(r => r.PlaceId == placeId).ToListAsync();
        }

        // GET: api/Ratings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Rating>> GetRating(int id)
        {
            var rating = await _context.Rating.FindAsync(id);

            if (rating == null)
            {
                return NotFound();
            }

            return rating;
        }

        // PUT: api/Ratings/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutRating(int id, Rating rating)
        {
            if (id != rating.RatingId)
            {
                return BadRequest();
            }

            _context.Entry(rating).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RatingExists(id))
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

        // POST: api/Ratings
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [Authorize]
        public ActionResult<string> PostRating()
        {
            int rate = 0;
            int placeId = 0;
            int userId = 0;

            var dict = HttpContext.Request.Form.ToDictionary(x => x.Key, x => x.Value.ToString());
            foreach (string key in HttpContext.Request.Form.Keys)
            {
                if (key.Equals("rating1")) { rate = int.Parse(dict[key]); }
                if (key.Equals("placeId")) { placeId = int.Parse(dict[key]); }
                if (key.Equals("userId")) { userId = int.Parse(dict[key]); }
            }
            Places place = _context.Places.Find(placeId);
            Users user = _context.Users.Find(userId);

            Rating newRate = new Rating();
            newRate.Rating1 = (short)rate;
            newRate.Place = place;
            newRate.User = user;
            
            _context.Rating.Add(newRate);
            _context.SaveChangesAsync();

            return Ok("success");
        }

        // DELETE: api/Ratings/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Rating>> DeleteRating(int id)
        {
            var rating = await _context.Rating.FindAsync(id);
            if (rating == null)
            {
                return NotFound();
            }

            _context.Rating.Remove(rating);
            await _context.SaveChangesAsync();

            return rating;
        }

        private bool RatingExists(int id)
        {
            return _context.Rating.Any(e => e.RatingId == id);
        }
    }
}
