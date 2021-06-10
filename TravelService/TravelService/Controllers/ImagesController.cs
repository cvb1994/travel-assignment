using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelService.Models;

namespace TravelService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        public IHostingEnvironment hostingEnvironment;
        private readonly travelContext _context;
        public ImagesController(IHostingEnvironment hostingEnv, travelContext db)
        {
            hostingEnvironment = hostingEnv;
            _context = db;
        }       
        public ActionResult<string> Images()
        {
            try
            {
                var files = HttpContext.Request.Form.Files;
                if (files != null && files.Count > 0)
                {
                    foreach (var file in files)
                    {
                        FileInfo fi = new FileInfo(file.FileName);
                        var newfilename = "Image_" + DateTime.Now.TimeOfDay.Milliseconds + fi.Extension;
                        var path = Path.Combine("", hostingEnvironment.ContentRootPath + "\\Images\\" + newfilename);
                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            file.CopyTo(stream);
                        }
                        Images images = new Images();
                        images.ImageLink = "\\Images\\" + newfilename;
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
        public ActionResult<List<Images>> GetImages()
        {
            var result = _context.Images.ToList();
            return result;
        }
    }
}
