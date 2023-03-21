using PushNotificationByMessage.Core.Entities;
using PushNotificationByMessage.Core.Request;

namespace PushNotificationByMessage.Core.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUserByIdAsync(int id);
        Task<User> PostAsync(UserRequest userRequest);
    }
}
