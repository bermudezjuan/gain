namespace core.Services.Base
{
    using Models;

    public interface IBaseService<T> where T : BaseEntity
    {
        
        Task<IEnumerable<T>> GetAllAsync(string includeProperties = null);
        Task<T> GetByIdAsync(int id, string includeProperties = null);
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<int> SaveChangesAsync();
    }
}
