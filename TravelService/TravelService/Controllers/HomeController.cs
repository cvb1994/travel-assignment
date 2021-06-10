using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelService.Models;
using Microsoft.EntityFrameworkCore;

namespace TravelService.Controllers
{
    [Route("api/search")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly travelContext _context;

        public HomeController(travelContext context)
        {
            _context = context;
        }

        // GET: api/Ratings
        [HttpGet("{search}")]
        public async Task<ActionResult<IEnumerable<Places>>> GetPlaceSearch(string search)
        {
            return await _context.Places.Where(p => p.Title.Contains(search)).ToListAsync();
        }
    }
}
