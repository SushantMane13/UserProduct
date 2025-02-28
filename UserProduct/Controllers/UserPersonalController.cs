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
    public class UserPersonalController : ControllerBase
    {
        public readonly IUserPersonalManager userPersonalManager;

        public UserPersonalController(IUserPersonalManager userPersonalManager)
        {
            this.userPersonalManager = userPersonalManager;
        }

        [HttpGet]
        public async Task<ActionResult<List<ResponseUserPersonalDTO>>> GetALlUserPersonal()
        {
            var res = await userPersonalManager.GetAllUserPersonals();
            return Ok(res);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseUserPersonalDTO>> GetUserPersonalById(int id)
        {
            try
            {
                var res = await userPersonalManager.GetUserPersonalById(id);

                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{id?}")]
        public async Task<ActionResult<ResponseUserPersonalDTO>> UpsertUserPersonal(int? id,RequestUserPersonalDTO userPersonalDTO)
        {
            try
            {
                var res = await userPersonalManager.UpsertUserPersonal(id,userPersonalDTO);
                
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

       

        [HttpDelete("{id}/{deletedBy}")]
        public async Task<ActionResult> DeleteUserPersonal(int id,int deletedBy)
        {
            try
            {
                var res = await userPersonalManager.DeleteUserPersonal(id, deletedBy);
               
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
