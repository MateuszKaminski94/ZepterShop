using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http;
using ZepterShop.Models;
using ZepterShop.Models.Dto;

namespace ZepterShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory, ApplicationDbContext context)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> WebService()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7259/api/OrdersSummary/CustomFiltered");

            if (!response.IsSuccessStatusCode)
            {
                return View("Error", $"B³¹d API: {response.StatusCode}");
            }

            var json = await response.Content.ReadAsStringAsync();
            var orders = JsonConvert.DeserializeObject<List<OrdersSummarySqlDto>>(json);

            return View(orders);
        }

        public async Task<IActionResult> EntityFramework()
        {
            var query = _context.Orders
                .Where(o => o.OrderItems.Sum(l => l.GrossPrice * l.Quantity) >= 150)
                .GroupBy(o => o.PaymentMethod)
                .Select(g => new OrdersSummaryEfDto
                {
                    PaymentMethod = g.Key.ToString(),
                    OrderCount = g.Count(),
                    TotalGross = g.Sum(o => o.OrderItems.Sum(l => l.GrossPrice * l.Quantity))
                });

            var result = await query.ToListAsync();
            return View(result);
        }
    }
}
