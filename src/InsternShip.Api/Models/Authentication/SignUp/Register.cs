using System.ComponentModel.DataAnnotations;

namespace InsternShip.Api.Models.Authentication.SignUp
{
    public class Register
    {
        public string? FullName { get; set; }
        public string? Username { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Username is required")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }
    }
}