using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace OrderService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestOrderController : ControllerBase
    {
        private readonly OrderDbContext _db;
        public TestOrderController(OrderDbContext db)
        {
            _db = db;
        }
        [HttpPost("AddOrder")]
        public async Task<IActionResult> Post([FromBody]Order order)
        {
            _db.Orders.Add(order);
            _db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            var result = await _db.SaveChangesAsync();
            return Ok(result > 0);
        }

        [HttpPost("AddOrderDetail")]
        public async Task<IActionResult> PostDetail([FromBody] OrderDetail detail)
        {
            _db.OrderDetails.Add(detail);
            _db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            var result = await _db.SaveChangesAsync();
            return Ok(result > 0);
        }
    }
}
