using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using BlackSquares.Managers;
using BlackSquares.Models;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using BlackSquares.Data;
using Microsoft.EntityFrameworkCore;

namespace BlackSquares.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _env;

        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment env, ApplicationDbContext context)
        {
            _logger = logger;
            _env = env;
            _context = context;
        }

        [Route("/")]
        public IActionResult Start()
        {
            return View();
        }

        [Route("/about")]
        public IActionResult About()
        {
            return View();
        }

        [Route("/create")]
        public IActionResult Create()
        {
            return View();
        }

        [Route("/create")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateMemeModel createMemeModel)
        {
            try
            {
                ViewData["WebRootPath"] = _env.WebRootPath;
                using (var memoryStream = new MemoryStream())
                {
                    await createMemeModel.Image.CopyToAsync(memoryStream);
                    if (memoryStream.Length > 3145728) // 3MB
                    {
                        ModelState.AddModelError("Image", "Размер изображения не может быть больше 3 мегабайт");
                    }
                }
                if (!(new string[] { "image/png", "image/jpeg" }).Contains(createMemeModel.Image.ContentType))
                {
                    ModelState.AddModelError("Image", $"Тип {createMemeModel.Image.ContentType} не поддерживается");
                }

                if (ModelState.IsValid)
                {
                    ImageFileManager fm = new (
                        createMemeModel.Image,
                        ImageFileManager.GenerateFileName(createMemeModel.Image.FileName),
                        Path.Combine(_env.WebRootPath, "images\\uploads"));
                    fm.SaveFile();
                    ViewData["ImageFullPath"] = Path.GetFileName(fm.ImageFullPath);
                    ViewData["IsFormOk"] = true;
                    return View(createMemeModel);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
            }
            
            return View(createMemeModel);
        }

        [Route("/list")]
        public async Task<IActionResult> List()
        {
            ViewData["memesList"] = await _context.Memes!.OrderByDescending(t => t.CreationDate).ToListAsync();
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}