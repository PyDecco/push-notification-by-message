using PushNotificationByMessage.Api.Dtos;
using PushNotificationByMessage.Core.Entites;
using PushNotificationByMessage.Core.Entities;
using PushNotificationByMessage.Core.Request;

namespace Core.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id); 
        Task<IReadOnlyList<T>> ListAllAsync();
        Task<int> PostAsync(T entity);
        Task<User> GetByEmaildAsync(string email);
    }
}
