using PushNotificationByMessage.Models.Dtos;
using PushNotificationByMessage.Models.Entites;

namespace PushNotificationByMessage.Ports.Out
{
    public interface IUserRepository
    {
        Task<User> GetUserByIdAsync(int id);
        Task<User> PostAsync(UserRegisterDto userRequest);
    }
}
