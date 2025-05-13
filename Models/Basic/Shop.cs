namespace ZepterShop.Models.Basic
{
    public class Shop
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
