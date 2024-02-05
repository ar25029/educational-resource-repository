using System.ComponentModel.DataAnnotations;

namespace TeacherWebApplication.Models.RequestModels
{
    public class TeacherLoginModel
    {
        [Required(ErrorMessage = "Email is required")]
        [StringLength(50, ErrorMessage = "Length must be less than 50 characters")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Invalid Email")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        [StringLength(30, ErrorMessage = "Password can't be longer than 30 characters")]
        [DataType(DataType.Password, ErrorMessage = "Invalid Password")]
        public string Password { get; set; } = string.Empty;
    }
}
