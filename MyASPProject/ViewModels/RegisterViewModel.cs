using Microsoft.AspNetCore.Mvc;
using MyASPProject.Utilities;
using System.ComponentModel.DataAnnotations;

namespace MyASPProject.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Remote(action: "IsEmailInUse", controller: "Account")]
        [ValidEmailDomain(allowedDomain:"rapidtech.id", ErrorMessage ="Domain Harus rapidtech.id")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name ="Cinfirmation Password")]
        [Compare("Password", ErrorMessage = "Password dan Confirm password tidak sama")]
        public string ConfrimPassword { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string City { get; set; }
    }
}
