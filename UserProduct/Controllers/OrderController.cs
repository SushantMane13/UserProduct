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
    public class OrderController : ControllerBase
    {
        public readonly IOrderManager orderManager;

        public OrderController(IOrderManager orderManager)
        {
            this.orderManager = orderManager;
        }

        [HttpGet]
        public async Task<ActionResult<List<DetailedOrderDTO>>> GetALlOrder()
        {
            var res = await orderManager.GetDetailedOrder();
            return Ok(res);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<DetailedOrderDTO>>> GetOrderById(int id)
        {
            try
            {
                var res = await orderManager.GetOrderByUserId(id);

                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpPost]
        public async Task<ActionResult<OrderDTO>> CreateOrder(OrderDTO orderDTO)
        {
            try
            {
                var res = await orderManager.CreateOrder(orderDTO);
                
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<OrderDTO>> UpdateOrder(int id, OrderDTO orderDTO)
        {
            try
            {
                var res = await orderManager.UpdateOrder(id, orderDTO);
                
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteOrder(int id)
        {
            try
            {
                var res = await orderManager.DeleteOrder(id);
                
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
    }
}
