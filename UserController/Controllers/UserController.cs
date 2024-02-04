using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserController.Models.EntityModel;
using UserController.Models.RequestModel;
using UserController.Services;

namespace UserController.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserServices _userServices;

        public UserController(IUserServices userServices)
        {
            _userServices = userServices;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _userServices.GetAllUsers();
            if (result == null)
            {
                return BadRequest("The Recoed is empty.");
            }
            return Ok(result);
        }


        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            TryValidateModel(model);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _userServices.CreateUser(model);

            if (result == null)
            {
                return BadRequest("Failed to create User");
            }
            return Ok(result);
        }


        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {

            var result = await _userServices.GetById(id);
            if (result == null)
            {
                return BadRequest("User not Found");
            }
            return Ok(result);
        }

        [HttpPut("UpdateAdmin")]
        public async Task<IActionResult> Update(User model)
        {
            TryValidateModel(model);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _userServices.UpdateUser(model);

            if (result == null)
            {
                return BadRequest("Failed to update.");
            }
            return Ok(result);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _userServices.Deleteuser(id);
            if (!result)
            {
                return BadRequest("User not found with the given id.");
            }
            return Ok();
        }


        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            TryValidateModel(model);
            if (!ModelState.IsValid)
            {
                BadRequest(ModelState);
            }

            var result = await _userServices.LoginUser(model);

            if (result == 1)
            {
                return BadRequest("Email Not Found");
            }
            else if (result == 2)
            {
                return BadRequest("Password Not Found");
            }
            
            return Ok("Welcome to our website");

        }
    }
}
