using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserProduct.Managers.DTO.UserDTO;
using UserProduct.Managers.Interface.UserInterfaces;
using UserProduct.Services.Models.Entity;

namespace UserProduct.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public readonly IUserManager userManager;

        public UserController(IUserManager userManager)
        {
            this.userManager = userManager;
        }

        [HttpGet,Authorize]
        public async Task<ActionResult<List<ResponseUserDTO>>> GetALlUser()
        {
            var res = await userManager.GetAllUsers();
            return Ok(res);
        }

        [HttpGet("{id}"),Authorize]
        public async Task<ActionResult<RequestUserDTO>> GetUsersById(int id)
        {
            try
            {
                var res = await userManager.GetUserById(id);

                return Ok(res);
            }
            catch (Exception ex)
            { 
                return BadRequest(ex.Message);
            }
        }

        [HttpPost, Authorize]
        public async Task<ActionResult<ResponseUserDTO>> CreateUser(RequestUserDTO userDTO)
        {
            try
            {
                var res = await userManager.CreateUser(userDTO);
                
                return Ok(res);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            
        }

        [HttpPut("{id}"), Authorize]
        public async Task<ActionResult<RequestUserDTO>> UpdateUser(int id, RequestUserDTO userDTO)
        {
            try
            {
                var res = await userManager.UpdateUser(id, userDTO);
                
                return Ok(res);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            
        }

        [Authorize]
        [HttpDelete("{id}/{deletedBy}/{deletedById}")]
        public async Task<ActionResult> DeleteUser(int id, string deletedBy,int deletedById)
        {
            try
            {
                var res = await userManager.DeleteUser(id, deletedBy,deletedById);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpGet("{username}/{password}")]
        public async Task<ActionResult<string>> Login(string username,string password)
        {
            try
            {
                var res = await userManager.Login(username, password);
                return Ok(res);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            
        }
    }
}
