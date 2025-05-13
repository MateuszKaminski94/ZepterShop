namespace ZepterShop.Models.Dto
{
    public class OrdersSummarySqlDto
    {
        public int OrderId { get; set; }
        public string ShopName { get; set; }
        public string City { get; set; }
        public decimal NetTotal { get; set; }
    }
}
