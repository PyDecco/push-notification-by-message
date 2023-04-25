using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PushNotificationByMessage.Adapters.SqLiteAdapter.Infrastructure;
using PushNotificationByMessage.Models.Entites;
using PushNotificationByMessage.Ports.Out;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PushNotificationByMessage.Adapters.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly SensediaContext _context;
        private DbSet<T> _dbset;


        public GenericRepository(SensediaContext context)
        {
            _context = context;
            _dbset = context.Set<T>();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<int> PostAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);

            int result = 0;

            try
            {
                result = await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw e;
            }

            return result;
        }


        public Task<IReadOnlyList<T>> ListAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetByEmaildAsync(string email)
        {
            try
            {
                var result = await _context.Set<User>().FirstOrDefaultAsync(x => x.Email == email);

                return result;
            }
            catch (Exception e)
            {

                throw e;
            }

        }
    }
}
