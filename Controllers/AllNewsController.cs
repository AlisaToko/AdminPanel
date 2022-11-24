using AdminPanel.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.Controllers
{
    public class AllNewsController : Controller
    {
        ApplicationContext _context;
        IWebHostEnvironment _environment;
        public AllNewsController(ApplicationContext context, IWebHostEnvironment env)
        {
            _context = context;
            _environment = env;
        }

       public async Task<IActionResult> Index()
        {
            var allNewsList = await _context.AllNews.Include(p => p.Pictures1).ToListAsync();
            return View(allNewsList);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(string allNewsName, string allNewsDescription, string allNewsAction, IFormFileCollection uploadedFiles, int allNewsDate)
        {
            //создаем новость через экземпляр класса news
            var allNews = new AllNews { Name = allNewsName, Description = allNewsDescription, Date = allNewsDate };
            await _context.AllNews.AddAsync(allNews);
            _context.SaveChanges();

            if (uploadedFiles != null)
            {
                foreach (var uploadedFile in uploadedFiles)
                {
                    //путь для хранени файла
                    var path = "/AllPictures/" + uploadedFile.FileName;
                    using (var fileStream = new FileStream(_environment.WebRootPath + path, FileMode.Create))
                    {
                        //копируем изображения в папку wwrooot + path
                        await uploadedFile.CopyToAsync(fileStream);
                    }
                    Pictures1 allPictures = new Pictures1() { Name = uploadedFile.FileName, Path = path, AllNews = allNews };
                    await _context.Pictures1.AddAsync(allPictures);
                    await _context.SaveChangesAsync();
                }
            }
            return RedirectToAction("Index");

        }
    }
}
