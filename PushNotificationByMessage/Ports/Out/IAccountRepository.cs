using Microsoft.AspNetCore.Identity;
using PushNotificationByMessage.Models.Dtos;
using System.Threading.Tasks;

namespace PushNotificationByMessage.Ports.Out
{
    public interface IAccountRepository
    {
        Task<IdentityResult> SignUpAsync(UserRegisterDto signUpModel);
        Task<string> LoginAsync(LoginDto signInModel);
    }
}
