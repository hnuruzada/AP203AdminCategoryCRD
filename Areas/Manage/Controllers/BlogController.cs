using _23052022.DAL;
using _23052022.Extensions;
using _23052022.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace _23052022.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class BlogController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public BlogController(AppDbContext context,IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            List<Blog> blogs = _context.Blogs.ToList();
            return View(blogs);
        }
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Blog blog)
        {
            if(!ModelState.IsValid) return View();
            if (blog.ImageFile !=null)
            {
                if (blog.ImageFile.IsImage())
                {
                    ModelState.AddModelError("ImageFile", "format duzgun deyil.");
                    return View();
                }
                if (!blog.ImageFile.IsSizeOk(5))
                {
                    ModelState.AddModelError("ImageFile", "Sekil max 5 mb ola biler");
                    return View();
                }
                
                blog.Image = blog.ImageFile.SaveImg(_env.WebRootPath, "assets/blog");


            }
            else
            {
                ModelState.AddModelError("ImageFile","Sekil elave edin");
                return View();
            }

            _context.Blogs.Add(blog);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
