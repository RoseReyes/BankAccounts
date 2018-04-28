using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.TagHelpers;


namespace bank.Models

{
    public class RegisterViewModel
    {
        [Required(ErrorMessage="First name is required")]
        [Display(Name="First Name")]
        [MinLength(2)]
        public string FirstName { get; set; }
        
        [Required(ErrorMessage="Last name is required")]
        [Display(Name="Last Name")]
        [MinLength(2)]
        public string LastName { get; set; }
        
        [Required(ErrorMessage="Email is required")]
        [EmailAddress]
        [RegularExpression(@"^[a-zA-Z0-9.+_-]+@[a-zA-Z0-9._-]+\.[a-zA-Z]+$")]
        [Display(Name="Email")]
        public string Email { get; set; }
        
        [Required(ErrorMessage="Password is required")]
        [MinLength(8)]
        [Display(Name="Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        [Display(Name="Confirm Password")]
        [Compare("Password", ErrorMessage = "Password and confirmation must match.")]
        public string ConfirmPassword { get; set; }

    }
}
