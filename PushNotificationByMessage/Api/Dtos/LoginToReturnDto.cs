using PushNotificationByMessage.Core.Entities;

namespace PushNotificationByMessage.Api.Dtos
{
    public class LoginToReturnDto
    {
        public string Token { get; set; }

        public UserLogin User { get; set; }
    }

    public class UserLogin
    {
        public int id { get; set; }
        public string name { get; set; } 
        public string email { get; set; }
    }
}
