namespace Ap103PartialView.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Image { get; set; }
        public int TagId { get; set; }
        public Tag Tag { get; set; }
    }
}
