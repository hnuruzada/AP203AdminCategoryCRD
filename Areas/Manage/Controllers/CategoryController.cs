using _23052022.DAL;
using _23052022.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace _23052022.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class CategoryController : Controller
    {
        private readonly AppDbContext _context;
        public CategoryController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<Category> category = _context.Categories.ToList();
            return View(category);
        }
        public IActionResult Create()
        {
            
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            List<Category> SameName=_context.Categories.Where(c=>c.Name.ToLower().Trim().Contains(category.Name.ToLower().Trim())).ToList();

            if(!ModelState.IsValid) return View(category);
            foreach (var item in SameName)
            {
                if (item.Name.ToLower().Trim() == category.Name.ToLower().Trim())
                {
                    ModelState.AddModelError("Name", "Eyni adda category var");
                    return View(category);
                }
            }
            if(category==null) return NotFound();
            _context.Categories.Add(category);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            Category category = _context.Categories.FirstOrDefault(c=>c.Id==id);
            return View(category);
        }
        public IActionResult Edit(Category category,int id)
        {
            if(category==null) return NotFound();
            if(!ModelState.IsValid) return BadRequest();
            Category existCategory = _context.Categories.FirstOrDefault();


            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        
        public IActionResult Delete(int id)
        {
            Category category = _context.Categories.FirstOrDefault(c => c.Id == id);
            if(category==null) return Json(new{ Status=404});
            //category.IsDeleted= true;
            _context.Categories.Remove(category);
            _context.SaveChanges();
            
            return Json(new {status=200 });
        }
    }
}
