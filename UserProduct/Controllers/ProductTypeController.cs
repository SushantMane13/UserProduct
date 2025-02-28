using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserProduct.Managers.DTO.ProductDTO;
using UserProduct.Managers.Implmentattion.ProductClasses;
using UserProduct.Managers.Interface.ProductInterface;

namespace UserProduct.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductTypeController : ControllerBase
    {
        public readonly IProductTypeManager productTypeManager;

        public ProductTypeController(IProductTypeManager productTypeManager)
        {
            this.productTypeManager = productTypeManager;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductTypeDTO>>> GetALlProductTypes()
        {
            var res = await productTypeManager.GetAllProductTypes();
            return Ok(res);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductTypeDTO>> GetProductTypeById(int id)
        {
            try
            {
                var res = await productTypeManager.GetProductTypeById(id);

                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<ProductTypeDTO>> CreateProductType(ProductTypeDTO productTypeDTO)
        {
            try
            {
                var res = await productTypeManager.CreateProductType(productTypeDTO);
                
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ProductTypeDTO>> UpdateProductType(int id, ProductTypeDTO productTypeDTO)
        {
            try
            {
                var res = await productTypeManager.UpdateProductType(id, productTypeDTO);
               
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProductType(int id)
        {
            try
            {
                var res = await productTypeManager.DeleteProductType(id);
                if (!res)
                    return NotFound();
                return Ok();
            }catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
