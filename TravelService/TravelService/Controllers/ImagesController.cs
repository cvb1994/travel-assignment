using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelService.Models;

namespace TravelService.Controllers
{
    [Route("api/images")]
    [ApiController]
    [Authorize]
    public class ImagesController : ControllerBase
    {
        public IHostingEnvironment hostingEnvironment;
        private readonly travelContext _context;
        public ImagesController(IHostingEnvironment hostingEnv, travelContext db)
        {
            hostingEnvironment = hostingEnv;
            _context = db;
        }
        
        [HttpPost]
        public ActionResult<string> Images()
        {
            string placeId = null;
            var dict = HttpContext.Request.Form.ToDictionary(x => x.Key, x => x.Value.ToString());
            foreach (string key in HttpContext.Request.Form.Keys)
            {
                if (key.Equals("placeId")) { placeId = dict[key]; }
            }


            try
            {
                var files = HttpContext.Request.Form.Files;
                if (files != null && files.Count > 0)
                {
                    foreach (var file in files)
                    {
                        Stream filestream = file.OpenReadStream();
                        var base64 = ConvertToBase64(filestream);

                        Images images = new Images();
                        images.ImageLink = base64.ToString();

                        //Places place = _context.Places.Find(int.Parse(placeId));
                        //images.Place = place;
                        images.PlaceId = int.Parse(placeId);

                        _context.Images.Add(images);
                        _context.SaveChanges();
                    }
                    return "Saved Successf";
                }
                else
                {
                    return "select files";
                }
            }
            catch (Exception e1)
            {

                return e1.Message;
            }
        }
        [HttpGet]
        [Route("place/{placeId}")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Images>>> GetImages(int placeId)
        {
            return await _context.Images.Where(i => i.PlaceId == placeId).ToListAsync();
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
