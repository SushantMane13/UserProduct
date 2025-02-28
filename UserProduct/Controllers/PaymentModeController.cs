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
    public class PaymentModeController : ControllerBase
    {
        public readonly IPaymentModeManager paymentModeManager;

        public PaymentModeController(IPaymentModeManager paymentModeManager)
        {
            this.paymentModeManager = paymentModeManager;
        }

        [HttpGet]
        public async Task<ActionResult<List<PaymentModeDTO>>> GetALlPaymentModes()
        {
            var res = await paymentModeManager.GetAllPaymentModes();
            return Ok(res);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentModeDTO>> GetPaymentModeById(int id)
        {
            try
            {
                var res = await paymentModeManager.GetPaymentModeById(id);

                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<PaymentModeDTO>> CreatePaymentMode(PaymentModeDTO paymentModeDTO)
        {
            try
            {
                var res = await paymentModeManager.CreatePaymentMode(paymentModeDTO);
                
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PaymentModeDTO>> UpdatePaymentMode(int id, PaymentModeDTO paymentModeDTO)
        {
            try
            {
                var res = await paymentModeManager.UpdatePaymentMode(id, paymentModeDTO);
                
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePaymentMode(int id)
        {
            try
            {
                var res = await paymentModeManager.DeletePaymentMode(id);
                
                return Ok();
            }catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
