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
    [Route("api/comment")]
    [ApiController]
    [Authorize]
    public class CommentsController : ControllerBase
    {
        private readonly travelContext _context;

        public CommentsController(travelContext context)
        {
            _context = context;
        }

        // GET: api/Comments
        //[HttpGet]
        //[Route("place/{placeId}")]
        //[AllowAnonymous]
        //public async Task<ActionResult<IEnumerable<Comment>>> GetPlaceComment(int placeId)
        //{
        //    var list = await _context.Comment.Where(c => c.PlaceId == placeId).ToListAsync();
        //    return list;

        //}

        [HttpGet]
        [Route("place/{placeId}")]
        [AllowAnonymous]
        public List<CommentDTO> GetPlaceComment(int placeId)
        {
            var list = _context.Comment.Where(c => c.PlaceId == placeId).ToList();
            List<CommentDTO> tempList = new List<CommentDTO>();

            foreach (Comment obj in list)
            {
                Users user = _context.Users.Find(obj.UserId);
                tempList.Add(new CommentDTO(obj.CommentId, user.UserName, obj.Content));
            }

            return tempList;

        }


        // GET: api/Comments/5
        [HttpGet]
        [Route("detail/{id}")]
        public async Task<ActionResult<Comment>> GetComment(int id)
        {
            var comment = await _context.Comment.FindAsync(id);

            if (comment == null)
            {
                return NotFound();
            }

            return comment;
        }

        // PUT: api/Comments/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComment(int id, Comment comment)
        {
            if (id != comment.CommentId)
            {
                return BadRequest();
            }

            _context.Entry(comment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommentExists(id))
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

        // POST: api/Comments
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult> PostComment(Comment comment)
        {
            Places place = _context.Places.Find(comment.PlaceId);
            comment.Place = place;
            Users user = _context.Users.Find(comment.UserId);
            comment.User = user;

            _context.Comment.Add(comment);
            await _context.SaveChangesAsync();

            return Ok("success");
        }

        // DELETE: api/Comments/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Comment>> DeleteComment(int id)
        {
            var comment = await _context.Comment.FindAsync(id);
            if (comment == null)
            {
                return NotFound();
            }

            _context.Comment.Remove(comment);
            await _context.SaveChangesAsync();

            return comment;
        }

        private bool CommentExists(int id)
        {
            return _context.Comment.Any(e => e.CommentId == id);
        }
    }
}
