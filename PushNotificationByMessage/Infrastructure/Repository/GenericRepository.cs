using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using PushNotificationByMessage.Core.Entities;

namespace PushNotificationByMessage.Infrastructure.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly SensediaContext _context;

        public GenericRepository(SensediaContext context)
        {
            _context = context;

        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public Task<IReadOnlyList<T>> ListAllAsync()
        {
            throw new NotImplementedException();
        }
    }
}
