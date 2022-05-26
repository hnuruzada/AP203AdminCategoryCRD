using System;
using System.Collections.Generic;

namespace _23052022.Models
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public List<Product> Products { get; set; }
    }
}
