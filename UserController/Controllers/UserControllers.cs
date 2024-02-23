using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserController.data;
using UserController.Models.EntityModel;
using UserController.Models.RequestModel;
using UserController.Models.ResponseModel;
using UserController.Services;

namespace UserController.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserControllers : ControllerBase
    {

        private readonly IUserServices _userServices;
        private IConfiguration _configuration;
        private UserDbContext _db;

        public UserControllers(IUserServices userServices, IConfiguration configuration, UserDbContext db)
        {
            _userServices = userServices;
            _configuration = configuration;
            _db = db;
        }


        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            TryValidateModel(model);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var list = _db.Users.ToList();
            foreach (var user in list)
            {
                if (user.Email == model.Email)
                {
                    return BadRequest("Email already exists try giving another email.");
                }
            }
            var result = await _userServices.CreateUser(model);

            if (result == null)
            {
                return BadRequest("Username already exists try giving different username");
            }
            return Ok(result);
        }


        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _userServices.GetAllUsers();
            if (result == null)
            {
                return BadRequest("The Record is empty.");
            }
            return Ok(result);
        }


        [HttpGet("GetAllActiveUser")]
        public async Task<IActionResult> GetAllActive()
        {
            var result = await _userServices.GetAllActiveUsers();
            if (result == null)
            {
                return BadRequest("The Record is empty.");
            }
            return Ok(result);
        }



        [HttpPost("GetByStd/{std}")]
        public async Task<IActionResult> GetByStd(int std)
        {
            var result = await _userServices.GetAllByStd(std);
            if (result == null)
            {
                return BadRequest("The Record is empty.");
            }
            return Ok(result);
        }


        [HttpPost("GetActiveByStd/{std}")]
        public async Task<IActionResult> GetActiveByStd(int std)
        {
            var result = await _userServices.GetActiveStdentsByStd(std);
            if (result == null)
            {
                return BadRequest("The Record is empty.");
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
            
            var user = _db.Users.FirstOrDefault(u => u.Email == model.Email && u.Password == model.Password);

            var result = await _userServices.LoginUser(model);
            if (result == 2)
            {
                return Unauthorized("Password doesnt match with the Email");
            }
            else if(result == 1)
            {
                string token = GenerateToken(user);

                return Ok(new TokenResponseModel
                {
                    Token = token,
                    Username = user.Username,
                    Standard = user.Standard,
                    Id = user.Id,
                    Email = user.Email
                });
            }
            return BadRequest("Wrong Credentials");
      
                       
        }

        [NonAction]
        private string GenerateToken(User user)
        {
            var issuer = _configuration.GetValue<string>("jwt:Issuer");
            var audience = _configuration.GetValue<string>("jwt:Audience");
            var secretkey = _configuration.GetValue<string>("jwt:SecretKey");

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Name,user.Username),
                new Claim(JwtRegisteredClaimNames.Email,user.Email),
                new Claim(JwtRegisteredClaimNames.Sub,user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role,user.Role),
                new Claim(JwtRegisteredClaimNames.GivenName,user.Username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretkey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(2),
                signingCredentials: creds
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
