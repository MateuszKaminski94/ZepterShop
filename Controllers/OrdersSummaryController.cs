using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZepterShop.Models;
using ZepterShop.Models.Dto;

namespace ZepterShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersSummaryController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public OrdersSummaryController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("CustomFiltered")]
        public async Task<IActionResult> GetFilteredOrdersWithNetTotal()
        {
            var query = @"
                SELECT o.Id AS OrderId, 
                       s.Name AS ShopName, 
                       o.City,
                       SUM(oi.NetPrice * oi.Quantity) AS NetTotal
                FROM Orders o
                INNER JOIN Shops s ON o.ShopId = s.Id
                INNER JOIN OrderItems oi ON o.Id = oi.OrderId
                WHERE s.Id % 2 = 0 AND o.City LIKE '%w%'
                GROUP BY o.Id, s.Name, o.City
            ";

            var orders = await _context.Set<OrdersSummarySqlDto>()
                .FromSqlRaw(query)
                .ToListAsync();

            return Ok(orders);
        }
    }
}
