using System.Collections.Generic;

namespace _23052022.Models
{
    public class Color:BaseEntity
    {
        public string Name { get; set; }
        public List<ProductColor> ProductColors { get; set; }
    }
}
