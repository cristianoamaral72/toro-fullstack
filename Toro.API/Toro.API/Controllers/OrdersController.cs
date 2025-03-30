using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Toro.Domain.Interfaces.Service;
using Toro.Domain.Model;

namespace Toro.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderProcessingService _orderService;

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request)
        {

            try
            {
                var result = await _orderService.ProcessOrderAsync(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
