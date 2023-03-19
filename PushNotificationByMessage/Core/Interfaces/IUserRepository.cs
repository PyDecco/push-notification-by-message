using PushNotificationByMessage.Core.Entities;

namespace PushNotificationByMessage.Core.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUserByIdAsync(int id); 
    }
}
