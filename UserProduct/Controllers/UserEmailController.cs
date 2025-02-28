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
    public class UserEmailController : ControllerBase
    {
        public readonly IUserEmailManager userEmailManager;

        public UserEmailController(IUserEmailManager userEmailManager)
        {
            this.userEmailManager = userEmailManager;
        }

        [HttpGet]
        public async Task<ActionResult<List<ResponseUserEmailDTO>>> GetALlUserEmail()
        {
            var res = await userEmailManager.GetAllUserEmail();
            return Ok(res);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseUserEmailDTO>> GetUserEmailById(int id)
        {
            try
            {
                var res = await userEmailManager.GetUserEmailById(id);

                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{id?}")]
        public async Task<ActionResult<ResponseUserEmailDTO>> UpsertUserEmail(int? id,RequestUserEmailDTO userEmailDTO)
        {
            try
            {
                var res = await userEmailManager.UpsertUserEmail(id,userEmailDTO);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        

        [HttpDelete("{id}/{deletedBy}")]
        public async Task<ActionResult> DeleteUserEmail(int id,int deletedBy)
        {
            try
            {
                var res = await userEmailManager.DeleteUserEmail(id, deletedBy);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
