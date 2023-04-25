using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using PushNotificationByMessage.Models.Dtos;
using PushNotificationByMessage.Models.Entites;
using PushNotificationByMessage.Ports.In;
using PushNotificationByMessage.Ports.Out;

namespace PushNotificationByMessage.UseCases
{
    public class CRUDUserUseCase : ICRUDUserUseCase
    {
        private readonly UserManager<User> _userManager;
        private readonly IGenericRepository<User> _usersRepo;

        public CRUDUserUseCase(UserManager<User> userManager, IGenericRepository<User> usersRepo)
        {
            _userManager = userManager;
            _usersRepo = usersRepo;
        }

        public async Task<GetByUserReturnDto> GetById(int id)
        {
            var user = await EncontreUmUsuarioOuRetorneUmErro(id);
            var userReturn = await MapeieORetornoDoFluxo(user);
            return userReturn;
        }

        private async Task<GetByUserReturnDto> MapeieORetornoDoFluxo(User user)
        {
            return new GetByUserReturnDto()
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name,
                CompanyName = user.NameCompany,
                PhoneNumber = user.PhoneNumber,
                CompanyAddress = user.Address,
                Password = user.PasswordHash,
            };
        }

        private async Task<User> EncontreUmUsuarioOuRetorneUmErro(int id)
        {
            var user = await _usersRepo.GetByIdAsync(id);

            if (user == null) { throw new KeyNotFoundException("User Not Found!"); }
            return user;
        }
    }
}
