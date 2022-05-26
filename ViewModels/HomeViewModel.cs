using _23052022.Models;
using System.Collections.Generic;

namespace _23052022.ViewModels
{
    public class HomeViewModel
    {
        public List<Slider> Sliders { get; set; }
        public Dictionary<string, string> Settings { get; set; }
        public List<Product> Products { get; set; }
    }
}
