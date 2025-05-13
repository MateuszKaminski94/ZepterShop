namespace ZepterShop.Models.Dto
{
    public class OrdersSummaryEfDto
    {
        public string PaymentMethod { get; set; }
        public int OrderCount { get; set; }
        public decimal TotalGross { get; set; }
    }
}
