using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TeacherWebApplication.Data;
using TeacherWebApplication.Models.EntityModels;
using TeacherWebApplication.Models.RequestModels;
using TeacherWebApplication.Models.ResponseModels;
using TeacherWebApplication.Services;

namespace TeacherWebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly ITeacherService _ts;
        private readonly IConfiguration _configuration;

        public HomeController(ITeacherService teacherService, IConfiguration configuration)
        {
            _ts = teacherService;
            _configuration = configuration;
        }

        [HttpGet("getallteachers")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _ts.GetAllTeacher();
            if (result == null)
            {
                return BadRequest("The database is Empty");
            }
            return Ok(result);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(TeacherRegisterModel model)
        {
            TryValidateModel(model);
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _ts.CreateTeacher(model);
            if (result == null)
            {
                return BadRequest("Failed to Register");
            }
            return Ok(result);
        }

        [HttpGet("getbyid/{id}")]
        public async Task<IActionResult> GetbyId(int id)
        {
            var result = await _ts.GetTeacherById(id);
            if(result == null)
            {
                return BadRequest("User Not Found");
            }
            return Ok(result);
        }

        [HttpPut("updateteacher")]
        public async Task<IActionResult> Update(Teacher teacher)
        {
            TryValidateModel(teacher);
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _ts.UpdateTeacher(teacher);
            if (result == null)
            {
                return BadRequest("Failed to update");
            }
            return Ok(result);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _ts.DeleteTeacher(id);
            if(!result)
            {
                return BadRequest("User not fount");
            }
            return Ok();
        }

        //[HttpPost("login")]
        //public async Task<IActionResult> LoginTeacher(TeacherLoginModel model)
        //{
        //    TryValidateModel(model);
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState); 
        //    }
        //    var result = await _ts.LoginTeacher(model);
        //    if(result == 1)
        //    {
        //        return BadRequest("Password is Incorrect");
        //    }
        //    else if(result == 2)
        //    {
        //        return BadRequest("Email is Incorrect");
        //    }
        //    else if (result == 0)
        //    {
        //        return Ok("Welcome to our website");
        //    }
        //    return BadRequest("Access Denied");
        //}

        [HttpPost("token")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TokenResponseModel))]
        public async Task<ActionResult> Login(TeacherLoginModel model)
        {
            TryValidateModel(model);
            if (!ModelState.IsValid)
            {
                return Unauthorized(ModelState);
            }

            var result = await _ts.LoginTeacher(model);
            if (result == null)
            {
                return Unauthorized("Invalid Username and Password");
            }

            //if (model.Email == result.Email)
            //{
            //    if (model.Password != result.Password)
            //    {
            //        return Unauthorized("Wrong Password");
            //    }

            //}
            //else if (model.Password == result.Password)
            //{
            //    if (model.Email != result.Email)
            //    {
            //        return Unauthorized("Wrong Email");
            //    }
            //}

            string token = GenerateToken(result);

            return Ok(new TokenResponseModel
            {
                Token = token,
                Username = result.Username,
                FirstName = result.FirstName,
                LastName = result.LastName,
                Email = result.Email
            });
        }
        [NonAction]
        private string GenerateToken(Teacher user)
        {
            var issuer = _configuration.GetValue<string>("jwt:Issuer");
            var audience = _configuration.GetValue<string>("jwt:Audience");
            var secretKey = _configuration.GetValue<string>("jwt:SecretKey");
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Name, user.Username),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName)
            };
            for (int i = 0; i < 5; i++)
            {
                claims.Add(new Claim(JwtRegisteredClaimNames.Aud, "aud" + i));
            }
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                    issuer: issuer,
                    audience: null,
                    claims: claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: creds
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
