namespace ECommerceApi.Repositories.Interfaces
{
#pragma warning disable CS1591
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);

        Task Add(T entity);
        void Update(T entity);

        void Delete(T entity);

        Task SaveChangesAsync();
    }
}
