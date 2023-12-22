using PushNotificationByMessage.Models.Dtos;
using PushNotificationByMessage.Models.Entites;

namespace PushNotificationByMessage.Ports.In
{
    public interface ICRUDUserUseCase
    {
        Task<GetByUserReturnDto> GetById(int id);
    }
}
