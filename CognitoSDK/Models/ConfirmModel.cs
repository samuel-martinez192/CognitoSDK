using System.ComponentModel.DataAnnotations;

namespace CognitoSDK.Models
{
    public class ConfirmModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
        
        [Required]
        public string Code { get; set; }
    }
}
