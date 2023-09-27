namespace Film_API.Services
{
    public interface ICrudService<T, TKey>
    {
        Task<ICollection<T>> GetAllAsync();
        /// <summary>
        /// Gets a <typeparamref name="T"/> by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> GetByIdAsync(TKey id);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task DeleteByIdAsync(TKey id);

    }
}
