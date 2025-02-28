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
    public class DelivaryTypeController : ControllerBase
    {
        public readonly IDelivaryTypeManager delivaryTypeManager;

        public DelivaryTypeController(IDelivaryTypeManager delivaryTypeManager)
        {
            this.delivaryTypeManager = delivaryTypeManager;
        }

        [HttpGet]
        public async Task<ActionResult<List<DelivaryTypeDTO>>> GetALlDelivaryTypes()
        {
            var res = await delivaryTypeManager.GetAllDelivaryTypes();
            return Ok(res);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DelivaryTypeDTO>> GetDelivaryTypeById(int id)
        {
            try
            {
                var res = await delivaryTypeManager.GetDelivaryTypeById(id);

                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpPost]
        public async Task<ActionResult<DelivaryTypeDTO>> CreateDelivaryType(DelivaryTypeDTO delivaryTypeDTO)
        {
            try
            {
                var res = await delivaryTypeManager.CreateDelivaryType(delivaryTypeDTO);
                
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<DelivaryTypeDTO>> UpdateDelivaryType(int id, DelivaryTypeDTO delivaryTypeDTO)
        {
            try
            {
                var res = await delivaryTypeManager.UpdateDelivaryType(id, delivaryTypeDTO);
                
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDelivaryType(int id)
        {
            try
            {
                var res = await delivaryTypeManager.DeleteDelivaryType(id);
                
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
    }
}
