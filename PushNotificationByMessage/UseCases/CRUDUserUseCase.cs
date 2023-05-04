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
        private readonly IGenericRepository<User> _usersRepo;

        public CRUDUserUseCase( IGenericRepository<User> usersRepo)
        {
            _usersRepo = usersRepo;
        }

        public async Task<GetByUserReturnDto> GetById(int id)
        {
            var user = await EncontreUmUsuarioOuLanceUmErro(id);
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
                CompanyName = user.CompanyName,
                PhoneNumber = user.PhoneNumber,
                CompanyAddress = user.CompanyAddress,
                Password = user.Password,
            };
        }

        private async Task<User> EncontreUmUsuarioOuLanceUmErro(int id)
        {
            var user = await _usersRepo.GetByIdAsync(id);

            if (user == null) { throw new KeyNotFoundException("User Not Found!"); }
            return user;
        }
    }
}
