namespace PushNotificationByMessage.Core.Request
{
    public class UserRequest
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string? Name { get; set; }
        public string? NameCompany { get; set; }
        public string? Telephone { get; set; }
        public string? Address { get; set; }
        public string Password { get; set; }
    }
}
