using AdminController.Models.EntityModels;
using AdminController.Models.RequestModels;
using AdminController.Models.ResponseModels;
using AdminController.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdminController.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminServices _adminServices;

        public AdminController(IAdminServices adminServices)
        {
            _adminServices = adminServices;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _adminServices.GetAllAdmins();
            if(result == null)
            {
                return BadRequest("The database is empty.");
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
            var result = await _adminServices.CreateAdmin(model);

            if(result == null)
            {
                return BadRequest("Failed to create Admin");
            }
            return Ok(result);
        }


        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            
            var result = await _adminServices.GetAdminById(id);
            if(result == null)
            {
                return BadRequest("User not Found");
            }
            return Ok(result);
        }


        [HttpPut("UpdateAdmin")]
        public async Task<IActionResult> Update(Admin model)
        {
            TryValidateModel(model);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _adminServices.UpdateAdmin(model);

            if(result == null)
            {
                return BadRequest("Failed to update.");
            }
            return Ok(result); 
        }


        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _adminServices.DeleteAdmin(id);
            if(!result)
            {
               return  BadRequest("User not found with the given id.");
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

            var result = await _adminServices.LoginAdmin(model);

            if(result == 1)
            {
                return BadRequest("Email Not Found");
            }
            else if(result == 2)
            {
                return BadRequest("Password Not Found");
            }
            //else if(result == 3)
            //{
            //    return BadRequest("Email and Password are not matched");
            //}
            return Ok("Welcome to our website");

        }


    }
}
