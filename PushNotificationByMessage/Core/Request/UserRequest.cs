using System.ComponentModel.DataAnnotations;

namespace PushNotificationByMessage.Core.Request
{
    public class UserRegisterRequest
    {
        public int Id { get; set; }
        [Required]
        public string Email { get; set; }
        public string? Name { get; set; }
        public string? NameCompany { get; set; }
        public string? Telephone { get; set; }
        public string? Address { get; set; }
        [Required]
        [Compare("ConfirmPassword")]
        public string Password { get; set; }
        [Required]
        public string ConfirmPassword { get; set; }
    }

    public class LoginRequest
    {
        [Required, EmailAddress]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
