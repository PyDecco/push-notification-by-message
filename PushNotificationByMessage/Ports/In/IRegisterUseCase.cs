using PushNotificationByMessage.Models.Dtos;

namespace PushNotificationByMessage.Ports.In
{
    public interface IRegisterUseCase
    {
        Task<UserRegisterResponse> Register(UserRegisterDto userDto);
    }
}
