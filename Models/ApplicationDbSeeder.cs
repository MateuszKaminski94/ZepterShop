using ZepterShop.Models.Basic;

namespace ZepterShop.Models
{
    public class ApplicationDbSeeder
    {
        private readonly ApplicationDbContext _context;

        public ApplicationDbSeeder(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            if (_context.Shops.Any()) return; //don't run again

            var shops = Enumerable.Range(1, 10)
                .Select(i => new Shop { Name = $"Sklep {i}" }).ToList();

            _context.Shops.AddRange(shops);

            foreach (var shop in shops)
            {
                for (int j = 0; j < 5; j++)
                {
                    var order = new Order
                    {
                        Shop = shop,
                        Street = "Ulica " + j,
                        City = j % 2 == 0 ? "Wrocław" : "Poznań",
                        PostalCode = "00-0" + j,
                        PaymentMethod = (PaymentMethod)(j % 3),
                        OrderItems = new List<OrderItem>
                        {
                            new OrderItem
                            {
                                ProductCode = "P" + j,
                                NetPrice = 100 + j * 10,
                                GrossPrice = 120 + j * 10,
                                Quantity = 1 + j
                            }
                        }
                    };
                    _context.Orders.Add(order);
                }
            }

            _context.SaveChanges();
        }
    }
}
