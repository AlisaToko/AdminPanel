using AdminPanel.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace AdminPanel.Controllers
{
    public class HomeController : Controller
    {
        ApplicationContext _context;

        public HomeController(ApplicationContext context)
        {
            _context = context;
        }

        [Authorize]
       public IActionResult Index()
        
        {
            return View();
        }

        public async Task<IActionResult> GetAllEmployees() 
            => View(await _context.Employees.ToListAsync());


        
        [HttpPost]
        //создание нового сотрдуника и компании
        public async Task<IActionResult> Create(string nameEmployee, string nameCompany)
        {
            //экземплояр класса Компании
            Company company = new Company() { Name=nameCompany};
            //добавлем в контексте ApllicationContext
            await _context.AddAsync(company);
            //сохраняем изменения
            await _context.SaveChangesAsync();

            Employee emp = new Employee() {Name=nameEmployee, Company= company };
            //используем сохранение навигационного поля Company через объявленную переменную company
            Employee emp2 = new Employee() {Name="сотрудник по умолчанию", Company= company};
            await _context.AddRangeAsync(emp, emp2);
            await _context.SaveChangesAsync();

            return RedirectToAction("GetAllEmployees");
        }


    }
}