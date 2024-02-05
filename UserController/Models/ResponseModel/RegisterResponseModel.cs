using System.ComponentModel.DataAnnotations;

namespace UserController.Models.ResponseModel
{
    public class RegisterResponseModel
    {
        public int Id { get; set; }

         public string Username { get; set; } = string.Empty;

         public string Role { get; set; }


        public string Email { get; set; } = string.Empty;

          public int Standard { get; set; }


        public int Roll { get; set; }

        public string DOB { get; set; } 

    }
}
