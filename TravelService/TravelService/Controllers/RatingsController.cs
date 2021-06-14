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
    [Authorize(Roles = "Traveller")]
    public class RatingsController : ControllerBase
    {
        private readonly travelContext _context;

        public RatingsController(travelContext context)
        {
            _context = context;
        }

        // GET: api/Ratings
        [HttpGet]
        [Route("place/{placeId}/{userId}")]
        public async Task<ActionResult<Rating>> GetPlaceRating(int placeId, int userId)
        {
            return await _context.Rating.Where(r => r.PlaceId == placeId && r.UserId == userId).FirstOrDefaultAsync();
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
        public ActionResult<string> PutRating(int id)
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

            Rating editRate = _context.Rating.Find(id);
            if(editRate.UserId != userId) { return BadRequest(); }

            editRate.Rating1 = (short)rate;
            editRate.UserId = userId;
            editRate.PlaceId = placeId;

            _context.Entry(editRate).State = EntityState.Modified;
            _context.SaveChangesAsync();

            return Ok("Success");
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
