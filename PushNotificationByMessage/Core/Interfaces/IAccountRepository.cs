using Microsoft.AspNetCore.Identity;
using PushNotificationByMessage.Core.Request;
using System.Threading.Tasks;

namespace PushNotificationByMessage.Core.Request
{
    public interface IAccountRepository
    {
        Task<IdentityResult> SignUpAsync(UserRegisterRequest signUpModel);
        Task<string> LoginAsync(LoginRequest signInModel);
    }
}
