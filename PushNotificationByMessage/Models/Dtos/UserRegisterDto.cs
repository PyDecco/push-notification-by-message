using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace PushNotificationByMessage.Models.Dtos
{
    public class UserRegisterDto
    {
        
        public string Email { get; set; }
        public string? Name { get; set; }
        public string? CompanyName { get; set; }
        public string? Telephone { get; set; }
        public string? Address { get; set; }
        [Required]
        [Compare("ConfirmPassword")]
        public string Password { get; set; }
        [Required]
        public string ConfirmPassword { get; set; }
    }

    public class UserRegisterResponse
    {
        public int Id { get; set; }
    }

    public class ComplementUserRegisterDto : UserRegisterDto
    {
        public int Id { get; set; }
    }

    public class LoginDto
    {
        [Required, EmailAddress]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
    }

    public class GetByUserReturnDto
    {
        [JsonProperty("id")]
        public int Id;

        [JsonProperty("email")]
        public string Email;

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("company_name")]
        public string CompanyName;

        [JsonProperty("phone_number")]
        public string? PhoneNumber;

        [JsonProperty("company_address")]
        public string CompanyAddress;

        [JsonProperty("password")]
        public string Password;
    }
}
