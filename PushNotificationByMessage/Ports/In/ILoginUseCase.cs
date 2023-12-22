using PushNotificationByMessage.Models.Dtos;

namespace PushNotificationByMessage.Ports.In
{
    public interface ILoginUseCase
    {
        Task<LoginToReturnDto> Login(LoginDto loginRequest);
    }
}
