using Ap103PartialView.DAL;
using Ap103PartialView.Extensions;
using Ap103PartialView.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Ap103PartialView.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class BlogController : Controller
    {
        private readonly IWebHostEnvironment _env;

        private readonly AppDbContext _context;
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
            if (!ModelState.IsValid) return View();
            Blog SameName = _context.Blogs.FirstOrDefault(b => b.Title.ToLower().Trim().Contains(blog.Title.ToLower().Trim()));
            if (blog.ImageFile != null)
            {
                if (!blog.ImageFile.IsImage())
                {
                    ModelState.AddModelError("ImageFile", "Sekil formati duzgun deyil");
                    return View();
                }
                if (!blog.ImageFile.IsSizeOk(5))
                {
                    ModelState.AddModelError("ImageFile", "Sekil 5 mb yuksek olmaz");
                    return View();
                }
                blog.Image = blog.ImageFile.SaveImg(_env.WebRootPath, "blogimg");
            }
            else
            {
                ModelState.AddModelError("ImageFile", "Sekil daxil edin");
                return View();
            }

            _context.Blogs.Add(blog);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
