using System.ComponentModel.DataAnnotations;

namespace ProblemsBlog.Models
{
    public class User
    {
        public int UserId { get; set; }

        [Display(Name = "Full Name")]
        public string Name { get; set; }


        [DataType(DataType.EmailAddress, ErrorMessage = "Provide a valid Email ")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]

        public string Email { get; set; }


        [Display(Name = "User Name")]
        public string UserName { get; set; }


        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

        public string Image { get; set; }
        public string Location { get; set; }
    }
}