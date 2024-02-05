using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TeacherWebApplication.Models.EntityModels
{
    public class Teacher
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        //username of teacher
        [Required(ErrorMessage = "Username is required")]
        [StringLength(50, ErrorMessage = "Username can't longer than 50 characters")]
        public string Username { get; set; }

        //email address of user
        [Required(ErrorMessage = "Email is required")]
        [StringLength(50, ErrorMessage = "Length must be less than 50 characters")]
        //[DataType(DataType.EmailAddress, ErrorMessage = "Invalid Email")]
        public string Email { get; set; } = string.Empty;

        // passsword for teacher
        [Required(ErrorMessage = "Password is required")]
        [StringLength(30, ErrorMessage = "Password can't be longer than 30 characters")]
        //[DataType(DataType.Password, ErrorMessage = "Invalid Password")]
        public string Password { get; set; } = string.Empty;

        //phone number of teahcer
        [Required(ErrorMessage = "Mobile No is required")]
        [StringLength(15)]
        //[DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; } = string.Empty;

        // what classes are assigned to teacher will be here by admin
        public string AssignedClasses {  get; set; } = string.Empty;

        // what classes are assigned to teacher will be here by admin
        public string TeacherSubjects {  get; set; } = string.Empty;

        [StringLength(20, ErrorMessage = "Role can't be longer than 20 characters")]
        public string Role { get; set; } = "Teacher";

        [Required(ErrorMessage = "First Name is required")]
        [StringLength(40, ErrorMessage = "Must be less than 40 characters")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Last name is required")]
        [StringLength(40, ErrorMessage = "Must be less than 40 charecters")]
        public string LastName { get; set; } = string.Empty;

        public string Qualifications {  get; set; } = string.Empty;
    }
}
