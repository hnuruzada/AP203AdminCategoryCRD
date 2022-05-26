using _23052022.DAL;
using _23052022.Models;
using _23052022.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace _23052022.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<Slider> sliders = _context.Sliders.ToList();
            Dictionary<string, string> settings = _context.Settings.ToDictionary(x => x.Key, x => x.Value);
            List<Product> products = _context.Products.Where(p => !p.IsDeleted)
                .OrderByDescending(p => p.Id).ToList();

            HomeViewModel homeVM = new HomeViewModel
            {
                Sliders = sliders,
                Settings = settings,
                Products = products
            };

            return View(homeVM);
        }
    }
}
