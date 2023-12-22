using Microsoft.AspNetCore.Identity;
using PushNotificationByMessage.Models.Dtos;
using PushNotificationByMessage.Models.Entites;
using PushNotificationByMessage.Ports.In;
using PushNotificationByMessage.Ports.Out;

namespace PushNotificationByMessage.UseCases
{
    public class RegisterUseCase : IRegisterUseCase
    {
        private readonly UserManager<User> _userManager;
        private readonly IGenericRepository<User> _usersRepo;

        public RegisterUseCase(UserManager<User> userManager, IGenericRepository<User> usersRepo)
        {
            _userManager = userManager;
            _usersRepo = usersRepo;
        }

        public async Task<UserRegisterResponse> Register(UserRegisterDto userDto)
        {
            var user = await TransformeOUserRegisterDtoEmUser(userDto);
            await CrieUmUsuarioOuRetorneError(user, userDto.Password);

            return new UserRegisterResponse() { Id = user.Id };
        }

        private async Task<User> TransformeOUserRegisterDtoEmUser(UserRegisterDto userDto)
        {
            return new User()
            {
                Name = userDto.Name,
                PhoneNumber = userDto.Telephone,
                Address = userDto.Address,
                Email = userDto.Email,
                UserName = userDto.Email
            };
        }

        private async Task CrieUmUsuarioOuRetorneError(User userDto, string password)
        {
            var result = await _userManager.CreateAsync(userDto, password);
            
            if (!result.Succeeded) {
                throw new UnauthorizedAccessException(result.Errors.ToList().ToString()); }
        }
    }
}
