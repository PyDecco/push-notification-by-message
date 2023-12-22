using Microsoft.EntityFrameworkCore;
using PushNotificationByMessage.Adapters.SqLiteAdapter.Infrastructure;
using PushNotificationByMessage.Models.Dtos;
using PushNotificationByMessage.Models.Entites;
using PushNotificationByMessage.Ports.Out;

namespace PushNotificationByMessage.Adapters.Repository
{
    public class UserRepository : IUserRepository
    {
        private SensediaContext _context;

        public UserRepository(SensediaContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(prop => prop.Id == id);

        }

        public Task<User> PostAsync(UserRegisterDto userRequest)
        {
            throw new NotImplementedException();
        }
    }
}
