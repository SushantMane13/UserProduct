using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserProduct.Managers.DTO.ProductDTO;
using UserProduct.Managers.Interface.ProductInterface;

namespace UserProduct.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderStatusController : ControllerBase
    {
        public readonly IOrderStatusManager orderStatusManager;

        public OrderStatusController(IOrderStatusManager orderStatusManager)
        {
            this.orderStatusManager = orderStatusManager;
        }

        [HttpGet]
        public async Task<ActionResult<List<OrderStatusDTO>>> GetALlOrderStatus()
        {
            var res = await orderStatusManager.GetAllOrderStatus();
            return Ok(res);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderStatusDTO>> GetOrderStatusById(int id)
        {
            try
            {
                var res = await orderStatusManager.GetOrderStatusById(id);

                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<OrderStatusDTO>> CreateOrderStatus(OrderStatusDTO orderStatusDTO)
        {
            try
            {
                var res = await orderStatusManager.CreateOrderStatus(orderStatusDTO);
                
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<OrderStatusDTO>> UpdateOrderStatus(int id, OrderStatusDTO orderStatusDTO)
        {
            try
            {
                var res = await orderStatusManager.UpdateOrderStatus(id, orderStatusDTO);
                
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteOrderStatus(int id)
        {
            try
            {
                var res = await orderStatusManager.DeleteOrderStatus(id);
                
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
    }
}
