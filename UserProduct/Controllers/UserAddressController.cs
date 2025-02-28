using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserProduct.Managers.DTO.UserDTO;
using UserProduct.Managers.Interface.UserInterfaces;

namespace UserProduct.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserAddressController : ControllerBase
    {
        public readonly IUserAddressManager userAddressManager;

        public UserAddressController(IUserAddressManager userAddressManager)
        {
            this.userAddressManager = userAddressManager;
        }

        [HttpGet]
        public async Task<ActionResult<List<ResponseUserAddressDTO>>> GetALlUserAddress()
        {
            var res = await userAddressManager.GetAllUsersAddress();
            return Ok(res);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseUserAddressDTO>> GetUserAddressById(int id)
        {
            try
            {
                var res = await userAddressManager.GetUserAddressById(id);

                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{id?}")]
        public async Task<ActionResult<ResponseUserAddressDTO>> UpsertUserAddress(int? id,RequestUserAddressDTO userAddressDTO)
        {
            try
            {
                var res = await userAddressManager.UpsertUserAddress(id,userAddressDTO);
                
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

      

        [HttpDelete("{id}/{deletedBy}")]
        public async Task<ActionResult> DeleteUserAddress(int id,int deletedBy)
        {
            try
            {
                var res = await userAddressManager.DeleteUserAddress(id, deletedBy);
                if (!res)
                    return NotFound();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
