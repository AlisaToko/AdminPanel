using AdminPanel.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace AdminPanel.Controllers
{

    public class NewsController : Controller
    {
        ApplicationContext _context;
        IWebHostEnvironment _environment;
        public NewsController(ApplicationContext context, IWebHostEnvironment env)
        {
            _context = context;
            _environment = env;
        }
      
        public async Task<IActionResult> Index()
        {
            var newsList = await _context.News.Include(p=>p.Pictures).ToListAsync();
            return View(newsList);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(string newsName, string newsDescription, IFormFileCollection uploadedFiles)
        {
            //создаем новость через экземпляр класса news
            var news = new News { Name = newsName, Description = newsDescription };
            await _context.News.AddAsync(news);
            _context.SaveChanges();

            if (uploadedFiles != null)
            {
                foreach (var uploadedFile in uploadedFiles ) {
                    //путь для хранени файла
                    var path = "/Pictures/" + uploadedFile.FileName;
                    using (var fileStream = new FileStream(_environment.WebRootPath + path, FileMode.Create))
                    {
                        //копируем изображения в папку wwrooot + path
                        await uploadedFile.CopyToAsync(fileStream);
                    }
                    Pictures pic = new Pictures() { Name = uploadedFile.FileName, Path = path, News = news };
                    await _context.Pictures.AddAsync(pic);
                    await _context.SaveChangesAsync();
                }
            }
            return RedirectToAction("Index");
        }
    }
}
