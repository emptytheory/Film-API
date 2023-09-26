namespace Film_API.Services
{
    public interface ICrudService<T, TKey>
    {
        ICollection<T> GettAll();
        T GetById(TKey id);
        T Save(T entity);
        T Update(T entity);
        void Delete(TKey id);

    }
}
