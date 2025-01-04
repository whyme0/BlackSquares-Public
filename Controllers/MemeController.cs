using Microsoft.AspNetCore.Mvc;
using BlackSquares.Models;
using BlackSquares.Data;
using BlackSquares.Managers;

namespace BlackSquares.Controllers
{
    public class MemeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public MemeController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        //[Route("memes/{id?}")]
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        [Consumes("multipart/form-data")]
        [Route("memes/create")]
        [HttpPost]
        public async Task<ActionResult> Create(string imageName, string image)
        {
            string base64img = image.Substring(image.LastIndexOf(",") + 1);
            string imgName = imageName.Substring(imageName.LastIndexOf("/") + 1);
            string imgUploadPath = Path.Combine(_env.WebRootPath, "images\\db_uploads");
            
            Meme m = new Meme() { ImageUrl = Path.Combine("images\\db_uploads", imgName) };
            ImageFileManager fm = new ImageFileManager(
                base64img,
                imgName,
                imgUploadPath);

            fm.SaveBase64File();

            _context.Memes.Add(m);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
