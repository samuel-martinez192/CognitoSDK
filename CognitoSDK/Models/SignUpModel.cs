using System.ComponentModel.DataAnnotations;

namespace CognitoSDK.Models
{
    public class SignUpModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [StringLength(8, ErrorMessage = "Password must be at least {0} characters long!")]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [StringLength(8, ErrorMessage = "Password must be at least {0} characters long!")]
        [Compare("Password", ErrorMessage = "Password and confirmation doesn't match")]
        public string ConfirmPassword { get; set; }
    }
}
