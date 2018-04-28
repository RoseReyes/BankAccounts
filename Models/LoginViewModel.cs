using System.ComponentModel.DataAnnotations;
namespace bank.Models
{
    public class LoginViewModel 
    {
       
        [Required(ErrorMessage="Email is required")]
        [Display(Name = "Email")]
        [EmailAddress]
        [RegularExpression(@"^[a-zA-Z0-9.+_-]+@[a-zA-Z0-9._-]+\.[a-zA-Z]+$")]
        public string Email { get; set; }
 
        [Required(ErrorMessage="Password is required")]
        [Display(Name="Password")]
        [MinLength(8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
