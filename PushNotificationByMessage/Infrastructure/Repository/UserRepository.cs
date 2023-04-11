using Microsoft.EntityFrameworkCore;
using PushNotificationByMessage.Core.Entities;
using PushNotificationByMessage.Core.Interfaces;
using PushNotificationByMessage.Core.Request;

namespace PushNotificationByMessage.Infrastructure.Repository
{
    public class UserRepository: IUserRepository
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

        public Task<User> PostAsync(UserRegisterRequest userRequest)
        {
            throw new NotImplementedException();
        }
    }
}
