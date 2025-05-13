namespace ZepterShop.Models.Basic
{
    public class Order
    {
        public int Id { get; set; }

        public int ShopId { get; set; }
        public Shop Shop { get; set; }

        public string Street { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
