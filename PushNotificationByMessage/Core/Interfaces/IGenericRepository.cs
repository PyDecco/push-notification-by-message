using PushNotificationByMessage.Core.Entities;
using PushNotificationByMessage.Core.Request;

namespace Core.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(int id); 
        Task<IReadOnlyList<T>> ListAllAsync();
        Task<int> PostAsync(T entity);
    }
}
