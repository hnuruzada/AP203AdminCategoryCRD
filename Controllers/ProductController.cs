using _23052022.DAL;
using _23052022.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace _23052022.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;
        public ProductController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Detail(int? id)
        {
            if (id is null)
                return BadRequest();

            Product product = _context.Products.FirstOrDefault(p => p.Id == id && !p.IsDeleted);
            if(product == null)
                return NotFound();

            return View(product);
        }
    }
}
