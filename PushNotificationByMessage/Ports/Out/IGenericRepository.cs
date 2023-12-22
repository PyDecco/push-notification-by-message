using PushNotificationByMessage.Models.Entites;

namespace PushNotificationByMessage.Ports.Out
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> ListAllAsync();
        Task<int> PostAsync(T entity);
        Task<User> GetByEmaildAsync(string email);
    }
}
