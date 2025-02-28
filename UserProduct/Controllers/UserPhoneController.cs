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
    public class UserPhoneController : ControllerBase
    {
        public readonly IUserPhoneManager userPhoneManager;

        public UserPhoneController(IUserPhoneManager userPhoneManager)
        {
            this.userPhoneManager = userPhoneManager;
        }

        [HttpGet]
        public async Task<ActionResult<List<ResponseUserPhoneDTO>>> GetALlUserPhone()
        {
            var res = await userPhoneManager.GetAllUserPhones();
            return Ok(res);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseUserPhoneDTO>> GetUserPhoneById(int id)
        {
            try
            {
                var res = await userPhoneManager.GetUserPhoneById(id);

                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{id?}")]
        public async Task<ActionResult<ResponseUserPhoneDTO>> UpsertUserPhone(int? id,RequestUserPhoneDTO userPhoneDTO)
        {
            try
            {
                var res = await userPhoneManager.UpsertUserPhone(id,userPhoneDTO);
                
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        

        [HttpDelete("{id}/{deletedBy}")]
        public async Task<ActionResult> DeleteUser(int id,int deletedBy)
        {
            try
            {
                var res = await userPhoneManager.DeleteUserPhone(id, deletedBy);
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
